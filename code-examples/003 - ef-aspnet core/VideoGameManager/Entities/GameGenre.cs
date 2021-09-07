using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VideoGameManager.Entities
{
    public class GameGenre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //This anotation to handle error (A possible object cycle was detected).
        [JsonIgnore]
        public List<Game> Games { get; set; } = new();
    }
}
