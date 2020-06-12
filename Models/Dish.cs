using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public String Name { get; set; }

        [Required]
        [Range(1,10000)]
        [Display(Name = "Number of Calories")]
        public int Calories { get; set; }

        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required]
        public String Description { get; set; }

        [Display(Name = "Chef")]
        public int ChefId { get; set; }
        public Chef Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

