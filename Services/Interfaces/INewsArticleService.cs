using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface INewsArticleService
    {
        void SaveNewsArticle(NewsArticle p);
        void DeleteNewsArticle(NewsArticle p);
        void UpdateNewsArticle(NewsArticle p);
        List<NewsArticle> GetNewsArticles();
        NewsArticle? GetNewsArticleById(string id);
    }
}
