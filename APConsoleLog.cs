using System;
using System.Collections.Generic;

public enum APConsoleLineType
{
    Normal,
    ItemSent,
    ItemReceived,
    DeathLink,
    Warning
}

public struct APConsoleLine
{
    public string Text;
    public APConsoleLineType Type;
}

public static class APConsoleLog
{
    private const int MaxLines = 200;
    private static readonly List<APConsoleLine> lines = new List<APConsoleLine>(MaxLines);

    public static IReadOnlyList<APConsoleLine> Lines => lines;

    public static void Log(string message, APConsoleLineType type = APConsoleLineType.Normal)
    {
        if (lines.Count >= MaxLines)
            lines.RemoveAt(0);

        lines.Add(new APConsoleLine
        {
            Text = $"[{DateTime.Now:HH:mm:ss}] {message}",
            Type = type
        });
    }

    public static void Clear()
    {
        lines.Clear();
    }
}
