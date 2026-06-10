namespace OopBankApp.Models;

public record Transaction(
    string Type,
    decimal Amount,
    decimal BalanceAfter,
    DateTime CreatedAt
)
{
    public override string ToString() =>
        $"[{CreatedAt:dd/MM/yyyy HH:mm}] {Type,-12} | {Amount,15:C} | Số dư: {BalanceAfter:C}";
}