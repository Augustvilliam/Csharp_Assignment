
//Hela ombyggd med chatgpt för  användinng av : INotifyPropertyChanged: Detta för att lätt uppdatera gränssnittet i MAUI.
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Business.Models;

public class User : INotifyPropertyChanged
{
    private string _userId = null!;
    private string _firstName = null!;
    private string _lastName = null!;
    private string _email = null!;
    private string _adress = null!;
    private string _postal = null!;
    private string _locality = null!;
    private string _phonenmbr = null!;

    public string UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Adress
    {
        get => _adress;
        set => SetProperty(ref _adress, value);
    }

    public string Postal
    {
        get => _postal;
        set => SetProperty(ref _postal, value);
    }

    public string Locality
    {
        get => _locality;
        set => SetProperty(ref _locality, value);
    }

    public string Phonenmbr
    {
        get => _phonenmbr;
        set => SetProperty(ref _phonenmbr, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null!)
    {
        if (EqualityComparer<T>.Default.Equals(backingField, value))
            return false;

        backingField = value;
        OnPropertyChanged(propertyName);
        return true;
    }

}
