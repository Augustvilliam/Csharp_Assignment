
using Business.Models;

namespace Business.Interfaces;

public interface IFileService //kräven savelisttofile, samt att ladda in user i listan. 
{
    void SaveListToFile<T>(List<T> list);
    List<User> LoadList();
}
