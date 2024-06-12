using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public List<SystemAccount> GetAccounts() => SystemAccountDAO.GetAccounts();
        public SystemAccount? GetAccountByEmailAndPass(string email, string password) => SystemAccountDAO.GetAccountByEmailAndPass(email, password);

        public SystemAccount GetAccountById(short accountID) => SystemAccountDAO.GetAccountById(accountID);

        public void UpdateAccount(SystemAccount account) => SystemAccountDAO.UpdateAccount(account);
        public void DeleteAccount(SystemAccount account) => SystemAccountDAO.Delete(account);
        public void AddAccount(SystemAccount account) => SystemAccountDAO.Add(account);
    }
}
