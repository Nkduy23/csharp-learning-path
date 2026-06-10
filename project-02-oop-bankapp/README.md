# 🔵 Project 02 — OOP Bank App

**Độ khó:** ⭐⭐  
**Chủ đề:** Class, Interface, Inheritance, Polymorphism, Abstract, Generics

---

## 🎯 Mục tiêu

Xây dựng **Banking System** mô phỏng hệ thống ngân hàng với nhiều loại tài khoản.

---

## 📋 Chức năng cần làm

- [ ] Tài khoản cơ bản (SavingsAccount, CheckingAccount, LoanAccount)
- [ ] Nạp tiền / Rút tiền / Chuyển khoản
- [ ] Tính lãi suất theo loại tài khoản (override)
- [ ] Lịch sử giao dịch (Transaction log)
- [ ] Interface `ITransferable`, `IInterestBearing`
- [ ] In sao kê tài khoản

---

## 🏗️ Cấu trúc gợi ý

```
project-02-oop-bankapp/
├── Models/
│   ├── BankAccount.cs        ← abstract base class
│   ├── SavingsAccount.cs     ← : BankAccount
│   ├── CheckingAccount.cs    ← : BankAccount
│   └── Transaction.cs        ← record Transaction(...)
├── Interfaces/
│   ├── ITransferable.cs
│   └── IInterestBearing.cs
├── Services/
│   └── BankService.cs
└── Program.cs
```

---

## 💡 Kiến thức áp dụng

- `abstract class`, `virtual`, `override`
- `interface` với multiple implementation
- `record` cho Transaction
- Properties `{ get; set; }`, `{ get; init; }`
- `decimal` cho tiền tệ
- Generic `Stack<Transaction>` cho lịch sử

---

## 📖 Doc cần đọc trước

→ [`docs/02-oop-concepts.md`](../docs/02-oop-concepts.md)
