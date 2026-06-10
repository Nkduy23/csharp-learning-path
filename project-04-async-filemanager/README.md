# 🟠 Project 04 — Async File Manager

**Độ khó:** ⭐⭐⭐⭐  
**Chủ đề:** async/await, Task, File I/O, JSON, Exception Handling nâng cao

---

## 🎯 Mục tiêu

Xây dựng **CLI File & Note Manager** — quản lý ghi chú với async I/O và JSON storage.

---

## 📋 Chức năng cần làm

- [ ] Tạo / đọc / sửa / xóa ghi chú (lưu file JSON)
- [ ] Tìm kiếm full-text trong ghi chú (async)
- [ ] Import nhiều file text cùng lúc (`Task.WhenAll`)
- [ ] Export ghi chú ra file `.txt` hoặc `.json`
- [ ] Log mọi thao tác vào file log (async append)
- [ ] Retry mechanism khi đọc/ghi file thất bại
- [ ] Backup tự động (copy file với timestamp)

---

## 💡 Kiến thức áp dụng

- `async Task`, `async Task<T>`, `await`
- `Task.WhenAll`, `Task.WhenAny`
- `StreamReader` / `StreamWriter` với `using`
- `JsonSerializer` (System.Text.Json)
- `try/catch` trong async context
- `CancellationToken` (bonus)

---

## 📖 Doc cần đọc trước

→ [`docs/04-async-advanced.md`](../docs/04-async-advanced.md)
