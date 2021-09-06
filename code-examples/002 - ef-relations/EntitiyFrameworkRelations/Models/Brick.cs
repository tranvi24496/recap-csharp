using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiyFrameworkRelations.Models
{
    public class Brick
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;
        public Color? Color { get; set; }
        public List<Tag>? Tags { get; set; } = new();

        public List<BrickAvailability>? Availabilities { get; set; } = new();
    }

    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black,
        White
    }
}
