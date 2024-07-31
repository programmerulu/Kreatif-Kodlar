using System.ComponentModel.DataAnnotations;

namespace BlogProject.Entites
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public string? About { get; set; } = "Herkese merhaba, makalelerimi okumaktan keyif almanızı umarım.";
        public string? Code { get; set; }= Guid.NewGuid().ToString().Substring(0,5);

        public List<Comment>? Comments { get; set; } = new List<Comment>();
        public List<Post>? Posts { get; set; } = new List<Post>();
    }
}
