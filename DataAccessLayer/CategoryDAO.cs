using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCatagories = new List<Category>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listCatagories = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCatagories;
        }

        public static void Add(Category p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Categories.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(Category p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<Category>(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Delete(Category p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                var p1 = db.Categories.SingleOrDefault(x => x.CategoryId == p.CategoryId);
                db.Categories.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
