using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BlogProject.Data
{
    public class PostRepository : IEfRepository<Post>
    {
        private readonly BlogContext _blogContext;
        public PostRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task CreateAsync(Post entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Entity boş");
            }

            await _blogContext.Posts.AddAsync(entity);
            await _blogContext.SaveChangesAsync();

        }

        public Task DeleteAsync(Post entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _blogContext.Posts.Include(p => p.Tags).Include(p => p.User).Include(p => p.Comments).ToListAsync();
        }

        public async Task<Post> GetById(int receivedId)
        {
            var post = await _blogContext.Posts.Include(p => p.Tags).Include(p => p.User).Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == receivedId);
            return post;
        }

        public async Task<Post> UpdateAsync(Post entity)
        {
            _blogContext.Posts.Update(entity);
            await _blogContext.SaveChangesAsync();
            return entity;
        }
    }
}
