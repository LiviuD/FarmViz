namespace FarmVizModels
{
    public interface IEat
    {
        decimal MaximumEatenDailyQuantity { get; }
        void  Eat(IActivity feed);
    }
}