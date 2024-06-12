using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository iAccountRepository;

        public SystemAccountService(ISystemAccountRepository accountRepository)
        {
            iAccountRepository = accountRepository;
        }

        public List<SystemAccount> GetAccounts()
        {
            return iAccountRepository.GetAccounts();
        }

        public SystemAccount? GetAccountByEmailAndPass(string email, string password)
        {
            return iAccountRepository.GetAccountByEmailAndPass(email, password);
        }

        public SystemAccount GetAccountById(short accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }

        public void UpdateAccount(SystemAccount account)
        {
            iAccountRepository.UpdateAccount(account);
        }
        public void DeleteAccount(SystemAccount account) 
        {
            iAccountRepository.DeleteAccount(account);
        }

        public void AddAccount(SystemAccount account)
        {
            iAccountRepository.AddAccount(account);
        }
    }
}
