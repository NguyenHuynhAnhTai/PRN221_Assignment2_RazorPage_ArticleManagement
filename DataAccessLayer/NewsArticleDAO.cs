using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class NewsArticleDAO
    {
        public static List<NewsArticle> GetNewsArticles()
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var db = new FunewsManagementDbContext();
                listNewsArticles = db.NewsArticles
                                        .Include(a => a.Tags)
                                        .Include(a => a.Category)
                                        .Include(a => a.CreatedBy)
                                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }

        public static NewsArticle? GetNewsArticleById(string id)
        {
            using var db = new FunewsManagementDbContext();
            NewsArticle? article = db.NewsArticles
                                     .Include(a => a.Tags)
                                     .Include(a => a.Category)
                                     .Include(a => a.CreatedBy)
                                     .FirstOrDefault(x => x.NewsArticleId.Equals(id));
            return article;
        }

        public static void SaveNewsArticle(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.NewsArticles.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateNewsArticle(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<NewsArticle>(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteNewsArticle(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                var p1 = db.NewsArticles.SingleOrDefault(x => x.NewsArticleId == p.NewsArticleId);
                db.NewsArticles.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
