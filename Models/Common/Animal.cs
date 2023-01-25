using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class Animal : IAnimal
    {
        public int Id { get; set;}

        [JsonConstructor]
        public Animal(int id, AnimalType animalType, Gender gender, List<Feed> feedings) : this (animalType, gender)
        {
            Id = id;
            Feedings = feedings?.Cast<IActivity>().ToList() ?? new List<Feed>().Cast<IActivity>().ToList();
        }

        public Animal(AnimalType animalType, Gender gender)
        {
            AnimalType = animalType;
            Sex = gender;
            Feedings = new List<Feed>().Cast<IActivity>().ToList();
        }

        public decimal MaximumEatenDailyQuantity { get; init; }

        public  Gender Sex { get; init; }

        public  AnimalType AnimalType { get; init; }

        protected IList<IActivity> Feedings { get; set; }

        public IEnumerable<Feed> GetFeedings()
        {
            return new List<Feed>(Feedings.Cast<Feed>()).AsEnumerable();
        }
        public virtual decimal FeedOnADate(DateTime date)
        {
            var result = Feedings.Where(x => x.DateTime.Date == date.Date).Sum(x => x.Amount);
            return result;
        }

        public virtual void Eat(IActivity feed)
        {
            if (MaximumEatenDailyQuantity > 0 && MaximumEatenDailyQuantity < feed.Amount + FeedOnADate(feed.DateTime))
            {
                throw new ArgumentOutOfRangeException($"The {Enum.GetName(AnimalType)} cannot eat more than {MaximumEatenDailyQuantity}!");
            }
            this.Feedings.Add(feed);
        }
       
    }
}
