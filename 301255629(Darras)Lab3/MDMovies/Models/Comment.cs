using PlaygroupConnect.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDMovies.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CommentId { get; set; }   // Corresponds to the partition key "id"
        public string Owner { get; set; }     // Corresponds to the sort key "Owner"
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public double Rating { get; set; }
        public int PostId { get; set; } // Foreign key to Post

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }

}
