using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{

    public class PastDOB : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Type cast 'value' into a DateTime:
            DateTime InputDate = Convert.ToDateTime(value);

            if (InputDate < DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date of birth must be in the past.");
            }
        }
    }

    public class IsOlderThan18 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Type cast 'value' into a DateTime:
            DateTime InputDate = Convert.ToDateTime(value);

            if (InputDate <= DateTime.Now.AddYears(-18))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Chef must be at least 18 years old.");
            }
        }
    }

    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [PastDOB]
        [IsOlderThan18]
        public DateTime DateOfBirth { get; set; }

        public List<Dish> CreatedDishes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}

