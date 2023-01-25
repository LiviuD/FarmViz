namespace FarmVizModels
{
    public interface IAnimal: IEat, IGender
    {
        AnimalType AnimalType { get; }
    }
}