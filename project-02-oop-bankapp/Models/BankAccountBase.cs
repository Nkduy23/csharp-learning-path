using OopBankApp.Interfaces;

namespace OopBankApp.Models;

public abstract class BankAccountBase : ITransferable
{
    public string AccountNumber { get; init; }
    public string Owner { get; set; }
    protected decimal _balance;
    public decimal Balance => _balance;

    private readonly List<Transaction> _transactions = new();
    public IReadOnlyList<Transaction> Transactions => _transactions;

    protected BankAccountBase(string accountNumber, string owner, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        _balance = initialBalance;
    }

    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Số tiền phải lớn hơn 0.");
        _balance += amount;
        LogTransaction("Nạp tiền", amount);
        Console.WriteLine($"✅ Nạp {amount:C} thành công. Số dư: {_balance:C}");
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Số tiền phải lớn hơn 0.");
        if (amount > _balance) throw new InvalidOperationException("Số dư không đủ.");
        _balance -= amount;
        LogTransaction("Rút tiền", amount);
        Console.WriteLine($"✅ Rút {amount:C} thành công. Số dư: {_balance:C}");
    }

    public void TransferTo(BankAccountBase target, decimal amount)
    {
        Withdraw(amount);
        target.Deposit(amount);
        Console.WriteLine($"✅ Chuyển {amount:C} → [{target.AccountNumber}] {target.Owner}");
    }

    protected void LogTransaction(string type, decimal amount)
    {
        _transactions.Add(new Transaction(type, amount, _balance, DateTime.Now));
    }

    public void PrintStatement()
    {
        Console.WriteLine($"\n===== Sao kê [{AccountNumber}] {Owner} =====");
        if (_transactions.Count == 0)
        {
            Console.WriteLine("Chưa có giao dịch nào.");
            return;
        }
        foreach (var t in _transactions)
            Console.WriteLine(t);
        Console.WriteLine($"{"Số dư hiện tại:",-30} {_balance:C}");
    }

    public override string ToString() =>
        $"[{AccountNumber}] {Owner} | {GetType().Name} | Số dư: {_balance:C}";
}