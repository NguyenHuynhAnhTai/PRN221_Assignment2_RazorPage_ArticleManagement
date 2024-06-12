using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SystemAccountDAO
    {
        public static List<SystemAccount> GetAccounts()
        {
            var listCatagories = new List<SystemAccount>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listCatagories = context.SystemAccounts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCatagories;
        }

        public static SystemAccount GetAccountById(short accountID)
        {
            using var db = new FunewsManagementDbContext();
            return db.SystemAccounts.FirstOrDefault(x => x.AccountId.Equals(accountID));
        }

        public static SystemAccount? GetAccountByEmailAndPass(string email, string password)
        {
            using var db = new FunewsManagementDbContext();
            return db.SystemAccounts.FirstOrDefault(x => x.AccountEmail.Equals(email) && x.AccountPassword.Equals(password));
        }

        public static void UpdateAccount(SystemAccount p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<SystemAccount>(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Delete(SystemAccount p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                var p1 = db.SystemAccounts.SingleOrDefault(x => x.AccountId == p.AccountId);
                db.SystemAccounts.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Add(SystemAccount p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.SystemAccounts.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
