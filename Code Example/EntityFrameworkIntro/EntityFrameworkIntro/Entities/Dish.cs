using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkIntro.Entities
{
    public class Dish
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Note { get; set; }
        public int? Star { get; set; }
        public List<DishIngredient> Gredients { get; set; } = new();
    }

}
