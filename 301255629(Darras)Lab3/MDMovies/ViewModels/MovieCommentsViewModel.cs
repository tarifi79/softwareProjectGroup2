using MDMovies.Models;

namespace MDMovies.ViewModels
{
    public class MovieCommentsViewModel
    {
        public Movie Movie { get; set; }
        public Comment NewComment { get; set; }
        public List<Comment> Comments { get; set; }

        public bool UserHasCommented { get; set; }
        public Comment CurrentUserComment { get; set; } // To hold the current user's comment

        public double AverageRating { get; set; }


    }
}
