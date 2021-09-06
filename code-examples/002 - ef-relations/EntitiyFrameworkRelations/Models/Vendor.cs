using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiyFrameworkRelations.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string? VendorName { get; set; }
        public List<BrickAvailability>? Availabilities { get; set; } = new();
    }
}
