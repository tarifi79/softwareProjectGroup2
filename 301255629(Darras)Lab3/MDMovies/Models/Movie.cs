

namespace MDMovies.Models
{
    public class Movie
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Owner { get; set; }

        public string Description { get; set; }
        public string Directors { get; set; }
        public List<string> DirectorsList { get; set; }
        public string Stars { get; set; }
        public List<string> StarsList { get; set; }
        public DateTime ReleaseTime { get; set; }
        public int RunTime { get; set; }
        public string S3URL { get; set; }
        public double AverageRating {  get; set; }


        public IFormFile MovieFile { get; set; }

    }

}
