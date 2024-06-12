using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository iCategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            iCategoryRepository = categoryRepository;
        }

        public List<Category> GetCategories() 
        {
            return iCategoryRepository.GetCategories(); 
        }

        public void Add(Category p)
        {
            iCategoryRepository.Add(p);
        }

        public void Update(Category p)
        {
            iCategoryRepository.Update(p);
        }

        public void Delete(Category p)
        {
            iCategoryRepository.Delete(p);
        }
    }
}
