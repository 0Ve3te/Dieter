using System.ComponentModel.DataAnnotations;

namespace Dieter.Models
{
    public class RecipeProduct
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
        public int Ammount { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual Product? Product { get; set; }
    }
}
