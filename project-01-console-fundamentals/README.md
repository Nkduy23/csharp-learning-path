# 🟢 Project 01 — Console Fundamentals

**Độ khó:** ⭐ (Cơ bản)  
**Chủ đề:** Syntax, Variables, Control Flow, Methods, Collections, File I/O

---

## 🎯 Mục tiêu

Xây dựng **CLI Student Grade Manager** — ứng dụng console quản lý điểm sinh viên.

---

## 📋 Chức năng cần làm

- [ ] Thêm sinh viên (tên, tuổi, điểm các môn)
- [ ] Hiển thị danh sách sinh viên
- [ ] Tính điểm trung bình từng sinh viên
- [ ] Xếp loại học lực (Xuất sắc / Giỏi / Khá / Trung bình / Yếu)
- [ ] Tìm kiếm sinh viên theo tên
- [ ] Sắp xếp theo điểm trung bình (cao → thấp)
- [ ] Lưu/đọc dữ liệu từ file `.txt`

---

## 🏗️ Cấu trúc project

```
project-01-console-fundamentals/
├── project-01-console-fundamentals.csproj
├── Program.cs          ← entry point, menu loop
├── Models/
│   └── Student.cs      ← class Student
├── Services/
│   └── StudentService.cs   ← business logic
└── Utils/
    └── FileHelper.cs   ← đọc/ghi file
```

---

## 🚀 Khởi tạo project

```bash
cd project-01-console-fundamentals
dotnet new console
dotnet run
```

---

## 💡 Kiến thức áp dụng

- `string`, `int`, `double`, `bool`, `List<T>`, `Dictionary`
- `if/else`, `switch`, `for`, `foreach`, `while`
- Methods, optional parameters
- String interpolation `$"..."`
- `File.ReadAllText`, `File.WriteAllText`
- `Console.ReadLine()`, `Console.WriteLine()`

---

## 📖 Doc cần đọc trước

→ [`docs/01-csharp-core-syntax.md`](../docs/01-csharp-core-syntax.md)

---

## ✅ Hoàn thành khi...

App chạy được menu loop, CRUD sinh viên, lưu/đọc file không bị lỗi.
