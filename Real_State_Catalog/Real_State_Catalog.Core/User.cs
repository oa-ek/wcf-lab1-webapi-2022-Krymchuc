using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Core
{
	public class User
	{
        [PersonalData]
        [Display(Name = "FirstName")]
        public String? FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Name")]
        public String? LastName { get; set; }

        [Display(Name = "Accommodation")]
        public virtual List<Accommodation>? Accommodations { get; set; }

        [Display(Name = "Favorites")]
        public virtual List<Bookmark>? Bookmarks { get; set; }
    }
}
