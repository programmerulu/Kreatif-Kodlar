using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Entites
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region COMMENT_POST

        public int? PostId { get; set; }
        public Post Post { get; set; }
        #endregion
        #region COMMENT_USER

        public int? UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
