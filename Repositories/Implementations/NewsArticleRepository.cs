using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void DeleteNewsArticle(NewsArticle p) => NewsArticleDAO.DeleteNewsArticle(p);
        public void SaveNewsArticle(NewsArticle p) => NewsArticleDAO.SaveNewsArticle(p);
        public void UpdateNewsArticle(NewsArticle p) => NewsArticleDAO.UpdateNewsArticle(p);
        public List<NewsArticle> GetNewsArticles() => NewsArticleDAO.GetNewsArticles();
        public NewsArticle? GetNewsArticleById(string id) => NewsArticleDAO.GetNewsArticleById(id);
    }
}
