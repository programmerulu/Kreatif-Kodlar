using BlogProject.Entites;

namespace BlogProject.Models
{
    public class UpdatePostViewModel
    {
        public Post Post { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public List<Tag> AllTags { get; set; }

    }
}
