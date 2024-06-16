using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface ITagRepository
    {
        void Add(Tag p);
        void Update(Tag p);
        void Delete(Tag p);
        List<Tag> GetTags();
        Tag? GetTagById(int tagId);
    }
}
