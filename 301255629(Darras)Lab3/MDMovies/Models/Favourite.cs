using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaygroupConnect.Models
{
    public class Favorite
    {
        //Model class for Favorites

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } // Assuming you're using ASP.NET Identity
        public int PostId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual Post Post { get; set; }
    }

}
