namespace AsyncFileManager.Models;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString() =>
        $"[{Id}] {Title,-30} | Tags: {string.Join(", ", Tags),-20} | {CreatedAt:dd/MM/yyyy HH:mm}";
}