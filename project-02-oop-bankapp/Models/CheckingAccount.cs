namespace OopBankApp.Models;

public class CheckingAccount : BankAccountBase
{
    public decimal OverdraftLimit { get; }

    public CheckingAccount(string accountNumber, string owner, decimal initialBalance = 0, decimal overdraftLimit = 5_000_000)
        : base(accountNumber, owner, initialBalance)
    {
        OverdraftLimit = overdraftLimit;
    }

    // Override — cho phép rút vượt số dư tới giới hạn overdraft
    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Số tiền phải lớn hơn 0.");
        if (amount > _balance + OverdraftLimit)
            throw new InvalidOperationException($"Vượt giới hạn overdraft ({OverdraftLimit:C}).");
        _balance -= amount;
        LogTransaction("Rút tiền", amount);
        Console.WriteLine($"✅ Rút {amount:C}. Số dư: {_balance:C}");
    }
}