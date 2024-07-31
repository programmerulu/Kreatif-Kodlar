using BlogProject.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Models
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


    }
}
