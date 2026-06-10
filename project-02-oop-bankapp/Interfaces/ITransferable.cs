using OopBankApp.Models;

namespace OopBankApp.Interfaces;

public interface ITransferable
{
    void TransferTo(BankAccountBase target, decimal amount);
}