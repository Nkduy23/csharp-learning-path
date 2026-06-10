# 🛠️ Setup Môi Trường C#

## 1. Chọn IDE

### ✅ Khuyên dùng: Visual Studio Code (VS Code)
- Nhẹ, miễn phí, bạn đã quen từ web dev
- Cần cài extension thêm

### ✅ Thay thế: JetBrains Rider
- Giống IntelliJ IDEA (bạn đang dùng cho Java)
- Mạnh hơn cho C#, nhưng trả phí (có bản trial 30 ngày)

### ❌ Không cần: Visual Studio (Windows)
- Nặng, chỉ phù hợp Windows, overkill cho việc học

---

## 2. Cài đặt .NET SDK

### Bước 1 — Tải .NET SDK
Truy cập: https://dotnet.microsoft.com/download

**Chọn:** `.NET 8` (LTS — Long Term Support) ← bản ổn định nhất hiện tại

### Bước 2 — Kiểm tra cài đặt
```bash
dotnet --version
# Output mong đợi: 8.x.x

dotnet --info
# Hiển thị thông tin chi tiết SDK
```

---

## 3. Cài VS Code + Extensions

### Extensions bắt buộc:
```
1. C# Dev Kit           (Microsoft)  ← extension chính
2. C# (OmniSharp)       (Microsoft)  ← IntelliSense, debugging
3. .NET Install Tool    (Microsoft)  ← quản lý SDK
```

### Cài nhanh qua terminal:
```bash
code --install-extension ms-dotnettools.csdevkit
code --install-extension ms-dotnettools.csharp
```

---

## 4. Tạo project C# đầu tiên (test thử)

```bash
# Tạo console app
dotnet new console -n HelloCSharp
cd HelloCSharp

# Chạy project
dotnet run
# Output: Hello, World!
```

### Cấu trúc project được tạo ra:
```
HelloCSharp/
├── HelloCSharp.csproj    ← file config (giống pom.xml của Maven)
├── Program.cs            ← file code chính
└── obj/                  ← build artifacts (ignore)
```

---

## 5. Cấu hình VS Code cho C#

### Tạo file `.vscode/settings.json` trong workspace:
```json
{
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "ms-dotnettools.csharp",
  "[csharp]": {
    "editor.tabSize": 4,
    "editor.insertSpaces": true
  },
  "dotnet.defaultSolution": "disable"
}
```

### Phím tắt quan trọng:
| Phím | Chức năng |
|------|-----------|
| `F5` | Debug / Run |
| `Ctrl + Shift + P` | Command Palette |
| `Ctrl + .` | Quick Fix (giống IntelliJ Alt+Enter) |
| `F12` | Go to Definition |
| `Shift + Alt + F` | Format code |

---

## 6. Cấu hình Debug trong VS Code

VS Code sẽ tự tạo `.vscode/launch.json` khi bạn nhấn F5 lần đầu.  
Nếu không tự tạo, tạo file này:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (console)",
      "type": "coreclr",
      "request": "launch",
      "program": "${workspaceFolder}/bin/Debug/net8.0/${workspaceFolderBasename}.dll",
      "args": [],
      "cwd": "${workspaceFolder}",
      "console": "internalConsole",
      "stopAtEntry": false
    }
  ]
}
```

---

## 7. Lệnh dotnet CLI thường dùng

```bash
# Tạo project mới
dotnet new console -n TenProject    # Console app
dotnet new webapi -n TenProject     # Web API (dùng ở project 05)
dotnet new classlib -n TenProject   # Class Library

# Build & Run
dotnet build          # Biên dịch
dotnet run            # Chạy project
dotnet watch run      # Auto-reload khi code thay đổi (giống nodemon)

# Quản lý packages (giống npm/Maven)
dotnet add package Newtonsoft.Json  # Thêm thư viện
dotnet restore                       # Restore packages
dotnet list package                  # Xem packages đã cài

# Test
dotnet test           # Chạy unit tests
```

---

## 8. Git Setup

```bash
# Thêm .gitignore cho C#
dotnet new gitignore   # Tự tạo .gitignore chuẩn cho .NET

# Hoặc thêm thủ công vào .gitignore:
# bin/
# obj/
# .vs/
# *.user
```

---

## ✅ Checklist trước khi bắt đầu

- [ ] Cài .NET 8 SDK (`dotnet --version` chạy được)
- [ ] Cài VS Code + C# Dev Kit extension
- [ ] Tạo thử project `HelloCSharp` và chạy được `dotnet run`
- [ ] Debug F5 hoạt động, thấy được breakpoint
- [ ] Clone repo `csharp-learning-path` về máy

---

## 🔗 Tài nguyên tham khảo

- [Microsoft Learn — C# cho người mới](https://learn.microsoft.com/vi-vn/dotnet/csharp/)
- [.NET Documentation](https://docs.microsoft.com/dotnet)
- [C# vs Java Cheat Sheet](https://www.harding.edu/fmccown/java_csharp_comparison.html)
