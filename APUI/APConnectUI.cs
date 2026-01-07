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

    private readonly Rect panelRect = new Rect(10, 180, 320, 370);
    private const float Padding = 10f;

    void Update()
    {
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

        DrawPanel(panelRect);
        DrawCloseButton(panelRect);

        GUILayout.BeginArea(new Rect(
            panelRect.x + Padding,
            panelRect.y + Padding,
            panelRect.width - Padding * 2,
            panelRect.height - Padding * 2
        ));

        DrawContents();

        GUILayout.EndArea();

        if (Event.current != null &&
            panelRect.Contains(Event.current.mousePosition) &&
            (Event.current.isMouse || Event.current.isKey))
        {
            Event.current.Use();
        }
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
