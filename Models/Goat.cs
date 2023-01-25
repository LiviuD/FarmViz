using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class Goat: MilkingAnimal
    {
        [JsonConstructor]
        public Goat(Gender gender) : base(AnimalType.Goat, gender)
        {
            MaximumDailyMilkQuantity = 8;
            MaximumEatenDailyQuantity = 3;
        }
    }
}
