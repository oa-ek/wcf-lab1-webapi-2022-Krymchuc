using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Core
{
    public class Bookmark
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public int? OfferId { get; set; }
        public virtual Offer? Offer { get; set; }
    }
}
