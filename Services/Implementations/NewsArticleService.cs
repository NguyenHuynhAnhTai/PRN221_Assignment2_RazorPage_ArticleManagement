using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository iNewsArticleRepository;

        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            iNewsArticleRepository = newsArticleRepository;
        }

        public void DeleteNewsArticle(NewsArticle p)
        {
            iNewsArticleRepository.DeleteNewsArticle(p);
        }

        public NewsArticle? GetNewsArticleById(string id)
        {
            return iNewsArticleRepository.GetNewsArticleById(id);
        }

        public List<NewsArticle> GetNewsArticles()
        {
            return iNewsArticleRepository.GetNewsArticles();
        }

        public void SaveNewsArticle(NewsArticle p)
        {
            iNewsArticleRepository.SaveNewsArticle(p);
        }

        public void UpdateNewsArticle(NewsArticle p)
        {
            iNewsArticleRepository.UpdateNewsArticle(p);
        }
    }
}
