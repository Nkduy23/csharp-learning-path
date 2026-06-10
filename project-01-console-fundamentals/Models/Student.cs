namespace ConsoleFundamentals.Models;

public class Student
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public Dictionary<string, double> Scores { get; set; } = new();

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public double AverageScore()
    {
        if (Scores.Count == 0) return 0;
        return Scores.Values.Average();
    }

    public string Rank()
    {
        double avg = AverageScore();
        return avg switch
        {
            >= 9.0 => "Xuất sắc",
            >= 8.0 => "Giỏi",
            >= 6.5 => "Khá",
            >= 5.0 => "Trung bình",
            _      => "Yếu"
        };
    }

    public override string ToString()
    {
        return $"{Name} | Tuổi: {Age} | TB: {AverageScore():F2} | {Rank()}";
    }
}