using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface ISystemAccountService
    {
        void AddAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        SystemAccount GetAccountById(short accountID);
        SystemAccount? GetAccountByEmailAndPass(string email, string password);
        List<SystemAccount> GetAccounts();
    }
}
