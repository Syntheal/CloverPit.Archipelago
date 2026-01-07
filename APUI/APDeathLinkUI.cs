using UnityEngine;
using static APState;

public class APDeathLinkUI : MonoBehaviour
{
    private const float ShowDuration = 4f;
    private float hideTime;
    private bool isShowing;

    private void Update()
    {
        if (APDeathState.ShowDeathLinkUI && !isShowing)
        {
            Open();
        }

        if (isShowing && Time.unscaledTime >= hideTime)
        {
            Close();
        }
    }

    private void OnGUI()
    {
        if (!isShowing)
            return;

        Rect rect = new Rect(
            Screen.width * 0.5f - 220f,
            Screen.height * 0.5f - 90f,
            440f,
            180f
        );

        GUI.color = Color.black;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;

        GUILayout.BeginArea(rect);
        GUILayout.Space(12);

        GUILayout.Label("DEATH LINK", TitleStyle());
        GUILayout.Space(12);

        GUILayout.Label($"Killed by: {APDeathState.DeathLinkSource}");
        GUILayout.Label(APDeathState.DeathLinkCause);

        GUILayout.Space(20);
        GUILayout.Label("The network has claimed you.");

        GUILayout.EndArea();
    }

    private void Open()
    {
        isShowing = true;
        hideTime = Time.unscaledTime + ShowDuration;

        EnableCursor();
        APFreezeHelper.FreezePlayer();
    }

    private void Close()
    {
        isShowing = false;

        APDeathState.ShowDeathLinkUI = false;
        APDeathState.DeathLinkSource = "";
        APDeathState.DeathLinkCause = "";

        DisableCursor();
    }

    private GUIStyle TitleStyle()
    {
        return new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = Color.red }
        };
    }

    private static void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private static void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
