using ConsoleFundamentals.Models;

namespace ConsoleFundamentals.Services;

public class StudentService
{
    private List<Student> _students = new();

    public void Add(Student student)
    {
        _students.Add(student);
        Console.WriteLine($"✅ Đã thêm sinh viên: {student.Name}");
    }

    public List<Student> GetAll() => _students;

    public Student? FindByName(string name)
    {
        return _students.FirstOrDefault(s =>
            s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> SortByAverage() =>
        _students.OrderByDescending(s => s.AverageScore()).ToList();

    public void AddScore(string studentName, string subject, double score)
    {
        var student = FindByName(studentName);
        if (student == null)
        {
            Console.WriteLine("❌ Không tìm thấy sinh viên.");
            return;
        }
        student.Scores[subject] = score;
        Console.WriteLine($"✅ Đã thêm điểm {subject}: {score} cho {student.Name}");
    }

    public void Remove(string name)
    {
        var student = FindByName(name);
        if (student == null)
        {
            Console.WriteLine("❌ Không tìm thấy sinh viên.");
            return;
        }
        _students.Remove(student);
        Console.WriteLine($"✅ Đã xóa: {student.Name}");
    }

    public void LoadFromFile(List<Student> students)
    {
        _students = students;
    }
}