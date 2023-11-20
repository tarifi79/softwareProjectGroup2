using MDMovies.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaygroupConnect.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Content { get; set; }
        public  string AgeRange { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Reported { get; set; } = false;
        public double Cost { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
        public double Rate { get; set; } = 0;

        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }

		[NotMapped, ValidateNever] // This attribute prevents the ImageFile property from being mapped to the database.
		public IFormFile ImageFile { get; set; }

	}
}
