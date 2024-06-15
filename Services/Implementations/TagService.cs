using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository iTagRepository;

        public TagService(ITagRepository tagRepository)
        {
            iTagRepository = tagRepository;
        }

        public Tag? GetTagById(int tagId)
        {
            return iTagRepository.GetTagById(tagId);
        }

        public List<Tag> GetTags()
        {
            return iTagRepository.GetTags();
        }

        public void Add(Tag p)
        {
            iTagRepository.Add(p);
        }

        public void Update(Tag p)
        {
            iTagRepository.Update(p);
        }
    }
}
