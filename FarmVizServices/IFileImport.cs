using FarmVizModels;

namespace FarmVizServices
{
    public interface IFileImport
    {
        List<Farm> Import(string filePath);
    }
}