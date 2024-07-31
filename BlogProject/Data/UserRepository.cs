using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BlogProject.Data
{
    public class UserRepository : IEfRepository<User>
    {
        private readonly BlogContext _blogContext;
        private readonly IWebHostEnvironment _env;
        public UserRepository(BlogContext blogContext, IWebHostEnvironment env)
        {
            _blogContext = blogContext;
            _env = env;
        }
        public async Task CreateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Entity boş");
            }

            await _blogContext.Users.AddAsync(entity);
            await _blogContext.SaveChangesAsync();

        }


        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _blogContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int receivedUserId)
        {
            var user = await _blogContext.Users
                                 .Include(u => u.Posts)
                                 .ThenInclude(p => p.Tags)
                                 .FirstOrDefaultAsync(u => u.Id == receivedUserId);
            return user;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Entity boş");
            }

            _blogContext.Users.Update(entity);
            await _blogContext.SaveChangesAsync();

            return entity;
        }


        #region My Methods


        private async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return filePath;
            }
            return null;
        }

        public async Task CreateUserAsync(User entity, IFormFile profilePicture)
        {
            if (entity == null)
            {
                throw new ApplicationException("Entity boş");
            }
            string filePath = await UploadFileAsync(profilePicture);
            User welcomeUser = new User
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Mail = entity.Mail,
                Password = entity.Password,
                ProfilePicture = filePath,
            };
            await CreateAsync(welcomeUser);
        }

        #endregion
    }
}
