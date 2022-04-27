namespace Dieter.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryRecipe> Recipes { get; set; } = new List<CategoryRecipe>();
    }
}
