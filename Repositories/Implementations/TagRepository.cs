using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        public void Add(Tag p) => TagDAO.Add(p);

        public void Update(Tag p) => TagDAO.Update(p);

        public void Delete(Tag p) => TagDAO.Delete(p);

        public Tag? GetTagById(int tagId) => TagDAO.GetTagById(tagId);

        public List<Tag> GetTags() => TagDAO.GetTags();

    }
}
