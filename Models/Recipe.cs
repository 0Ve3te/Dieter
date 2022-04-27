using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dieter.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole {0} przepisu jest wymagane.")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Pole {0} do zdjęcia jest wymagane.")]
        [Display(Name = "Link do zdjęcia")]
        public string ImgUrl { get; set; } = "";
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Opis przygotowania")]
        [MinLength(10)]
        public string Description { get; set; } = "";
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Wybierz jedną lub więcej kategorii")]
        [NotMapped]
        public List<int> CategoriesIds { get; set; } = new List<int>();
        [NotMapped]
        public string Ammounts { get; set; } = "";

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<CategoryRecipe> Categories { get; set; } = new List<CategoryRecipe>();

    }
}
