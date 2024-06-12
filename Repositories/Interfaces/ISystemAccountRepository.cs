using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        void AddAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        SystemAccount GetAccountById(short accountID);
        SystemAccount? GetAccountByEmailAndPass(string email, string password);
        List<SystemAccount> GetAccounts();
    }
}
