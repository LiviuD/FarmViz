using FarmVizModels;
using System.Text.Json;
using System.Text.Json.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace FarmVizServices
{
    public class FileImport : IFileImport
    {
        public List<Farm> Import(string filePath)
        {
            var jsonText = string.Join(' ', File.ReadAllLines(filePath));
            JsonNode document = JsonNode.Parse(jsonText)!;
            JsonNode root = document.Root;
            JsonArray farmsArray = root!.AsArray();
            var farms = new List<Farm>();

            if (farmsArray is not null)
            {
               foreach(var farmJson in farmsArray)
               {
                    if (farmJson?["Name"] is JsonNode nameNode)
                    {
                        var farm = new Farm(nameNode.ToString());
                        farms.Add(farm);
                        if (farmJson?["Animals"] is JsonArray animalsArray)
                        {
                            foreach(var animalJson in animalsArray)
                            {
                                if (animalJson?["Id"] is JsonNode idNode
                                    && animalJson?["Sex"] is JsonNode sexNode
                                    && animalJson?["AnimalType"] is JsonNode animalTypeNode)
                                {
                                    Animal animal = null;
                                    var sex = (Gender)Enum.Parse(typeof(Gender), sexNode.ToString(), true);
                                    var animalType = (AnimalType)Enum.Parse(typeof(AnimalType), animalTypeNode.ToString(), true);
                                    switch (animalType)
                                    {
                                        case AnimalType.Cow:
                                            var cow = new Cow(sex);
                                            cow.Id = (int)idNode;
                                            animal = cow;
                                            farm.Animals.Add(cow);
                                            break;

                                        case AnimalType.Goat:
                                            var goat = new Goat(sex);
                                            goat.Id = (int)idNode;
                                            animal = goat;
                                            farm.Animals.Add(goat);
                                            break;

                                        default:
                                            animal = new Animal(animalType, sex);
                                            farm.Animals.Add(animal);
                                            break;

                                    }

                                    if (animal is not null && animalJson?["Feedings"] is JsonArray feedingsArray)
                                    {
                                        foreach (var feedingJson in feedingsArray)
                                        {
                                            decimal amount = 0;
                                            DateTime dateTime = DateTime.MinValue;
                                            if (feedingJson?["Amount"] is JsonNode amountNode)
                                            {
                                                amount = (decimal)amountNode;
                                            }
                                            if (feedingJson?["DateTime"] is JsonNode datetimeNode)
                                            {
                                                dateTime = DateTime.ParseExact(datetimeNode.ToString(), "yyyy-MM-ddTHH:mm:ss", null);
                                            }
                                            var feed = new Feed(dateTime, amount);
                                            try
                                            {
                                                animal.Eat(feed);
                                            }
                                            catch(ArgumentOutOfRangeException)
                                            {
                                                //log 
                                            }
                                        }
                                    }

                                    if (animal is MilkingAnimal milkingAnimal
                                        && animalJson?["Milkings"] is JsonArray milkingsArray)
                                    {
                                        foreach (var milkingJson in milkingsArray)
                                        {
                                            decimal amount = 0;
                                            DateTime dateTime = DateTime.MinValue;
                                            if (milkingJson?["Amount"] is JsonNode amountNode)
                                            {
                                                amount = (decimal)amountNode;
                                            }
                                            if (milkingJson?["DateTime"] is JsonNode datetimeNode)
                                            {
                                                dateTime = DateTime.ParseExact(datetimeNode.ToString(), "yyyy-MM-ddTHH:mm:ss", null);
                                            }
                                            var milking = new Milking(dateTime, amount);
                                            try
                                            {
                                                milkingAnimal.Milk(milking);
                                            }
                                            catch(ArgumentOutOfRangeException)
                                            {
                                                //log
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
               }

            }
            
            return farms;
        }
    }
}
