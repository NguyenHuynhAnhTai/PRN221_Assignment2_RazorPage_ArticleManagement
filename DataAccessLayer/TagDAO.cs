using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class TagDAO
    {
        public static List<Tag> GetTags()
        {
            var listTags = new List<Tag>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listTags = context.Tags
                    .Include(x => x.NewsArticles)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listTags;
        }

        public static Tag? GetTagById(int tagId)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                Tag? tag = context.Tags.Find(tagId);
                return tag;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Add(Tag p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Tags.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Update(Tag p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<Tag>(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
