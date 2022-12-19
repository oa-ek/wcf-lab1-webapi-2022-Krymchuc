using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Real_State_Catalog.Core
{
    public class Accommodation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Display(Name="Count of rooms")]
        public int? CountOfRooms { get; set; }
        [Display(Name= "Type of accomodation (house/apartment)")]
        public string? Type { get; set; }
        [Display(Name = "Maximum Travelers")]
        public int MaxTraveler { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Price")]
        public string? ImagePath { get; set; }
        [Display(Name="Adress")]
        public string? Adress { get; set; }
        [Required]
        public string? Price { get; set; }
        public Amenity? Amenity { get; set; }
    }
}