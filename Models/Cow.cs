using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class Cow : MilkingAnimal
    {
        [JsonConstructor]
        public Cow(Gender gender) : base(AnimalType.Cow, gender)
        {
            MaximumEatenDailyQuantity = 30;
            MaximumDailyMilkQuantity = 35;
        }
    }
}
