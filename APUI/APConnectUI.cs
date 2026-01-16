using Archipelago;
using System.Threading.Tasks;
using UnityEngine;

public class APConnectUI : MonoBehaviour
{
    private string host = "archipelago.gg:38281";
    private string slotName = "";
    private string password = "";

    private string statusText = "Disconnected";
    private string errorText = "";

    private bool isConnecting = false;

    private const float ReferenceWidth = 1920f;
    private const float ReferenceHeight = 1080f;

    private readonly Rect panelRect = new Rect(10, 180, 320, 370);
    private const float Padding = 10f;

    private Matrix4x4 oldMatrix;
    private float currentScale = 1f;

    private void BeginGUIScale()
    {
        oldMatrix = GUI.matrix;

        float scaleX = Screen.width / ReferenceWidth;
        float scaleY = Screen.height / ReferenceHeight;
        currentScale = Mathf.Min(scaleX, scaleY);

        GUI.matrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            new Vector3(currentScale, currentScale, 1f)
        );
    }

    private void EndGUIScale()
    {
        GUI.matrix = oldMatrix;
    }

    private Rect GetCenteredPanel()
    {
        return new Rect(
            (ReferenceWidth - panelRect.width) / 2f,
            (ReferenceHeight - panelRect.height) / 2f,
            panelRect.width,
            panelRect.height
        );
    }

    void Update()
    {
        if (!isConnecting)
        {
            if (APState.IsConnected)
            {
                statusText = "Connected (Close to interact)";
            }
            else
            {
                statusText = "Disconnected";
            }
        }

        if (Input.GetKeyDown(KeyCode.F8) && !APUITrapPopup.Instance.isShowing)
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        APState.IsAPUIOpen = !APState.IsAPUIOpen;

        if (APState.IsAPUIOpen)
        {
            EnableCursor();
            APFreezeHelper.FreezePlayer();
        }
        else
        {
            DisableCursor();
        }
    }

    void OnGUI()
    {
        if (!APState.IsAPUIOpen)
            return;

        BeginGUIScale();

        Rect panel = GetCenteredPanel();

        DrawPanel(panel);
        DrawCloseButton(panel);

        GUILayout.BeginArea(new Rect(
            panel.x + Padding,
            panel.y + Padding,
            panel.width - Padding * 2,
            panel.height - Padding * 2
        ));

        DrawContents();

        GUILayout.EndArea();

        if (Event.current != null)
        {
            Vector2 unscaledMouse = Event.current.mousePosition / currentScale;

            if (panel.Contains(unscaledMouse) &&
                (Event.current.isMouse || Event.current.isKey))
            {
                Event.current.Use();
            }
        }

        EndGUIScale();
    }

    private void DrawContents()
    {
        GUILayout.Label("ARCHIPELAGO", TitleStyle());
        GUILayout.Space(10);

        GUILayout.Label("Status:");
        GUILayout.Label(statusText, StatusStyle(APState.IsConnected));

        GUILayout.Space(14);

        GUILayout.Label("Host");
        DrawReadOnlyTextField(ref host, APState.IsConnected || isConnecting);

        GUILayout.Label("Slot");
        DrawReadOnlyTextField(ref slotName, APState.IsConnected || isConnecting);

        GUILayout.Label("Password");
        DrawReadOnlyTextField(ref password, APState.IsConnected || isConnecting);

        GUILayout.Space(16);

        GUI.enabled = !APState.IsConnected && !isConnecting;
        if (GUILayout.Button(isConnecting ? "Connecting..." : "Connect"))
        {
            _ = StartConnect();
        }
        GUI.enabled = true;

        if (APState.IsConnected)
        {
            GUILayout.Space(6);
            if (GUILayout.Button("Disconnect"))
            {
                APClient.Disconnect();
                APResyncManager.Reset();
                statusText = "Disconnected";
            }
        }

        GUILayout.Space(6);

        if (GUILayout.Button("Resync Items"))
        {
            APClient.RequestResync();
            statusText = "Resync requested...";
        }

        if (!string.IsNullOrEmpty(errorText))
        {
            GUILayout.Space(6);
            GUILayout.Label(errorText, ErrorStyle());
        }
    }

    private async Task StartConnect()
    {
        errorText = "";
        statusText = "Connecting...";
        isConnecting = true;

        try
        {
            await APClient.Connect(host, slotName, password);

            if (APState.IsConnected)
            {
                statusText = "Connected";
            }
            else
            {
                statusText = "Disconnected";
                errorText = "Failed to connect";
            }
        }
        catch (System.Exception ex)
        {
            statusText = "Disconnected";
            errorText = ex.Message;
        }
        finally
        {
            isConnecting = false;
        }
    }

    private void DrawReadOnlyTextField(ref string value, bool readOnly)
    {
        GUI.enabled = !readOnly;
        value = GUILayout.TextField(value);
        GUI.enabled = true;
    }

    private void DrawPanel(Rect rect)
    {
        Color old = GUI.color;
        GUI.color = Color.black;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);

        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(rect.x, rect.yMax - 1, rect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(rect.x, rect.y, 1, rect.height), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(rect.xMax - 1, rect.y, 1, rect.height), Texture2D.whiteTexture);

        GUI.color = old;
    }

    private void DrawCloseButton(Rect panel)
    {
        Rect buttonRect = new Rect(
            panel.xMax - 40,
            panel.y + 4,
            36,
            36
        );

        Color old = GUI.color;
        GUI.color = Color.red;

        if (GUI.Button(buttonRect, "X"))
        {
            APState.IsAPUIOpen = false;
            DisableCursor();
        }

        GUI.color = old;
    }

    private GUIStyle TitleStyle()
    {
        return new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.white }
        };
    }

    private GUIStyle StatusStyle(bool connected)
    {
        return new GUIStyle(GUI.skin.label)
        {
            normal = { textColor = connected ? Color.green : Color.red }
        };
    }

    private GUIStyle ErrorStyle()
    {
        return new GUIStyle(GUI.skin.label)
        {
            wordWrap = true,
            normal = { textColor = Color.red }
        };
    }

    public static void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
