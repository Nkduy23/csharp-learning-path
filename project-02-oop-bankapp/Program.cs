using OopBankApp.Models;
using OopBankApp.Services;

var bank = new BankService();

// Seed dữ liệu mẫu
var savings = new SavingsAccount("SAV001", "Nguyen Duy", 10_000_000, interestRate: 0.06);
var checking = new CheckingAccount("CHK001", "Tran Nam", 5_000_000, overdraftLimit: 2_000_000);
var loan = new LoanAccount("LON001", "Le Linh", loanAmount: 50_000_000, interestRate: 0.08);

bank.AddAccount(savings);
bank.AddAccount(checking);
bank.AddAccount(loan);

while (true)
{
    Console.WriteLine("\n===== 🏦 NGÂN HÀNG =====");
    Console.WriteLine("1. Xem danh sách tài khoản");
    Console.WriteLine("2. Nạp tiền");
    Console.WriteLine("3. Rút tiền");
    Console.WriteLine("4. Chuyển khoản");
    Console.WriteLine("5. Áp dụng lãi suất");
    Console.WriteLine("6. Xem sao kê");
    Console.WriteLine("7. Trả nợ (Loan)");
    Console.WriteLine("0. Thoát");
    Console.Write("👉 Chọn: ");

    switch (Console.ReadLine())
    {
        case "1":
            bank.PrintAll();
            break;

        case "2":
            Console.Write("Số tài khoản: ");
            var acc = bank.Find(Console.ReadLine() ?? "");
            if (acc == null) { Console.WriteLine("❌ Không tìm thấy."); break; }
            Console.Write("Số tiền nạp: ");
            acc.Deposit(decimal.Parse(Console.ReadLine() ?? "0"));
            break;

        case "3":
            Console.Write("Số tài khoản: ");
            var acc3 = bank.Find(Console.ReadLine() ?? "");
            if (acc3 == null) { Console.WriteLine("❌ Không tìm thấy."); break; }
            Console.Write("Số tiền rút: ");
            try { acc3.Withdraw(decimal.Parse(Console.ReadLine() ?? "0")); }
            catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
            break;

        case "4":
            Console.Write("Tài khoản nguồn: ");
            var from = bank.Find(Console.ReadLine() ?? "");
            Console.Write("Tài khoản đích: ");
            var to = bank.Find(Console.ReadLine() ?? "");
            if (from == null || to == null) { Console.WriteLine("❌ Tài khoản không tồn tại."); break; }
            Console.Write("Số tiền: ");
            try { from.TransferTo(to, decimal.Parse(Console.ReadLine() ?? "0")); }
            catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
            break;

        case "5":
            bank.PrintAll();
            Console.Write("Số tài khoản áp dụng lãi: ");
            var accInt = bank.Find(Console.ReadLine() ?? "");
            if (accInt is OopBankApp.Interfaces.IInterestBearing ib)
                ib.ApplyInterest();
            else
                Console.WriteLine("❌ Tài khoản này không hỗ trợ lãi suất.");
            break;

       case "6":
            Console.Write("Số tài khoản: ");
            var acc6 = bank.Find(Console.ReadLine() ?? "");
            if (acc6 == null) Console.WriteLine("❌ Không tìm thấy.");
            else acc6.PrintStatement();
            break;

        case "7":
            Console.Write("Số tài khoản vay: ");
            var loanAcc = bank.Find(Console.ReadLine() ?? "") as LoanAccount;
            if (loanAcc == null) { Console.WriteLine("❌ Không phải tài khoản vay."); break; }
            Console.Write("Số tiền trả: ");
            loanAcc.Repay(decimal.Parse(Console.ReadLine() ?? "0"));
            break;

        case "0":
            Console.WriteLine("👋 Tạm biệt!");
            return;

        default:
            Console.WriteLine("❌ Lựa chọn không hợp lệ.");
            break;
    }
}