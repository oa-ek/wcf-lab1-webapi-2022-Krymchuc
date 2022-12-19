using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Shared.Dtos
{
    public class AccommodationCreateDto
    {
        public int? Id { get; set; }

        [StringLength(16), MinLength(4)]
        [Required(ErrorMessage = "You must enter a name for your accommodation")]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required]
        public int? CountOfRooms { get; set; }
        [Required]
        public string? Type { get; set; }

        [Required(ErrorMessage = "You must specify the maximum number of travelers")]
        [Display(Name = "Maximum Travelers")]
        public int MaxTraveler { get; set; }

        [Required(ErrorMessage = "You must enter the description of your accommodation")]
        public string? Description { get; set; }
        [Required]
        public string? ImagePath { get; set; }
        public string? Amenity {get; set; }
        [Required]
        public string? Adress { get; set; }
        [Required]
        public string? Price { get; set; }
    }
}
