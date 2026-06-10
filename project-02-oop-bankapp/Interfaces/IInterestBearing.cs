namespace OopBankApp.Interfaces;

public interface IInterestBearing
{
    double InterestRate { get; }
    decimal CalculateInterest();
    void ApplyInterest();
}