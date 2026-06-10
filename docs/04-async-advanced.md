# 📘 Async/Await & Nâng cao

---

## 1. Async/Await cơ bản

```csharp
// Synchronous — chặn thread, chờ xong mới tiếp
string content = File.ReadAllText("file.txt");   // block!

// Asynchronous — không chặn, tiếp tục làm việc khác
string content = await File.ReadAllTextAsync("file.txt");   // không block!

// Khai báo async method
public async Task<string> ReadFileAsync(string path) {
    string content = await File.ReadAllTextAsync(path);
    return content.ToUpper();
}

// async void — chỉ dùng cho event handler, không dùng cho API
public async void OnButtonClick() {
    await DoSomethingAsync();
}

// Gọi async method
var result = await ReadFileAsync("data.txt");
```

---

## 2. Task

```csharp
// Task — đại diện cho một công việc bất đồng bộ (không có giá trị trả về)
public async Task SaveDataAsync(string data) {
    await File.WriteAllTextAsync("output.txt", data);
}

// Task<T> — có giá trị trả về
public async Task<int> GetCountAsync() {
    var data = await File.ReadAllLinesAsync("data.txt");
    return data.Length;
}

// Chạy nhiều task song song
var task1 = ReadFileAsync("file1.txt");
var task2 = ReadFileAsync("file2.txt");
var task3 = ReadFileAsync("file3.txt");

// Đợi tất cả xong — chạy song song!
string[] results = await Task.WhenAll(task1, task2, task3);

// Đợi cái nào xong trước
var fastest = await Task.WhenAny(task1, task2, task3);
```

---

## 3. Exception Handling trong Async

```csharp
public async Task ProcessFileAsync(string path) {
    try {
        var content = await File.ReadAllTextAsync(path);
        Console.WriteLine(content);
    } catch (FileNotFoundException ex) {
        Console.WriteLine($"File không tồn tại: {ex.FileName}");
    } catch (IOException ex) {
        Console.WriteLine($"Lỗi đọc file: {ex.Message}");
    }
}
```

---

## 4. File I/O

```csharp
// Đọc file
string text = await File.ReadAllTextAsync("file.txt");
string[] lines = await File.ReadAllLinesAsync("file.txt");
byte[] bytes = await File.ReadAllBytesAsync("image.png");

// Ghi file
await File.WriteAllTextAsync("out.txt", "Hello");
await File.WriteAllLinesAsync("out.txt", new[] { "line1", "line2" });
await File.AppendAllTextAsync("log.txt", "New log entry\n");

// Kiểm tra tồn tại
File.Exists("file.txt");
Directory.Exists("folder/");

// StreamReader — đọc từng dòng (file lớn)
using var reader = new StreamReader("large.txt");
while (!reader.EndOfStream) {
    string? line = await reader.ReadLineAsync();
    Console.WriteLine(line);
}

// StreamWriter
using var writer = new StreamWriter("output.txt");
await writer.WriteLineAsync("Hello");
await writer.WriteLineAsync("World");
// using tự động Dispose/Close khi ra khỏi scope
```

---

## 5. JSON (rất quan trọng cho Web API)

```csharp
// Cài package: dotnet add package System.Text.Json (built-in .NET 6+)
using System.Text.Json;

public record Product(int Id, string Name, decimal Price);

// Serialize (object → JSON string)
var product = new Product(1, "Laptop", 25_000_000);
string json = JsonSerializer.Serialize(product);
// {"Id":1,"Name":"Laptop","Price":25000000}

// Với options đẹp hơn
var options = new JsonSerializerOptions { WriteIndented = true };
string prettyJson = JsonSerializer.Serialize(product, options);

// Deserialize (JSON string → object)
string jsonInput = """{"Id":2,"Name":"Phone","Price":10000000}""";
var p = JsonSerializer.Deserialize<Product>(jsonInput);

// Đọc/ghi JSON file
await File.WriteAllTextAsync("products.json", json);
string fileContent = await File.ReadAllTextAsync("products.json");
var fromFile = JsonSerializer.Deserialize<Product>(fileContent);
```

---

## 6. using statement & IDisposable

```csharp
// C# có pattern để tự động giải phóng tài nguyên
// Giống Java's try-with-resources

// Cách cũ
using (var reader = new StreamReader("file.txt")) {
    // reader tự Dispose khi ra khỏi block này
}

// Cách mới (C# 8+) — gọn hơn
using var reader = new StreamReader("file.txt");
// reader Dispose khi ra khỏi method/scope
```

---

## 📚 Tiếp theo

→ Đọc `05-aspnet-webapi.md` trước **Project 05**
