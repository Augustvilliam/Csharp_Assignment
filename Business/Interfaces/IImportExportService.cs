

namespace Business.Interfaces;

public interface IImportExportService
{
    void SaveListToFile<T>(List<T> list, string fileName);
    List<T> LoadListFromFile<T>(string fileName);
    void ShowMenu();
}
