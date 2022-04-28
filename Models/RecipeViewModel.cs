namespace Dieter.Models
{
    public class RecipeViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CategoryRecipe> CategoriesRecipes { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
