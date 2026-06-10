using OopBankApp.Interfaces;

namespace OopBankApp.Models;

public class LoanAccount : BankAccountBase, IInterestBearing
{
    public double InterestRate { get; }
    public decimal LoanAmount { get; }

    public LoanAccount(string accountNumber, string owner, decimal loanAmount, double interestRate = 0.08)
        : base(accountNumber, owner, loanAmount)
    {
        LoanAmount = loanAmount;
        InterestRate = interestRate;
    }

    public decimal CalculateInterest() =>
        _balance * (decimal)InterestRate;

    public void ApplyInterest()
    {
        decimal interest = CalculateInterest();
        _balance += interest;
        LogTransaction("Lãi vay", interest);
        Console.WriteLine($"⚠️  Cộng lãi vay {interest:C}. Dư nợ: {_balance:C}");
    }

    // Trả nợ = deposit vào loan account
    public void Repay(decimal amount)
    {
        if (amount > _balance) amount = _balance; // không trả quá số nợ
        _balance -= amount;
        LogTransaction("Trả nợ", amount);
        Console.WriteLine($"✅ Trả nợ {amount:C}. Dư nợ còn: {_balance:C}");
    }
}