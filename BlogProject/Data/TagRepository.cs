using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Data
{
    public class TagRepository : IEfRepository<Tag>
    {
        private readonly BlogContext _blogContext;
        public TagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public Task CreateAsync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _blogContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetById(int receivedId)
        {
            var tag = await _blogContext.Tags.FirstOrDefaultAsync(t => t.Id == receivedId);
            return tag;
        }

        public Task<Tag> UpdateAsync(Tag entity)
        {
            throw new NotImplementedException();
        }
    }
}
