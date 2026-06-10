# 📘 OOP trong C# — Concepts

> Bạn đã biết OOP từ Java — đây là cách C# thể hiện nó

---

## 1. Class & Object

```csharp
public class BankAccount {
    // Fields (private — encapsulation)
    private decimal _balance;
    private string _accountNumber;

    // Properties (C# style — thay thế getter/setter)
    public string Owner { get; set; }
    public decimal Balance => _balance;          // read-only property
    public string AccountNumber => _accountNumber;

    // Constructor
    public BankAccount(string owner, string accountNumber, decimal initialBalance = 0) {
        Owner = owner;
        _accountNumber = accountNumber;
        _balance = initialBalance;
    }

    // Methods
    public void Deposit(decimal amount) {
        if (amount <= 0) throw new ArgumentException("Số tiền phải dương");
        _balance += amount;
        Console.WriteLine($"Nạp {amount:C} — Số dư: {_balance:C}");
    }

    public void Withdraw(decimal amount) {
        if (amount > _balance) throw new InvalidOperationException("Không đủ tiền");
        _balance -= amount;
    }

    // Override ToString (giống Java)
    public override string ToString() =>
        $"[{_accountNumber}] {Owner} — Số dư: {_balance:C}";
}

// Sử dụng
var acc = new BankAccount("Duy", "ACC001", 1_000_000);
acc.Deposit(500_000);
Console.WriteLine(acc);
```

---

## 2. Inheritance (Kế thừa)

```csharp
// Base class
public class Animal {
    public string Name { get; set; }
    
    public Animal(string name) { Name = name; }
    
    public virtual void Speak() {          // virtual — cho phép override
        Console.WriteLine($"{Name} nói gì đó...");
    }
    
    public void Breathe() {                // không virtual — không override được
        Console.WriteLine($"{Name} đang thở");
    }
}

// Derived class
public class Dog : Animal {                // Java: extends Animal
    public string Breed { get; set; }
    
    public Dog(string name, string breed) : base(name) {   // gọi constructor cha
        Breed = breed;
    }
    
    public override void Speak() {         // override
        Console.WriteLine($"{Name} sủa: Gâu Gâu!");
    }
}

public class Cat : Animal {
    public Cat(string name) : base(name) { }
    
    public override void Speak() {
        Console.WriteLine($"{Name} kêu: Meo~");
    }
}

// Polymorphism
List<Animal> animals = new List<Animal> {
    new Dog("Rex", "Husky"),
    new Cat("Mimi"),
    new Dog("Buddy", "Labrador")
};

foreach (var animal in animals) {
    animal.Speak();    // gọi đúng method của từng class con
}
```

---

## 3. Abstract Class

```csharp
public abstract class Shape {              // Java: abstract class Shape
    public string Color { get; set; }
    
    // Abstract method — bắt buộc class con phải implement
    public abstract double Area();         // Java: abstract double area();
    public abstract double Perimeter();
    
    // Non-abstract method — có thể dùng luôn
    public void DisplayInfo() {
        Console.WriteLine($"{GetType().Name} — Diện tích: {Area():F2}, Chu vi: {Perimeter():F2}");
    }
}

public class Circle : Shape {
    public double Radius { get; set; }
    
    public Circle(double radius) { Radius = radius; }
    
    public override double Area() => Math.PI * Radius * Radius;
    public override double Perimeter() => 2 * Math.PI * Radius;
}

public class Rectangle : Shape {
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height) {
        Width = width; Height = height;
    }
    
    public override double Area() => Width * Height;
    public override double Perimeter() => 2 * (Width + Height);
}
```

---

## 4. Interface

```csharp
// Interface trong C# — giống Java nhưng mạnh hơn
public interface IPayable {                // Convention: tên bắt đầu bằng I
    decimal CalculatePay();
    void ProcessPayment();
    
    // Default implementation (C# 8+) — Java 8+ cũng có
    void PrintReceipt() {
        Console.WriteLine($"Thanh toán: {CalculatePay():C}");
    }
}

public interface ILoggable {
    void Log(string message);
}

// Implement nhiều interface (Java cũng vậy)
public class Employee : IPayable, ILoggable {
    public string Name { get; set; }
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
    
    public decimal CalculatePay() => HourlyRate * HoursWorked;
    
    public void ProcessPayment() {
        Console.WriteLine($"Trả lương {Name}: {CalculatePay():C}");
    }
    
    public void Log(string message) {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}
```

---

## 5. Record (C# 9+ — Không có trong Java)

Record là class đặc biệt — **immutable by default**, tự generate Equals, GetHashCode, ToString:

```csharp
// Thay vì viết class với đủ properties, constructor, Equals...
public record Person(string FirstName, string LastName, int Age);

// Sử dụng
var p1 = new Person("Nguyen", "Duy", 23);
var p2 = new Person("Nguyen", "Duy", 23);
Console.WriteLine(p1 == p2);          // true! (so sánh theo giá trị)
Console.WriteLine(p1);               // Person { FirstName = Nguyen, LastName = Duy, Age = 23 }

// "Sửa" một field — tạo bản copy mới (with expression)
var p3 = p1 with { Age = 24 };       // p1 không thay đổi
```

---

## 6. Static Class & Methods

```csharp
public static class MathHelper {       // không thể tạo instance
    public static double CircleArea(double r) => Math.PI * r * r;
    public static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
}

// Sử dụng
double area = MathHelper.CircleArea(5);
int fact = MathHelper.Factorial(5);
```

---

## 7. Extension Methods (Đặc trưng C# — Java không có)

Thêm method vào class có sẵn mà **không cần sửa class đó**:

```csharp
public static class StringExtensions {
    // 'this string str' — extend kiểu string
    public static bool IsValidEmail(this string str) {
        return str.Contains("@") && str.Contains(".");
    }
    
    public static string Capitalize(this string str) {
        if (string.IsNullOrEmpty(str)) return str;
        return char.ToUpper(str[0]) + str[1..];
    }
}

// Sử dụng — gọi như method của string!
string email = "duy@example.com";
email.IsValidEmail();    // true

string name = "duy";
name.Capitalize();       // "Duy"
```

---

## 8. Generics

```csharp
// Giống Java Generics
public class Stack<T> {
    private List<T> _items = new();
    
    public void Push(T item) => _items.Add(item);
    
    public T Pop() {
        if (_items.Count == 0) throw new InvalidOperationException("Stack rỗng");
        var item = _items[^1];    // [^1] = last element (C# index from end)
        _items.RemoveAt(_items.Count - 1);
        return item;
    }
    
    public T Peek() => _items[^1];
    public int Count => _items.Count;
    public bool IsEmpty => _items.Count == 0;
}

// Generic với constraint
public class Repository<T> where T : class, new() {    // T phải là class và có constructor
    private List<T> _data = new();
    public void Add(T item) => _data.Add(item);
    public List<T> GetAll() => _data;
}
```

---

## 📚 Tiếp theo

→ Đọc `03-collections-linq.md` trước khi bắt đầu **Project 03**  
→ Bắt đầu code **Project 02 — OOP Bank App** ngay!
