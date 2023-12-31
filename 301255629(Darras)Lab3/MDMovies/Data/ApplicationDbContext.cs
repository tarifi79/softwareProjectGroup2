﻿using MDMovies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaygroupConnect.Models;
using System.Reflection.Emit;

namespace MDMovies.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Favorite> Favorites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, 
                    Owner = "mdarras@my.centennialcollege.ca", 
                    Title = "Title1",
                    Content ="POST1",
                    Category ="Playing",
                    DateAdded=DateTime.Now,
                    Reported=false,
                    AgeRange="1-3",
                    Cost=0,
                    ImagePath="",
                    NumberofRatings = 0,
                    SumOfRating = 0
                },
                new Post
                {
                    Id = 2,
                    Title="Title2",
                    Owner = "tarifi79@gmail.com",
                    Category = "Camping",
                    Content="POST2",
                    DateAdded = DateTime.Now,
                    Reported = false,
                    AgeRange="3-5",
                    Cost = 99.99,
                    ImagePath = "",
                    NumberofRatings = 0,
                    SumOfRating=0

                }

                );

        }
	}
}
