using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dieter.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} jest wymagana."), Display(Name = "Nazwa")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Ilość kcal w 1 produkcie lub 100 gramach.")]
        //[RegularExpression(@"\b\d{1,18}\,\d{1,2}\b", ErrorMessage = "Podaj ilość kcal w postaci: xx,xx - np. 87,25")]
        public double Kcal { get; set; } = 0.00;
        [MaxLength(128, ErrorMessage = "Należy wybrać tylko jedną emotkę.")]
        [Display(Name = "Emoji")]
        public string? Emoji { get; set; }
        public virtual Recipe? Recipes { get; set; }
    }
}