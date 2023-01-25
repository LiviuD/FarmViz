using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class Farm
    {
        public Farm(string name)
        {
            this.Name = name;
            this.Animals = new List<Animal>();
        }

        [JsonConstructor]
        public Farm(string name,List<Animal> animals) : this(name)
        {
            Animals = animals;
        }
        public string Name { get; set; }
        [JsonPropertyName("Animals")]
        public List<Animal> Animals { get; set; }

    }
}
