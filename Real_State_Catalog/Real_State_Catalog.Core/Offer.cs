using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Real_State_Catalog.Core
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Ім'я")]
        public string? Name { get; set; }

        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [BindNever]
        public DateTime DateTime { get; set; }

        public List<OfferDetail>? OfferDetails { get; set; }
    }
}
