using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Entites
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public bool IsPublished { get; set; } = false;

        #region POST_USER


        public int? UserId { get; set; }
        public User User { get; set; }
        #endregion

        public  List<Comment>?  Comments { get; set; } = new List<Comment>();
        public  List<Tag>? Tags { get; set; } = new List<Tag>();

    }
}
