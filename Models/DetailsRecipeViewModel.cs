using System.ComponentModel.DataAnnotations.Schema;

namespace Dieter.Models
{
    public class DetailsRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<RecipeProduct> RecipeProducts { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        [NotMapped]
        public double kcalSum { get; set; } = 0;
    }
}
