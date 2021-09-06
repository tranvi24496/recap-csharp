using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiyFrameworkRelations.Models
{
    public class BrickAvailability
    {
        public int Id { get; set; }
        public Vendor? Vendor { get; set; }
        public int? VendorId { get; set; }

        public Brick? Brick { get; set; }
        public int? BrickId { get; set; }

        public int AvailableAmount { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

    }
}
