using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface ITagService
    {
        void Add(Tag p);
        void Update(Tag p);
        void Delete(Tag p);
        List<Tag> GetTags();
        Tag? GetTagById(int tagId);
    }
}
