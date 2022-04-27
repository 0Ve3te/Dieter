using Dieter.Models;
using Microsoft.EntityFrameworkCore;

namespace Dieter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeProduct> RecipeProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRecipe> CategoryRecipe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Name = "Śniadanie", Id = 1},
                new Category { Name = "Obiad", Id = 2},
                new Category { Name = "Kolacja", Id = 3},
                new Category { Name = "Przekąska", Id = 4},
                new Category { Name = "Niskokaloryczne", Id = 5},
                new Category { Name = "Wysokokaloryczne", Id = 6},
                new Category { Name = "Na słodko", Id = 7},
                new Category { Name = "Na słono", Id = 8 },
                new Category { Name = "Zupa", Id = 9},
                new Category { Name = "Sałatka", Id = 10},
                new Category { Name = "Danie główne", Id = 11},
            });
        }
    }
}
