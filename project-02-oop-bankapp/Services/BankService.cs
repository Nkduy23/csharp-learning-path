using OopBankApp.Models;

namespace OopBankApp.Services;

public class BankService
{
    private readonly List<BankAccountBase> _accounts = new();

    public void AddAccount(BankAccountBase account)
    {
        _accounts.Add(account);
        Console.WriteLine($"✅ Mở tài khoản [{account.AccountNumber}] cho {account.Owner}");
    }

    public BankAccountBase? Find(string accountNumber) =>
        _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

    public void PrintAll()
    {
        Console.WriteLine("\n===== Danh sách tài khoản =====");
        if (_accounts.Count == 0) { Console.WriteLine("Chưa có tài khoản."); return; }
        _accounts.ForEach(a => Console.WriteLine(a));
    }
}