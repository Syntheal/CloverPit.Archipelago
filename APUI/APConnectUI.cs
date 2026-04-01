using Archipelago;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

public class APConnectUI : MonoBehaviour
{
    private string host = "archipelago.gg:38281";
    private string slotName = "";
    private string password = "";

    private string statusText = "Disconnected";

    private bool isConnecting = false;

    private const float ReferenceWidth = 1920f;
    private const float ReferenceHeight = 1080f;

    private readonly Rect panelRect = new Rect(10, 180, 320, 335);
    private const float Padding = 10f;

    private Matrix4x4 oldMatrix;
    private float currentScale = 1f;

    private float deleteHoldTime = 0f;
    private const float deleteHoldDuration = 5f;

    private static string SaveDirectory =>
        Path.Combine(BepInEx.Paths.ConfigPath, "AP_SAVES");
    private static string DataDirectory =>
        Path.Combine(BepInEx.Paths.ConfigPath, "AP_LOGIN");
    private static string LoginFile =>
        Path.Combine(DataDirectory, "login.txt");
    void Start()
    {
        LoadLoginData();
    }

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
                statusText = "Connected (Close to interact [F8])";
            }
            else
            {
                statusText = "Disconnected";
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            APCardUnlocker.GrantPack("Console","Card Pack 1");
        }
        if (Input.GetKeyDown(KeyCode.F8))
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
            deleteHoldTime = 0f;
        }
    }

    void OnGUI()
    {
        if (!APState.IsAPUIOpen)
            return;

        BeginGUIScale();

        Rect panel = GetCenteredPanel();

        DrawPanel(panel);

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

            if (panel.Contains(unscaledMouse))
            {
                if (Event.current.isMouse)
                {
                    Event.current.Use();
                }
                else if (Event.current.isKey && Event.current.keyCode != KeyCode.F8)
                {
                    Event.current.Use();
                }
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

        GUILayout.Space(3);

        GUIStyle valueStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 13,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.green }
        };

        GUILayout.Space(3);

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

        GUILayout.Space(6);

        if (APState.IsConnected)
        {
            if (GUILayout.Button("Disconnect"))
            {
                APClient.Disconnect();
                statusText = "Disconnected";
            }
        }
        else
        {
            Rect buttonRect = GUILayoutUtility.GetRect(
                 new GUIContent("Delete Saves"),
                 GUI.skin.button
            );

            Event e = Event.current;

            bool hovering = buttonRect.Contains(e.mousePosition);

            if (hovering && e.type == EventType.MouseDown && e.button == 0)
            {
                deleteHoldTime = 0f;
            }

            if (hovering && Input.GetMouseButton(0))
            {
                deleteHoldTime += Time.deltaTime;

                if (deleteHoldTime >= deleteHoldDuration)
                {
                    DeleteAllSaves();
                    deleteHoldTime = 0f;
                }
            }
            else if (!Input.GetMouseButton(0))
            {
                deleteHoldTime = 0f;
            }

            float t = Mathf.Clamp01(deleteHoldTime / deleteHoldDuration);

            Color start = new Color(0.4f, 0f, 0f);
            Color end = Color.red;

            GUI.backgroundColor = Color.Lerp(start, end, t);

            string label = $"Delete Saves ({deleteHoldTime:0.0}s / {deleteHoldDuration}s)";

            GUI.Button(buttonRect, label);

            GUI.backgroundColor = Color.white;
        }
        GUI.enabled = true;
    }

    private void DeleteAllSaves()
    {
        try
        {
            if (Directory.Exists(SaveDirectory))
            {
                Directory.Delete(SaveDirectory, true);
                Directory.CreateDirectory(SaveDirectory);
            }

            statusText = "Saves Deleted";
            Debug.Log("[Archipelago] All AP saves deleted.");

            APState.Reset();
        }
        catch (System.Exception e)
        {
            statusText = "Delete Failed";
            Debug.LogError("[Archipelago] Failed to delete saves: " + e);
        }
    }

    private async Task StartConnect()
    {
        statusText = "Connecting...";
        isConnecting = true;
        GUI.FocusControl(null);

        try
        {
            await APClient.Connect(host, slotName, password);

            if (APState.IsConnected)
            {
                BlockNewRunWithoutAPPatch.connected = true;
                statusText = "Connected";
                SaveLoginData();
            }
            else
            {
                statusText = "Disconnected";
            }
        }
        finally
        {
            isConnecting = false;
        }
    }
    private void SaveLoginData()
    {
        try
        {
            Directory.CreateDirectory(DataDirectory);

            File.WriteAllLines(LoginFile, new[]
            {
            host ?? "",
            slotName ?? "",
            password ?? ""
        });

            Debug.Log("[Archipelago] Login data saved.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("[Archipelago] Failed to save login data: " + e);
        }
    }
    private void LoadLoginData()
    {
        try
        {
            if (!File.Exists(LoginFile))
                return;

            var lines = File.ReadAllLines(LoginFile);

            if (lines.Length > 0) host = lines[0];
            if (lines.Length > 1) slotName = lines[1];
            if (lines.Length > 2) password = lines[2];

            Debug.Log("[Archipelago] Login data loaded.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("[Archipelago] Failed to load login data: " + e);
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
