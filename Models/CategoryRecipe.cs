using System.ComponentModel.DataAnnotations;

namespace Dieter.Models
{
    public class CategoryRecipe
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }

        public virtual Recipe? Recipe { get; set; }
        public virtual Category? Category { get; set; }
    }
}
