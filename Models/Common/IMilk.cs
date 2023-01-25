namespace FarmVizModels
{
    internal interface IMilk
    {
        decimal MaximumDailyMilkQuantity { get; }
        void Milk(IActivity milking);
    }
}