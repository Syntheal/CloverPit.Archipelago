using System.Linq;
using UnityEngine;
using static APState;

public class APConsoleUI : MonoBehaviour
{
    private APConsoleLine[] cachedLines = System.Array.Empty<APConsoleLine>();
    private int lastLineCount = 0;

    private const float ReferenceWidth = 1920f;
    private const float ReferenceHeight = 1080f;

    private const float ClosedHeight = 32f;
    private const float OpenHeight = 260f;
    private const float Width = 720f;
    private const float SlideSpeed = 12f;
    private const float Padding = 10f;
    private const float TopOffset = -2f;

    private float currentHeight = ClosedHeight;
    private Vector2 scrollPosition = Vector2.zero;
    private bool shouldScrollToBottom = false;

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

    private Rect GetPanelRect()
    {
        return new Rect(
            (ReferenceWidth - Width) * 0.5f,
            Mathf.Clamp(TopOffset, -4f, 0f),
            Width,
            currentHeight
        );
    }

    private void Update()
    {
        if (!IsConnected)
            return;

        float targetHeight = IsAPUIOpen ? OpenHeight : ClosedHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.unscaledDeltaTime * SlideSpeed);

        int currentCount = APConsoleLog.Lines.Count;
        if (currentCount != lastLineCount)
        {
            shouldScrollToBottom = true;
            lastLineCount = currentCount;
        }
    }

    private void OnGUI()
    {
        if (!IsConnected)
            return;

        BeginGUIScale();

        Rect panel = GetPanelRect();
        DrawPanel(panel);

        if (!IsAPUIOpen)
        {
            DrawClosedHint(panel);
            EndGUIScale();
            return;
        }

        DrawConsoleContents(panel);

        EndGUIScale();
    }
    private void DrawClosedHint(Rect panel)
    {
        float t = Mathf.InverseLerp(OpenHeight, ClosedHeight, currentHeight);

        Color c = Color.ghostWhite;
        c.a = t;

        GUIStyle style = HintStyle();
        style.normal.textColor = c;

        GUI.Label(
            new Rect(
                panel.x + Padding,
                panel.y + 7,
                panel.width - Padding * 2,
                20
            ),
            "Press F8 to open Archipelago console",
            style
        );
    }

    private void DrawConsoleContents(Rect panel)
    {
        if (Event.current.type == EventType.Layout)
        {
            cachedLines = APConsoleLog.Lines.ToArray();
        }

        Rect headerRect = new Rect(
            panel.x + Padding,
            panel.y + Padding,
            panel.width - Padding * 2,
            30
        );

        GUILayout.BeginArea(headerRect);
        GUILayout.Label("ARCHIPELAGO CONSOLE", TitleStyle());

        GUILayout.EndArea();

        Rect scrollRect = new Rect(
            panel.x + Padding,
            panel.y + Padding + 30,
            panel.width - Padding * 2,
            panel.height - Padding * 2 - 30
        );

        GUILayout.BeginArea(scrollRect);

        scrollPosition = GUILayout.BeginScrollView(
            scrollPosition,
            GUILayout.Width(scrollRect.width),
            GUILayout.Height(scrollRect.height)
        );

        foreach (var line in cachedLines)
        {
            GUIStyle style = GetStyleForLine(line.Type);
            GUILayout.Label(line.Text, style);
        }

        if (shouldScrollToBottom)
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
