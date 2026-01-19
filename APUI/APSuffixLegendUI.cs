using UnityEngine;

public class APSuffixLegendUI : MonoBehaviour
{
    private const float PanelWidth = 300f;
    private const float PanelHeight = 180f;
    private const float Padding = 10f;
    private const float TopOffset = 50f;

    private const float ReferenceWidth = 1920f;
    private const float ReferenceHeight = 1080f;
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

    private void OnGUI()
    {
        if (!APState.IsConnected || !APState.APSaveLoaded)
            return;

        if (!APState.IsAPUIOpen)
            return;
        BeginGUIScale();

        Rect panelRect = new Rect(
            ReferenceWidth - PanelWidth - Padding,
            TopOffset,
            PanelWidth,
            PanelHeight
        );

        Color oldColor = GUI.color;
        GUI.color = Color.black;
        GUI.DrawTexture(panelRect, Texture2D.whiteTexture);
        GUI.color = Color.white;

        GUI.DrawTexture(new Rect(panelRect.x, panelRect.y, panelRect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.x, panelRect.yMax - 1, panelRect.width, 1), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.x, panelRect.y, 1, panelRect.height), Texture2D.whiteTexture);
        GUI.DrawTexture(new Rect(panelRect.xMax - 1, panelRect.y, 1, panelRect.height), Texture2D.whiteTexture);

        float y = panelRect.y + Padding;

        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.UpperCenter,
            normal = { textColor = Color.white }
        };
        GUI.Label(new Rect(panelRect.x, y, panelRect.width, 24), "ARCHIPELAGO SUFFIXES", titleStyle);
        y += 30f;

        GUIStyle explanationStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 12,
            wordWrap = true,
            alignment = TextAnchor.UpperCenter,
            normal = { textColor = Color.floralWhite }
        };
        GUI.Label(
            new Rect(panelRect.x + 5, y, panelRect.width - 10, 36),
            "Suffixes indicate whether a location has been checked in Archipelago.",
            explanationStyle
        );
        y += 45f;

        DrawLegendLine(panelRect, ref y, "B", "Bought", "Not Bought");
        DrawLegendLine(panelRect, ref y, "A", "Activated", "Not Activated");
        DrawLegendLine(panelRect, ref y, "T", "Triggered", "Not Triggered");

        GUI.color = oldColor;
        EndGUIScale();
    }

    private void DrawLegendLine(Rect panelRect, ref float y, string letter, string yesText, string noText)
    {
        float labelHeight = 24f;
        float spacing = 8f;
        float linePadding = 10f;

        GUIStyle styleLetterYes = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.green }
        };

        GUIStyle styleLetterNo = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.red }
        };

        GUIStyle styleText = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            normal = { textColor = Color.white }
        };

        GUIStyle stylePipe = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.gray },
            alignment = TextAnchor.MiddleCenter
        };

        float totalWidth = panelRect.width - linePadding * 2;
        float pipeWidth = 20f;
        float halfWidth = (totalWidth - pipeWidth) / 2f;

        Rect leftRect = new Rect(panelRect.x + linePadding, y, halfWidth, labelHeight);
        GUI.Label(new Rect(leftRect.x, leftRect.y, 20f, labelHeight), letter, styleLetterYes);
        GUI.Label(new Rect(leftRect.x + 22f, leftRect.y, leftRect.width - 22f, labelHeight), "- " + yesText, styleText);

        Rect middleRect = new Rect(panelRect.x + linePadding + halfWidth, y, pipeWidth, labelHeight);
        GUI.Label(middleRect, "|", stylePipe);

        Rect rightRect = new Rect(panelRect.x + linePadding + halfWidth + pipeWidth, y, halfWidth, labelHeight);

        string textPart = noText + " - ";
        string letterPart = letter;

        Vector2 textSize = styleText.CalcSize(new GUIContent(textPart));
        Vector2 letterSize = styleLetterNo.CalcSize(new GUIContent(letterPart));

        float totalSize = textSize.x + letterSize.x;
        float startX = rightRect.x + Mathf.Max(0f, rightRect.width - totalSize);

        GUIStyle styleRight = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleRight,
            richText = true,
            normal = { textColor = Color.white }
        };

        GUI.Label(
            new Rect(rightRect.x, rightRect.y, rightRect.width, labelHeight),
            noText + " - " + "<color=red>" + letter + "</color>",
            styleRight
        );


        y += labelHeight + spacing;
    }

}
