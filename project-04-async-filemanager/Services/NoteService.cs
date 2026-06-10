using System.Text.Json;
using AsyncFileManager.Models;
using AsyncFileManager.Utils;

namespace AsyncFileManager.Services;

public class NoteService
{
    private List<Note> _notes = new();
    private int _nextId = 1;
    private readonly string _dataFile = "notes.json";
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    // --- Load / Save ---

    public async Task LoadAsync()
    {
        if (!File.Exists(_dataFile)) return;

        try
        {
            string json = await File.ReadAllTextAsync(_dataFile);
            _notes = JsonSerializer.Deserialize<List<Note>>(json) ?? new();
            _nextId = _notes.Count > 0 ? _notes.Max(n => n.Id) + 1 : 1;
            Console.WriteLine($"📂 Đã load {_notes.Count} ghi chú.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi load: {ex.Message}");
        }
    }

    public async Task SaveAsync()
    {
        try
        {
            string json = JsonSerializer.Serialize(_notes, _jsonOptions);
            await File.WriteAllTextAsync(_dataFile, json);
            await Logger.LogAsync($"Đã lưu {_notes.Count} ghi chú.");
            Console.WriteLine("💾 Đã lưu.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi lưu: {ex.Message}");
        }
    }

    // --- CRUD ---

    public async Task AddAsync(string title, string content, List<string> tags)
    {
        var note = new Note
        {
            Id = _nextId++,
            Title = title,
            Content = content,
            Tags = tags
        };
        _notes.Add(note);
        await Logger.LogAsync($"Thêm ghi chú: [{note.Id}] {title}");
        Console.WriteLine($"✅ Đã thêm: {title}");
    }

    public async Task UpdateAsync(int id, string newTitle, string newContent)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null) { Console.WriteLine("❌ Không tìm thấy."); return; }

        note.Title = newTitle;
        note.Content = newContent;
        note.UpdatedAt = DateTime.Now;
        await Logger.LogAsync($"Sửa ghi chú ID {id}");
        Console.WriteLine("✅ Đã cập nhật.");
    }

    public async Task DeleteAsync(int id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null) { Console.WriteLine("❌ Không tìm thấy."); return; }

        _notes.Remove(note);
        await Logger.LogAsync($"Xóa ghi chú ID {id}: {note.Title}");
        Console.WriteLine($"✅ Đã xóa: {note.Title}");
    }

    public void PrintAll()
    {
        if (_notes.Count == 0) { Console.WriteLine("Chưa có ghi chú."); return; }
        _notes.ForEach(n => Console.WriteLine(n));
    }

    public void PrintDetail(int id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null) { Console.WriteLine("❌ Không tìm thấy."); return; }

        Console.WriteLine($"\n===== 📝 {note.Title} =====");
        Console.WriteLine($"ID       : {note.Id}");
        Console.WriteLine($"Tags     : {string.Join(", ", note.Tags)}");
        Console.WriteLine($"Tạo lúc  : {note.CreatedAt:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Sửa lúc  : {note.UpdatedAt:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"\n{note.Content}");
    }

    // --- Search ---

    public List<Note> Search(string keyword) =>
        _notes.Where(n =>
            n.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            n.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
        .ToList();

    public List<Note> FilterByTag(string tag) =>
        _notes.Where(n => n.Tags.Any(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    // --- Import / Export ---

    public async Task ImportFromFilesAsync(string[] filePaths)
    {
        Console.WriteLine($"📥 Đang import {filePaths.Length} file song song...");

        // Đọc tất cả file cùng lúc — Task.WhenAll
        var tasks = filePaths.Select(path => ReadFileWithRetryAsync(path));
        string[] contents = await Task.WhenAll(tasks);

        for (int i = 0; i < filePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(contents[i])) continue;
            string title = Path.GetFileNameWithoutExtension(filePaths[i]);
            await AddAsync(title, contents[i], new List<string> { "imported" });
        }

        Console.WriteLine("✅ Import hoàn tất.");
    }

    public async Task ExportToFileAsync(int id, string format = "txt")
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null) { Console.WriteLine("❌ Không tìm thấy."); return; }

        string fileName = $"export_{note.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.{format}";
        string content = format == "json"
            ? JsonSerializer.Serialize(note, _jsonOptions)
            : $"Title: {note.Title}\nTags: {string.Join(", ", note.Tags)}\n\n{note.Content}";

        await File.WriteAllTextAsync(fileName, content);
        await Logger.LogAsync($"Export ghi chú ID {id} → {fileName}");
        Console.WriteLine($"✅ Đã export: {fileName}");
    }

    public async Task BackupAsync()
    {
        string backupFile = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        string json = JsonSerializer.Serialize(_notes, _jsonOptions);
        await File.WriteAllTextAsync(backupFile, json);
        await Logger.LogAsync($"Backup → {backupFile}");
        Console.WriteLine($"✅ Backup: {backupFile}");
    }

    // --- Retry mechanism ---

    private static async Task<string> ReadFileWithRetryAsync(string path, int maxRetry = 3)
    {
        for (int attempt = 1; attempt <= maxRetry; attempt++)
        {
            try
            {
                return await File.ReadAllTextAsync(path);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"⚠️  Lần {attempt} thất bại: {path} — {ex.Message}");
                if (attempt < maxRetry)
                    await Task.Delay(500 * attempt);   // chờ 500ms, 1000ms, 1500ms
            }
        }
        return string.Empty;
    }
}