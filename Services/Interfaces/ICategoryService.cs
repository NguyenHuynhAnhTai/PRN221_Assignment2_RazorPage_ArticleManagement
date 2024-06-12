using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void Add(Category p);
        void Update(Category p);
        void Delete(Category p);
    }
}
