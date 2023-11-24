using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasmDemo.Shared
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required]
        [Birthdate(MaxAge = 100, MinAge = 2, ErrorMessage = "Must be older than 2 and younger than 100")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Shirt size is required")]
        public int ShirtSizeId { get; set; }

        [Required(ErrorMessage = "Please provide a self description"), MaxLength(100, ErrorMessage = "Must be less than 100 characters")]
        public string? SelfDescription { get; set; }

        public string? FavoriteFood { get; set; }
    }
}
