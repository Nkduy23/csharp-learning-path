namespace AsyncFileManager.Utils;

public static class Logger
{
    private static readonly string LogFile = "app.log";

    public static async Task LogAsync(string message)
    {
        string entry = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {message}";
        await File.AppendAllTextAsync(LogFile, entry + Environment.NewLine);
    }

    public static async Task PrintLogsAsync()
    {
        if (!File.Exists(LogFile))
        {
            Console.WriteLine("Chưa có log nào.");
            return;
        }
        string[] lines = await File.ReadAllLinesAsync(LogFile);
        Console.WriteLine($"\n===== 📋 LOG ({lines.Length} dòng) =====");
        foreach (var line in lines.TakeLast(20))
            Console.WriteLine(line);
    }
}