using System.Text.Json;
using ConsoleFundamentals.Models;

namespace ConsoleFundamentals.Utils;

public static class FileHelper
{
    private static readonly string FilePath = "students.json";

    public static void Save(List<Student> students)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(students, options);
        File.WriteAllText(FilePath, json);
        Console.WriteLine("💾 Đã lưu dữ liệu.");
    }

    public static List<Student> Load()
    {
        if (!File.Exists(FilePath)) return new List<Student>();

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }
}