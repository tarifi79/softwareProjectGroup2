using MDMovies.Models;
using PlaygroupConnect.Models;

namespace PlaygroupConnect.ViewModels
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
