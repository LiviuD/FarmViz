using FarmVizModels;
using Spectre.Console.Rendering;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmVizUI
{
    public class FarmsReports
    {
        public List<Farm> Farms { get; init; }
        public FarmsReports(List<Farm> farms)
        {
            Farms = farms;
        }
        public Renderable ShowBestProducingAnimal()
        {
            if(Farms is null)
            {
                return null;
            }
            var barchart = new BarChart()
             .Width(60)
             .Label("[green bold underline]Best Producing Animal So Far By Farm[/]")
             .CenterLabel();
            foreach (var farm in Farms)
            {
                var color = Color.Yellow;
                if (Farms.IndexOf(farm) % 3 == 0) { color = Color.Yellow; }
                if (Farms.IndexOf(farm) % 3 == 1) { color = Color.Red; }
                if (Farms.IndexOf(farm) % 3 == 2) { color = Color.Blue; }

                var animalsWithTotalProduced = farm.Animals.Select(x => x is MilkingAnimal ? new { Animal = x, Total = (x as MilkingAnimal).GetMilkings().Sum(s => s.Amount) } : null);
                var animal = animalsWithTotalProduced.MaxBy(x => x?.Total ?? 0);
                barchart.AddItem($"{farm.Name} Id:{animal.Animal.Id}-{animal.Animal.AnimalType}", (double)animal.Total, color);
            }

            return barchart;
        }
    }
}
