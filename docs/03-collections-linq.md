# 📘 Collections & LINQ

> LINQ là một trong những tính năng mạnh nhất của C# — không có trong Java

---

## 1. Collections quan trọng

```csharp
// List<T> — ordered, duplicates allowed
var list = new List<int> { 3, 1, 4, 1, 5, 9, 2 };
list.Add(6);
list.Insert(0, 0);           // chèn vào index 0
list.Remove(1);              // xóa phần tử đầu tiên có giá trị 1
list.RemoveAt(2);            // xóa theo index
list.Sort();
list.Reverse();
list.Contains(5);            // true
list.IndexOf(9);             // vị trí của 9
list[^1];                    // phần tử cuối (C# syntax)
list[1..4];                  // slice index 1 đến 3 (Range)

// HashSet<T> — không duplicate, unordered
var set = new HashSet<string> { "C#", "Java", "Python" };
set.Add("C#");               // không thêm vì đã có
set.UnionWith(other);        // hợp
set.IntersectWith(other);    // giao
set.ExceptWith(other);       // hiệu

// Dictionary<K,V>
var dict = new Dictionary<string, int>();
dict["key"] = 1;
dict.TryGetValue("key", out int val);    // safe get
dict.ContainsKey("key");
dict.Remove("key");
foreach (var (key, value) in dict) { }  // deconstruct tuple

// Queue<T> — FIFO
var queue = new Queue<string>();
queue.Enqueue("first");
queue.Dequeue();             // lấy và xóa
queue.Peek();                // xem không xóa

// Stack<T> — LIFO
var stack = new Stack<int>();
stack.Push(1);
stack.Pop();
stack.Peek();
```

---

## 2. LINQ — Language Integrated Query

LINQ cho phép query collections **như query SQL** ngay trong C#.

### Method syntax (phổ biến hơn)

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Where — lọc (giống SQL WHERE)
var evens = numbers.Where(n => n % 2 == 0);
// [2, 4, 6, 8, 10]

// Select — biến đổi (giống SQL SELECT / map)
var squared = numbers.Select(n => n * n);
// [1, 4, 9, 16, 25, ...]

// OrderBy / OrderByDescending
var sorted = numbers.OrderByDescending(n => n);

// First, FirstOrDefault
var first = numbers.First(n => n > 5);          // 6 — throw nếu không có
var firstOrNull = numbers.FirstOrDefault(n => n > 100);  // default(int) = 0

// Any, All, Count
bool hasNegative = numbers.Any(n => n < 0);     // false
bool allPositive = numbers.All(n => n > 0);     // true
int countEven = numbers.Count(n => n % 2 == 0); // 5

// Sum, Min, Max, Average
double avg = numbers.Average();
int total = numbers.Sum();

// Take, Skip — pagination
var firstThree = numbers.Take(3);       // [1, 2, 3]
var skipTwo = numbers.Skip(2);          // [3, 4, 5, ...]
var page2 = numbers.Skip(3).Take(3);   // [4, 5, 6]

// Distinct — loại bỏ duplicate
var withDups = new List<int> { 1, 1, 2, 3, 3 };
var unique = withDups.Distinct();       // [1, 2, 3]

// ToList, ToArray — convert IEnumerable
var list = numbers.Where(n => n > 5).ToList();
var arr = numbers.Select(n => n * 2).ToArray();
```

---

## 3. LINQ với Objects

```csharp
public record Student(string Name, int Age, string Major, double GPA);

var students = new List<Student> {
    new("Duy", 23, "IT", 3.8),
    new("Nam", 22, "Business", 3.2),
    new("Linh", 24, "IT", 3.9),
    new("Mai", 21, "Business", 3.5),
    new("Tuan", 23, "IT", 3.1),
};

// Lọc IT students có GPA > 3.5, sắp xếp theo GPA giảm dần
var topIT = students
    .Where(s => s.Major == "IT" && s.GPA > 3.5)
    .OrderByDescending(s => s.GPA)
    .Select(s => new { s.Name, s.GPA })    // anonymous type (projection)
    .ToList();

// GroupBy — nhóm theo Major
var byMajor = students
    .GroupBy(s => s.Major)
    .Select(g => new {
        Major = g.Key,
        Count = g.Count(),
        AvgGPA = g.Average(s => s.GPA)
    });

foreach (var group in byMajor) {
    Console.WriteLine($"{group.Major}: {group.Count} sinh viên, GPA tb: {group.AvgGPA:F2}");
}

// Join — kết hợp 2 collections
var courses = new List<(string Major, string CourseName)> {
    ("IT", "Algorithms"),
    ("IT", "Database"),
    ("Business", "Economics"),
};

var studentCourses = students
    .Join(courses,
        s => s.Major,           // key từ students
        c => c.Major,           // key từ courses
        (s, c) => new { s.Name, c.CourseName });  // result
```

---

## 4. LINQ Query Syntax (SQL-like)

```csharp
// Method syntax (trên) vs Query syntax (dưới) — cùng kết quả

// Method syntax
var result1 = students
    .Where(s => s.GPA > 3.5)
    .OrderBy(s => s.Name)
    .Select(s => s.Name);

// Query syntax — đọc dễ hơn với người quen SQL
var result2 = from s in students
              where s.GPA > 3.5
              orderby s.Name
              select s.Name;

// Cả 2 đều trả về IEnumerable<string>
```

---

## 5. Deferred Execution — Quan trọng!

LINQ **không chạy ngay** — chỉ chạy khi bạn iterate hoặc gọi ToList/ToArray:

```csharp
var query = numbers.Where(n => n > 5);    // CHƯA chạy
// ... thêm số vào numbers ...
numbers.Add(100);

var result = query.ToList();    // CHỈ ĐẾN ĐÂY mới chạy — và có 100!

// Để chạy ngay, gọi ToList() ngay lập tức:
var snapshot = numbers.Where(n => n > 5).ToList();
```

---

## 📚 Tiếp theo

→ Đọc `04-async-advanced.md` trước **Project 04**  
→ Bắt đầu **Project 03** ngay!
