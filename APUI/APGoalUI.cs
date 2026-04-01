using UnityEngine;

public class APGoalUI : MonoBehaviour
{
    private const float PanelWidth = 225f;
    private const float PanelHeight = 270f;
    private const float Padding = 10f;
    private const float TopOffset = 400f;

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
            ReferenceWidth / 2 + PanelWidth + Padding + 25,
            TopOffset,
            PanelWidth,
            PanelHeight
        );

        DrawPanel(panelRect);

        GUILayout.BeginArea(new Rect(
            panelRect.x + Padding,
            panelRect.y + Padding,
            panelRect.width - Padding * 2,
            panelRect.height - Padding * 2
        ));

        DrawGoalContents();

        GUILayout.EndArea();

        EndGUIScale();
    }

    private void DrawGoalContents()
    {
        GUILayout.Label("STATS", TitleStyle());

        GUILayout.Space(6);

        GUILayout.Label("Goal:");

        if (APState.goalType == "key")
        {
            if (APState.RequiredKeyEnding == 1)
                GUILayout.Label("Achieve Good Ending");

            if (APState.RequiredKeyEnding == 0)
                GUILayout.Label("Achieve Bad Ending");
        }

        if (APState.goalType == "deadline")
        {
            GUILayout.Label(
                "Deadline " + APState.deadlineGoal +
                " (" + APState.deadlinesCompleted +
                "/" + APState.deadlineAmount + ")"
            );
        }

        if (APState.goalType == "card")
        {
            GUILayout.Label(
                "Beat cards " +
                APState.BeatModifiers.Count +
                "/" + APState.PackAmount
            );
        }

        GUILayout.Space(6);

        GUIStyle rt = new GUIStyle(GUI.skin.label)
        {
            richText = true
        };

        GUILayout.Label("Bonus Luck: <color=green><b>" + APState.LuckSaved + "</b></color>", rt);

        GUILayout.Space(6);

        GUILayout.Label("Bonus Clover Tickets:\nDeadlines: <color=green><b>" + APState.BonusTicketsSaved + "</b></color>\nStarting: <color=green><b>" + APState.StartTicketsSaved + "</b></color>", rt);

        GUILayout.Space(6);

        GUILayout.Label("Drawers Unlocked: <color=green><b>" + APState.UnlockedDrawers + "</b></color>", rt);

        GUILayout.Space(6);

        GUILayout.Label("Skeleton Pieces Unlocked: <color=green><b>" + APState.UnlockedSkeleton + "</b></color>", rt);
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
            alignment = TextAnchor.UpperCenter,
            normal = { textColor = Color.white }
        };
    }
}