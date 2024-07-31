using BlogProject.Entites;

namespace BlogProject
{
    public class SharePostViewModel
    {
        public User User { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public List<Tag> AllTags { get; set; }
        public Post newPost { get; set; } = new Post();
    }
}