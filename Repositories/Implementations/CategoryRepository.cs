using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public void Add(Category p) => CategoryDAO.Add(p);
        public void Update(Category p) => CategoryDAO.Update(p);
        public void Delete(Category p) => CategoryDAO.Delete(p);
    }
}
