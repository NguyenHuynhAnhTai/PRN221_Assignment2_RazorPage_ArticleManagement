using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface INewsArticleRepository
    {
        void SaveNewsArticle(NewsArticle p);
        void UpdateNewsArticle(NewsArticle p);
        void DeleteNewsArticle(NewsArticle p);
        List<NewsArticle> GetNewsArticles();
        NewsArticle? GetNewsArticleById(string id);
    }
}
