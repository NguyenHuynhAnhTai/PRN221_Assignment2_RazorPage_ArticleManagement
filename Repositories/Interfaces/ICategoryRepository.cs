using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void Add(Category p);
        void Update(Category p);
        void Delete(Category p);
    }
}
