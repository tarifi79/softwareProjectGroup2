using MDMovies.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlaygroupConnect.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter Title for your Activity")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter Description for your Activity")]
        public string Content { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please add the Age Range for your Activity")]
        public  string AgeRange { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please add the Age Range for your Activity")]
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Reported { get; set; } = false;
        [Range(0,25, ErrorMessage = "The cost must be between 0$ and 25$"), System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Add the Cost to Join your Activity")]
        public double Cost { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
        public int NumberofRatings { get; set; }
        public int SumOfRating { get; set; }

        public double Rate
        {
            get
            {
                // Check to prevent division by zero
                if (NumberofRatings > 0)
                {
                    return Math.Round((double)SumOfRating / NumberofRatings,1);
                }
                else
                {
                    return 0;
                }
            }
        }

        [ValidateNever]
        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped, ValidateNever, System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Pleas add an Image to Support your Activity")] // This attribute prevents the ImageFile property from being mapped to the database.
        public IFormFile ImageFile { get; set; }

	}
}
