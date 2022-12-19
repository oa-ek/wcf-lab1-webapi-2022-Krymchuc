using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Core
{
    public class OfferDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? OfferId { get; set; }

        public int? AccommodationId { get; set; }

        public double? Price { get; set; }

        public virtual Accommodation? Accommodation { get; set; }

        public virtual Offer? Offer { get; set; }
    }
}