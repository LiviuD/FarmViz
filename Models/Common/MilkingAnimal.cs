using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class MilkingAnimal: Animal, IMilk
    {
        public MilkingAnimal(AnimalType animalType, Gender gender) : base(animalType, gender)
        {
            Milkings = new List<Milking>().Cast<IActivity>().ToList();
        }

        [JsonConstructor]
        public MilkingAnimal(int id, AnimalType animalType, Gender gender, List<Milking> milikings, List<Feed> feedings) : this(animalType, gender)
        {

            Id = id;
            AnimalType = animalType;
            Sex = gender;
            Milkings = milikings?.Cast<IActivity>().ToList() ?? new List<Milking>().Cast<IActivity>().ToList();
            Feedings = feedings?.Cast<IActivity>().ToList() ?? new List<Feed>().Cast<IActivity>().ToList();
        }
        public decimal MaximumDailyMilkQuantity { get; init; }

        public IEnumerable<Milking> GetMilkings() 
        { 
            return new List<Milking>(Milkings.Cast<Milking>()).AsEnumerable(); 
        }
        protected IList<IActivity> Milkings { get; set; }
        protected virtual decimal MilkingOnDate(DateTime date)
        {
            var result = Milkings.Where(x => x.DateTime.Date == date.Date).Sum(x => x.Amount);
            return result;
        }
        public virtual void Milk(IActivity milking)
        {
            if (Sex != Gender.Female)
            {
                throw new ArgumentOutOfRangeException("Only females can be milked or produce milk!");
            }

            if (MaximumDailyMilkQuantity > 0 && MaximumDailyMilkQuantity < MilkingOnDate(milking.DateTime) + milking.Amount)
            {
                throw new ArgumentOutOfRangeException("The maximum milk quantity has exceeded!");
            }

            this.Milkings.Add(milking);
        }
    }
}
