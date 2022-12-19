using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class RoomImage
    {

        public int Id { get; set; }
        public int RoomId { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}
