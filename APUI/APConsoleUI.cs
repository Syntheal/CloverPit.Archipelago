using UnityEngine;
using static APState;

public class APConsoleUI : MonoBehaviour
{
    private const float ClosedHeight = 32f;
    private const float OpenHeight = 260f;
    private const float Width = 720f;
    private const float SlideSpeed = 12f;
    private const float Padding = 10f;

    private float currentHeight = ClosedHeight;

    private Vector2 scrollPosition = Vector2.zero;

    // Flag to determine if we need to scroll to the bottom
    private bool shouldScrollToBottom = false;

    private const float TopOffset = -2f;

    private Rect PanelRect =>
        new Rect(
            (Screen.width - Width) * 0.5f,
            Mathf.Clamp(TopOffset, -4f, 0f),
            Width,
            currentHeight
        );

    private void Update()
    {
        if (!IsConnected)
            return;

        float targetHeight = IsAPUIOpen ? OpenHeight : ClosedHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.unscaledDeltaTime * SlideSpeed);

        if (APConsoleLog.Lines.Count > 0 && !shouldScrollToBottom)
        {
            shouldScrollToBottom = true;
        }
    }

    private void OnGUI()
    {
        if (!IsConnected)
            return;

        DrawPanel(PanelRect);

        if (!IsAPUIOpen)
        {
            DrawClosedHint();
            return;
        }

        DrawConsoleContents();
    }

    private void DrawClosedHint()
    {
        float t = Mathf.InverseLerp(OpenHeight, ClosedHeight, currentHeight);

        Color c = Color.gray;
        c.a = t;

        GUIStyle style = HintStyle();
        style.normal.textColor = c;

        GUI.Label(
            new Rect(
                PanelRect.x + Padding,
                PanelRect.y + 7,
                PanelRect.width - Padding * 2,
                20
            ),
            "Press F8 to open Archipelago console",
            style
        );
    }

    private void DrawConsoleContents()
    {
        Rect headerRect = new Rect(
            PanelRect.x + Padding,
            PanelRect.y + Padding,
            PanelRect.width - Padding * 2,
            30
        );
        GUILayout.BeginArea(headerRect);
        GUILayout.Label("ARCHIPELAGO CONSOLE", TitleStyle());
        GUILayout.EndArea();

        Rect scrollRect = new Rect(
            PanelRect.x + Padding,
            PanelRect.y + Padding + 30,
            PanelRect.width - Padding * 2,
            PanelRect.height - Padding * 2 - 30
        );

        GUILayout.BeginArea(scrollRect);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(scrollRect.width), GUILayout.Height(scrollRect.height));

        foreach (var line in APConsoleLog.Lines)
        {
            GUIStyle style = GetStyleForLine(line.Type);
            GUILayout.Label(line.Text, style);
        }

        if (shouldScrollToBottom && Mathf.Abs(scrollPosition.y - Mathf.Infinity) < 0.1f)
        {
            scrollPosition.y = float.MaxValue;
            shouldScrollToBottom = false;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private GUIStyle GetStyleForLine(APConsoleLineType type)
    {
        Color color = Color.white;
        FontStyle fontStyle = FontStyle.Normal;

        switch (type)
        {
            case APConsoleLineType.DeathLink:
                color = Color.red;
                fontStyle = FontStyle.Bold;
                break;
            case APConsoleLineType.ItemReceived:
                color = Color.green;
                break;
            case APConsoleLineType.ItemSent:
                color = Color.cyan;
                break;
            case APConsoleLineType.Warning:
                color = Color.yellow;
                break;
        }

        return new GUIStyle(GUI.skin.label)
        {
            normal = { textColor = color },
            fontStyle = fontStyle,
            wordWrap = true
        };
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

    private GUIStyle TitleStyle() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 14,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.MiddleCenter,
        normal = { textColor = Color.white }
    };

    private GUIStyle HintStyle() => new GUIStyle(GUI.skin.label)
    {
        fontSize = 12,
        alignment = TextAnchor.MiddleCenter,
        normal = { textColor = Color.gray }
    };
}
