# 📘 C# Core Syntax — Kiến thức cốt lõi

> So sánh với Java để bạn nắm nhanh hơn

---

## 1. Cấu trúc chương trình

### Java
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello");
    }
}
```

### C# (truyền thống)
```csharp
namespace MyApp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello");
        }
    }
}
```

### C# (Top-level statements — .NET 6+) ← Cách viết hiện đại
```csharp
Console.WriteLine("Hello, World!");
// Không cần class, không cần Main() — viết thẳng vào!
```

---

## 2. Kiểu dữ liệu

| Java | C# | Ghi chú |
|------|----|---------|
| `int` | `int` | Giống nhau |
| `long` | `long` | Giống nhau |
| `double` | `double` | Giống nhau |
| `boolean` | `bool` | Tên khác |
| `String` | `string` | Lowercase trong C# |
| `char` | `char` | Giống nhau |
| `void` | `void` | Giống nhau |
| `var` (Java 10+) | `var` | Giống nhau — type inference |

```csharp
int age = 25;
double salary = 15_000_000.0;   // _ để đọc dễ hơn
bool isActive = true;
string name = "Duy";
var city = "Ho Chi Minh";       // compiler tự suy ra kiểu string

// C# có thêm: decimal (chính xác cao, dùng cho tiền)
decimal price = 99.99m;         // hậu tố 'm' cho decimal
```

---

## 3. Nullable Types

C# có tính năng đặc biệt hơn Java — Nullable value types:

```csharp
int? age = null;          // int thường không thể null, nhưng int? thì được
string? name = null;      // Reference types (string) mặc định nullable

// Null-coalescing operator
string display = name ?? "Không có tên";    // giống: name != null ? name : "..."

// Null-conditional operator  
int? length = name?.Length;    // nếu name là null, trả về null thay vì throw exception
```

---

## 4. String

```csharp
string name = "Duy";
int age = 23;

// String Interpolation (C# đặc trưng — rất tiện)
string msg = $"Tên: {name}, Tuổi: {age}";          // ← dùng $ prefix
string calc = $"Năm sau tôi {age + 1} tuổi";       // có thể tính toán trong {}

// Verbatim string (raw string — giống Python r"...")
string path = @"C:\Users\Duy\Documents";            // không cần escape backslash

// String methods (gần giống Java)
string s = "  Hello World  ";
s.Trim()           // bỏ khoảng trắng 2 đầu
s.ToUpper()        // HELLO WORLD
s.ToLower()        // hello world
s.Contains("World")    // true
s.Replace("World", "C#")
s.Split(" ")       // string[]
s.StartsWith("Hello")
s.Substring(0, 5)  // "Hello"
s.Length           // property, không phải method (không có ())
```

---

## 5. Array & Collections

```csharp
// Array — cố định kích thước
int[] nums = { 1, 2, 3, 4, 5 };
string[] names = new string[3];
names[0] = "Duy";

// List<T> — giống ArrayList của Java
List<string> list = new List<string>();
list.Add("C#");
list.Add("Java");
list.Remove("Java");
list.Count;        // .Count thay vì .size() của Java

// Dictionary<K,V> — giống HashMap của Java
Dictionary<string, int> scores = new Dictionary<string, int>();
scores["Duy"] = 95;
scores["Nam"] = 87;
scores.ContainsKey("Duy");    // true
scores.TryGetValue("Duy", out int val);   // safe get

// Khởi tạo gọn (Collection initializer)
var langs = new List<string> { "C#", "Java", "Python" };
var map = new Dictionary<string, int> {
    { "one", 1 },
    { "two", 2 }
};
```

---

## 6. Control Flow

```csharp
// if/else — giống hệt Java
if (age >= 18) {
    Console.WriteLine("Người lớn");
} else if (age >= 13) {
    Console.WriteLine("Thiếu niên");
} else {
    Console.WriteLine("Trẻ em");
}

// Switch expression (C# 8+ — rất gọn)
string result = age switch {
    >= 18 => "Người lớn",
    >= 13 => "Thiếu niên",
    _     => "Trẻ em"          // _ là default
};

// for, while, foreach — giống Java
for (int i = 0; i < 5; i++) { }
while (condition) { }

foreach (var item in list) {    // giống Java's for-each
    Console.WriteLine(item);
}
```

---

## 7. Methods (Hàm)

```csharp
// Hàm cơ bản
static int Add(int a, int b) {
    return a + b;
}

// Expression body (arrow function style)
static int Multiply(int a, int b) => a * b;

// Optional parameters (Java không có)
static void Greet(string name, string greeting = "Xin chào") {
    Console.WriteLine($"{greeting}, {name}!");
}
Greet("Duy");              // "Xin chào, Duy!"
Greet("Duy", "Hello");    // "Hello, Duy!"

// Named arguments (Java không có)
Greet(greeting: "Hi", name: "Duy");

// out parameter — trả về nhiều giá trị
static void Divide(int a, int b, out int quotient, out int remainder) {
    quotient = a / b;
    remainder = a % b;
}
Divide(10, 3, out int q, out int r);   // q=3, r=1

// params — số lượng argument không cố định (giống Java varargs)
static int Sum(params int[] numbers) {
    return numbers.Sum();
}
Sum(1, 2, 3, 4, 5);   // 15
```

---

## 8. Exception Handling

```csharp
// Cú pháp gần giống Java
try {
    int result = 10 / 0;
} catch (DivideByZeroException ex) {
    Console.WriteLine($"Lỗi: {ex.Message}");
} catch (Exception ex) {           // catch tất cả
    Console.WriteLine($"Lỗi chung: {ex.Message}");
} finally {
    Console.WriteLine("Luôn chạy");
}

// Custom Exception
public class InsufficientFundsException : Exception {
    public decimal Amount { get; }
    
    public InsufficientFundsException(decimal amount) 
        : base($"Không đủ tiền. Thiếu: {amount}") {
        Amount = amount;
    }
}
```

---

## 9. Properties (Đặc trưng C#)

Java dùng getter/setter method. C# có **Properties** — gọn và đẹp hơn nhiều:

```csharp
public class Person {
    // Auto-property (phổ biến nhất)
    public string Name { get; set; }
    public int Age { get; set; }
    
    // Read-only property
    public string FullInfo { get; }    // chỉ get, không set
    
    // Property với validation
    private int _age;
    public int ValidatedAge {
        get => _age;
        set {
            if (value < 0) throw new ArgumentException("Tuổi không âm");
            _age = value;
        }
    }
    
    // Init-only property (C# 9+)
    public string Id { get; init; }    // chỉ set được khi khởi tạo
}

// Sử dụng
var p = new Person { Name = "Duy", Age = 23 };   // Object initializer
Console.WriteLine(p.Name);    // truy cập như field, không cần gọi getName()
```

---

## 10. Lambda & Delegates

```csharp
// Lambda — giống Java lambda
Func<int, int, int> add = (a, b) => a + b;
add(3, 4);   // 7

// Action — lambda không trả về giá trị
Action<string> print = name => Console.WriteLine(name);
print("Duy");

// Predicate — lambda trả về bool
Predicate<int> isEven = n => n % 2 == 0;
isEven(4);   // true

// Dùng với collections
var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
var evens = numbers.Where(n => n % 2 == 0).ToList();   // LINQ
var doubled = numbers.Select(n => n * 2).ToList();
```

---

## ⚡ Điểm khác biệt lớn Java vs C#

| Tính năng | Java | C# |
|-----------|------|-----|
| Properties | Getter/Setter methods | `{ get; set; }` |
| String format | `String.format()` | `$"..."` interpolation |
| Nullable | Optional<T> | `int?`, `string?` |
| Switch | switch statement | switch expression |
| Top-level code | Cần class + main | Viết thẳng |
| `var` scope | Java 10+ (hạn chế) | Rộng hơn |
| `decimal` | BigDecimal | `decimal` built-in |
| Extension methods | Không có | Có (rất mạnh) |

---

## 📚 Tiếp theo

→ Đọc `02-oop-concepts.md` trước khi bắt đầu **Project 02**  
→ Bắt đầu code **Project 01** ngay bây giờ!
