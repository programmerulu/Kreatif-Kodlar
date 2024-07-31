using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Data
{
    public class CommentRepository : IEfRepository<Comment>
    {
        private readonly BlogContext _blogContext;
        public CommentRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task CreateAsync(Comment entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Entity boş");
            }
            if (entity.UserId == null)
            {
                throw new ApplicationException("Üye olmanız gerek");

            }
            var allUsers = await _blogContext.Users.ToListAsync();
            var findedUser = allUsers.FirstOrDefault(u => u.Id == entity.UserId);
            if (findedUser == null)
            {
                throw new ApplicationException("User bulunamadı");

            }
            entity.User = findedUser;
            var allPosts = await _blogContext.Posts.ToListAsync();
            var findedPost = allPosts.FirstOrDefault(p => p.Id == entity.PostId);
            if (findedPost == null)
            {
                throw new ApplicationException("Post bulunamadı");

            }
            entity.Post = findedPost;



            await _blogContext.Comments.AddAsync(entity);
            await _blogContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetById(int receivedId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllCommentsOfPost (int postId)
        {
            return _blogContext.Comments.Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
