using OopBankApp.Interfaces;

namespace OopBankApp.Models;

public class SavingsAccount : BankAccountBase, IInterestBearing
{
    public double InterestRate { get; }

    public SavingsAccount(string accountNumber, string owner, decimal initialBalance = 0, double interestRate = 0.05)
        : base(accountNumber, owner, initialBalance)
    {
        InterestRate = interestRate;
    }

    public decimal CalculateInterest() =>
        _balance * (decimal)InterestRate;

    public void ApplyInterest()
    {
        decimal interest = CalculateInterest();
        _balance += interest;
        LogTransaction("Lãi suất", interest);
        Console.WriteLine($"💰 Cộng lãi {interest:C} (rate {InterestRate:P}). Số dư: {_balance:C}");
    }
}