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
        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        [Required]
        public int ShirtSizeId { get; set; }

        [Required, MaxLength(100)]
        public string? SelfDescription { get; set; }

        public string? FavoriteFood { get; set; }
    }
}
