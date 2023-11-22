using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3;
using MDMovies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Amazon.S3.Model;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using MDMovies.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using MDMovies.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaygroupConnect.Models;
using PlaygroupConnect.ViewModels;
using System.Security.Claims;

namespace MDMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;


        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;




        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext db )
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }



        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID
            var posts = _db.Posts.ToList();

            // Get the IDs of the user's favorite posts
            var favoritePostIds = _db.Favorites
                                     .Where(f => f.UserId == userId)
                                     .Select(f => f.PostId)
                                     .ToList();

            ViewBag.FavoritePostIds = favoritePostIds; // Pass this list to the view

            return View(posts);
        }


        public IActionResult AddActivity()
        {
            // Prepare AgeRange list
            ViewBag.AgeRange = new List<SelectListItem>
            {
                new SelectListItem { Text = "All Ages", Value = "All Ages"},
                new SelectListItem { Text = "Under 1", Value = "Under 1"},
                new SelectListItem { Text = "1-3", Value = "1-3"},
                new SelectListItem { Text = "3-5", Value = "3-5"},
                new SelectListItem { Text = "5-7", Value = "5-7"},
                new SelectListItem { Text = "8-10", Value = "8-10"},
                new SelectListItem { Text = "Over 10", Value = "Over 10"}
            };

            // Prepare Category list with 10 categories
            ViewBag.Category = new List<SelectListItem>
            {
                new SelectListItem { Text = "Early Development", Value = "Early Development"},
                new SelectListItem { Text = "Literacy and Language", Value = "Literacy and Language"},
                new SelectListItem { Text = "Mathematics and Logic", Value = "Mathematics and Logic"},
                new SelectListItem { Text = "Science and Nature", Value = "Science and Nature"},
                new SelectListItem { Text = "Creative Arts", Value = "Creative Arts"},
                new SelectListItem { Text = "Physical Development", Value = "Physical Development"},
                new SelectListItem { Text = "Social and Emotional Learning", Value = "Social and Emotional Learning"},
                new SelectListItem { Text = "Cultural and Global Awareness", Value = "Cultural and Global Awareness"},
                new SelectListItem { Text = "Technology and Digital Learning", Value = "Technology and Digital Learning"},
                new SelectListItem { Text = "Special Needs Resources", Value = "Special Needs Resources"}
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddActivity2([Bind("Owner,Content,AgeRange,Category,DateAdded,Reported,Cost")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.DateAdded = DateTime.Now; // Set the date added
               // post.Reported = false; // Default value for reported

                _db.Add(post);
                _db.SaveChanges();
                return RedirectToAction("Index"); // Redirect to Index action after successful save
            }
            return View(post); // If model is invalid, show the form again
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult AddActivity([Bind("Owner,Content,AgeRange,Category,DateAdded,Reported,Cost")] Post post, IFormFile ImageFile)
		{
			if (ModelState.IsValid)
			{
				if (ImageFile != null && ImageFile.Length > 0)
				{
					var fileName = Path.GetFileName(ImageFile.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						ImageFile.CopyTo(stream);
					}
					post.DateAdded = DateTime.Now;
					post.Reported = false;
					post.ImagePath = "/images/" + fileName; // Save the path relative to wwwroot
				}

				_db.Add(post);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(post);
		}



		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var post = _db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }



            _db.Posts.Remove(post);
            _db.SaveChanges(true);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateActivity(int? id, IFormFile ImageFile)
        {
            // Prepare AgeRange list
            ViewBag.AgeRange = new List<SelectListItem>
            {
                new SelectListItem { Text = "All Ages", Value = "All Ages"},
                new SelectListItem { Text = "Under 1", Value = "Under 1"},
                new SelectListItem { Text = "1-3", Value = "1-3"},
                new SelectListItem { Text = "3-5", Value = "3-5"},
                new SelectListItem { Text = "5-7", Value = "5-7"},
                new SelectListItem { Text = "8-10", Value = "8-10"},
                new SelectListItem { Text = "Over 10", Value = "Over 10"}
            };

            // Prepare Category list with 10 categories
            ViewBag.Category = new List<SelectListItem>
            {
                new SelectListItem { Text = "Early Development", Value = "Early Development"},
                new SelectListItem { Text = "Literacy and Language", Value = "Literacy and Language"},
                new SelectListItem { Text = "Mathematics and Logic", Value = "Mathematics and Logic"},
                new SelectListItem { Text = "Science and Nature", Value = "Science and Nature"},
                new SelectListItem { Text = "Creative Arts", Value = "Creative Arts"},
                new SelectListItem { Text = "Physical Development", Value = "Physical Development"},
                new SelectListItem { Text = "Social and Emotional Learning", Value = "Social and Emotional Learning"},
                new SelectListItem { Text = "Cultural and Global Awareness", Value = "Cultural and Global Awareness"},
                new SelectListItem { Text = "Technology and Digital Learning", Value = "Technology and Digital Learning"},
                new SelectListItem { Text = "Special Needs Resources", Value = "Special Needs Resources"}
            };

            if (id == null || id==0)
            {
                return NotFound();
            }
            

            var post = _db.Posts.Find(id);


            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult UpdateActivity2([Bind("Id,Owner,Content,AgeRange,Category,DateAdded,Reported,Cost")] Post post)
        {
            if (ModelState.IsValid)
            {
               

                _db.Update(post);
                _db.SaveChanges();
                return RedirectToAction("Index"); // Redirect to Index action after successful save
            }
            return View(post); // If model is invalid, show the form again
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateActivity(int id, [Bind("Id,Owner,Content,AgeRange,Category,DateAdded,Reported,Cost,ImagePath")] Post post, IFormFile ImageFile)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }
                        post.ImagePath = "/images/" + fileName; // Update the ImagePath
                    }

                    // Update other properties if needed
                    post.DateAdded = DateTime.Now; // Optionally update the date

                    _db.Update(post);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.Posts.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(post);
        }


        public IActionResult Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID

            var post = _db.Posts
                          .Include(p => p.Comments) // Assuming you want to include comments
                          .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var isFavorite = _db.Favorites.Any(f => f.PostId == id && f.UserId == userId);
            ViewBag.IsFavorite = isFavorite; // Pass the favorite status to the view

            return View(post);
        }

        [HttpPost]
        public IActionResult AddComment(int PostId, string Owner, string Content, int Rating)
        {
            var currentUser = _userManager.GetUserName(User);//Async(User);
            //var ownerName = currentUser.Email;
            var comment = new Comment
            {
                PostId = PostId,
                Owner = currentUser,
                Content = Content,
                Time = DateTime.Now,
                Rating = Rating
            };

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = PostId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToFavorites(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var favorite = new Favorite { UserId = userId, PostId = postId };

            _db.Favorites.Add(favorite);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = postId });
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromFavorites(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorite = _db.Favorites.FirstOrDefault(f => f.UserId == userId && f.PostId == postId);

            if (favorite != null)
            {
                _db.Favorites.Remove(favorite);
                _db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpPost]
        public IActionResult ReportPost(int postId)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Reported = true;
                _db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = postId });
        }


















    }
}