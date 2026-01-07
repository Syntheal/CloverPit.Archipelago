using UnityEngine;
using static APState;

public class APUITrapPopup : MonoBehaviour
{
    public static APUITrapPopup Instance { get; private set; }

    private const float PanelMaxWidth = 720f;
    private const float Padding = 20f;
    private const float ButtonHeight = 30f;

    public bool isShowing;
    private string message;
    private string senderName;

    private float panelWidth;
    private float panelHeight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (APState.ShowTrapPopup && !isShowing)
        {
            Open();
        }
    }

    private void OnGUI()
    {
        if (!isShowing)
            return;

        panelWidth = Mathf.Min(PanelMaxWidth, Screen.width * 0.8f);

        panelHeight = Mathf.Max(240f, 80f + (message.Split('\n').Length * 20f) + ButtonHeight + Padding * 2);

        Rect panelRect = new Rect(
            (Screen.width - panelWidth) * 0.5f,
            (Screen.height - panelHeight) * 0.5f,
            panelWidth,
            panelHeight
        );

        DrawPanel(panelRect);
        DrawContents(panelRect);
    }

    private void DrawContents(Rect panelRect)
    {
        GUILayout.BeginArea(new Rect(panelRect.x + Padding, panelRect.y + Padding, panelRect.width - Padding * 2, panelRect.height - Padding * 2));

        GUILayout.Label("TRAP ACTIVATED", TitleStyle());
        GUILayout.Space(12);

        GUILayout.Label($"Trap sent by: {senderName}", ContentStyle());
        GUILayout.Label($"Lost: {message}", ContentStyle());

        GUILayout.Space(20);

        GUILayout.Label("Be careful! More traps may be on the way.", ContentStyle());

        GUILayout.Space(10);
        if (GUILayout.Button("Close", GUILayout.Height(ButtonHeight)))
        {
            Close();
        }

        GUILayout.EndArea();
    }

    private void DrawPanel(Rect panelRect)
    {
        Color old = GUI.color;
        GUI.color = Color.black;
        GUI.DrawTexture(panelRect, Texture2D.whiteTexture);

        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(panelRect.x, panelRect.y, panelRect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.x, panelRect.yMax - 1, panelRect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.x, panelRect.y, 1, panelRect.height), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.xMax - 1, panelRect.y, 1, panelRect.height), Texture2D.whiteTexture);

        GUI.color = old;
    }

    private void Open()
    {
        isShowing = true;

        EnableCursor();
        APFreezeHelper.FreezePlayer();
    }

    private void Close()
    {
        isShowing = false;

        APState.ShowTrapPopup = false;
        senderName = "";
        message = "";

        DisableCursor();
    }

    private GUIStyle TitleStyle() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 20,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.MiddleCenter,
        normal = { textColor = Color.red }
    };

    private GUIStyle ContentStyle() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 16,
        alignment = TextAnchor.MiddleCenter,
        normal = { textColor = Color.white },
        wordWrap = true
    };

    public void ShowTrapInfo(string trapMessage, string sender)
    {
        message = trapMessage;
        senderName = sender;
        APState.ShowTrapPopup = true;
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
