using System.Collections.ObjectModel;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly IFileService _fileService;
    private readonly IUserFactory _userFactory;
    private readonly IUserValidation _userValidation;
    private ObservableCollection<User> _users;
    private User _selectedUser;
    public ListUserPage(IUserService userService, IFileService fileService, IUserFactory userFactory, IUserValidation userValidation)
    {
        InitializeComponent();
        _userService = userService;
        _fileService = fileService;
        _userFactory = userFactory;
        _userValidation = userValidation;
        _users = new ObservableCollection<User>(_userService.GetAll());

        LoadUsers();

    }

    private void LoadUsers() //laddar in anv�ndare fron GetAll i en observalbleCollection, Catchar om n�got gick lite snett Mycket ChatGPT h�r
    {
        try
        {
            var loadedUsers = _userService.GetAll();
            _users = new ObservableCollection<User>(loadedUsers);
            UserListView.ItemsSource = _users;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"Error loading users: {ex.Message}");
            DisplayAlert("Error", "Failed to load users.", "OK");
             
        }
    }
    private void ReloadUsers() //rensar, laddar in och laddar om anv�ndarlistan f�r att uppdatera gr�nsslittet.Mycket ChatGPT h�r
    {
        try
        {
            var loadedUsers = _userService.GetAll().ToList(); // Definiera loadedUsers h�r
            _users.Clear();
            foreach (var user in loadedUsers)
            {
                _users.Add(user);
            }
            Debug.WriteLine($"Reloading {loadedUsers.Count} users from service.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reloading users: {ex.Message}");
            DisplayAlert("Error", "Failed to reload users.", "OK");
        }

 
    }
    protected override void OnAppearing() //Hj�lpmetod som gjordes f�r att h�lla allt grafiskt uppdaterat vid anv�ndarhanteringen. Mycket ChatGPT h�r, p� LoadUSAer och RelaodUSer
    {
        base.OnAppearing();
        ReloadUsers();
    }
    private async void Button_EditConfirm_Clicked(object sender, EventArgs e) //tar och bekr�ftar knapptrycket f�r Edituser knappen (pennan) G�r den edit user som i sin tur sl�nger den till editUser och sparar ner den s� allt �r i sin ordningn innan den sparar ner den. 
    {
        if (_selectedUser == null)
            return;

        _selectedUser.FirstName = Entry_FirstName.Text;
        _selectedUser.LastName = Entry_LastName.Text;
        _selectedUser.Email = Entry_Email.Text;
        _selectedUser.Adress = Entry_Adress.Text;
        _selectedUser.Postal = Entry_Postal.Text; 
        _selectedUser.Locality = Entry_Locality.Text;
        _selectedUser.Phonenmbr = Entry_Phone.Text;

        if (!ValidateInputUser(_selectedUser, out string errorMessage))
            return;
        

        _userService.EditUser(_selectedUser.UserId, _selectedUser);
        _fileService.SaveListToFile(_users.ToList());

        await DisplayAlert("Sucess!", $"User: {_selectedUser.UserId} was uppdated", "ok");
        ClearForm();
        Button_Create.IsVisible = true;
        Button_EditConfirm.IsVisible = false;
        ReloadUsers(); //laddar om anv�ndarlistan s� att gamal info inte visas fram tills att man byer anv�ndare en g�ng.
    }
    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserMainPage");
    }
    private async void Button_Create_Clicked(object sender, EventArgs e) //Create user knappen, tar in all anv�ndar data fr�n f�lten och tilldelar dom till r�tt parameter, �r n�got som det inte ska? Till invalidsuer med dig. �r allt i sin ordning? Grattis du har ni en plats i listan.
    {
        var inputUser = new User

        {
            FirstName = Entry_FirstName.Text,
            LastName = Entry_LastName.Text,
            Email = Entry_Email.Text,
            Adress = Entry_Adress.Text,
            Postal = Entry_Postal.Text,
            Locality = Entry_Locality.Text,
            Phonenmbr = Entry_Phone.Text,
        };



        if (!ValidateInputUser(inputUser, out string errorMessage))
            return;

        var newUser = _userFactory.CreateUser(inputUser);
        _userService.Add(newUser);
        _users.Add(newUser);

        await DisplayAlert("Sucess", "User was created", "ok");
        ClearForm();

    } //
    private async void Button_Delete_Clicked(object sender, EventArgs e) // Delete knappen tar in en user (den man har klickat p�) p� den anv�ndaren sagd deleteknapp siter p�. En displayalert som varnar dig vad du h�ller p� med. sen g�r gamla vanliga v�gen till delete user via Id.
    {
        if (sender is Button { BindingContext: User user })
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {user.FirstName}?", "Yes", "No");
            if (confirm)
            {
                _userService.DeleteUser(user.UserId);
                _users.Remove(user);
            }
        }

    }
    private  void Button_Edit_Clicked(object sender, EventArgs e) //funktionen f�r att faktiskt v�lja env�ndaren som ska redigeras. tar in vald user och sl�nger in infon i create f�ltet och byter ut knappen mot edit ist�llet. 
    {
        if ((sender as Button)?.BindingContext is User user)
        {
            _selectedUser = user;
            Entry_FirstName.Text = _selectedUser.FirstName;
            Entry_LastName.Text = _selectedUser.LastName;
            Entry_Email.Text = _selectedUser.Email;
            Entry_Adress.Text = _selectedUser.Adress;
            Entry_Postal.Text = _selectedUser.Postal;
            Entry_Locality.Text = _selectedUser.Locality;
            Entry_Phone.Text = _selectedUser.Phonenmbr;

            Button_Create.IsVisible = false; //d�ljer create
            Button_EditConfirm.IsVisible = true; //Visar Edit comfirm.
        }

    }
    private void UsertListView_selectionChanged(object sender, SelectionChangedEventArgs e) //gjord med chat gpt. Visar urvalet av users(om det finns n�gra) d�r man kan v�lja
    {
        if (e.CurrentSelection.FirstOrDefault() is User selectedUser) //h�mtar f�rsta objeket fr�n listan. om det inte finns  �r det null. om anv�ndare blir vald blir den tilldelad selected user, och �r det en giltig anv�ndare g�r den vidare till if satsen.
        {//Tilldelar anv�ndar uppgifterna till An�nvaruppgifterna i user details listan p� h�gra sidan av anv�ndardelen 
            Label_UserId.Text = selectedUser.UserId;
            Label_UserName.Text = $"{selectedUser.FirstName} {selectedUser.LastName}";
            Label_Email.Text = selectedUser.Email;
            Label_Adress.Text = selectedUser.Adress;
            Label_Postal.Text = selectedUser.Postal;
            Label_Locality.Text = selectedUser.Locality;
            Label_Phone.Text = selectedUser.Phonenmbr;
        }
        else//ingen anv�ndare vald? ingen lista med uppgifter f�r dig. 
        {
            ClearUserDetails();
        }
    }
    private void ClearForm()//rensar samtliga anv�ndarf�lt. 
    {
        Entry_FirstName.Text = string.Empty;
        Entry_LastName.Text = string.Empty;
        Entry_Email.Text = string.Empty;
        Entry_Adress.Text = string.Empty;
        Entry_Postal.Text = string.Empty;
        Entry_Locality.Text = string.Empty;
        Entry_Phone.Text = string.Empty;

        Label_FirstNameError.IsVisible = false;
        Label_LastNameError.IsVisible = false;
        Label_EmailError.IsVisible = false;
        Label_AdressError.IsVisible = false;
        Label_PostalError.IsVisible = false;
        Label_LocalityError.IsVisible = false;
        Label_PhoneError.IsVisible = false;

        _selectedUser = null!;

    }
    private void ClearUserDetails()//s�tter anv�ndarf�lten till tomman.  
    {
        Label_UserId.Text = string.Empty;   
        Label_UserName.Text = string.Empty;
        Label_Email.Text = string.Empty;
        Label_Adress.Text = string.Empty;
        Label_Postal.Text = string.Empty;
        Label_Locality.Text = string.Empty;
        Label_Phone.Text = string.Empty;
    }
    private bool ValidateInputUser(User user, out string errorMessage) //anv�nader validateUser f�r att validera anv�ndarna. 
    {
        if (!_userValidation.ValidateUser(user, out errorMessage))
        {
            DisplayAlert("Validation Error", errorMessage, "OK");
            return false;
        }
        return true;
    }
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)  // f�r att hantera  Errorlabels till create och update user. mest chatGPT h�r.
    {
        if (sender is Entry entry)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                // D�lj felmeddelande om f�ltet �r tomt
                var emptyErrorLabel = GetErrorLabel(entry); 
                if (emptyErrorLabel != null)
                {
                    emptyErrorLabel.IsVisible = false;
                }
                else //om n�got skulle bli fel f�r man en debug writeline
                {
                    Debug.WriteLine($"Error: No matching label found for entry {entry.Placeholder}");
                }
                return;
            }

            var errorLabel = GetErrorLabel(entry); 
            if (errorLabel == null)
            {
                Debug.WriteLine($"Error: No matching label found for entry {entry.Placeholder}");
                return;
            }

            bool isValid = ValidateEntry(entry, e.NewTextValue);

            errorLabel.IsVisible = !isValid; //s�tter errorlabel till synlig om n�got f�lt �r invalid. 
            errorLabel.Text = !isValid ? GetValidationMessage(entry) : string.Empty;
        }
    }
    private Label GetErrorLabel(Entry entry) //h�mtar upp en errorlabel beronde p� vilken entry som inte �r giltig 
    {
        return entry == Entry_FirstName ? Label_FirstNameError :
       entry == Entry_LastName ? Label_LastNameError :
       entry == Entry_Email ? Label_EmailError :
       entry == Entry_Adress ? Label_AdressError :
       entry == Entry_Postal ? Label_PostalError :
       entry == Entry_Locality ? Label_LocalityError :
       entry == Entry_Phone ? Label_PhoneError : null!;
    }
    private bool ValidateEntry (Entry entry, string value) //hj�lper till med valideringen av samtliga entrys.
    {
        return entry == Entry_FirstName ? _userValidation.ValidateName(value) :
         entry == Entry_LastName ? _userValidation.ValidateName(value) :
         entry == Entry_Email ? _userValidation.ValidateEmail(value) :
         entry == Entry_Adress ? _userValidation.ValidateAdress(value) :
         entry == Entry_Postal ? _userValidation.ValidatePostal(value) :
         entry == Entry_Locality ? _userValidation.ValidateLocality(value) :
         entry == Entry_Phone ? _userValidation.ValidatePhone(value) : true;
    }
    private string GetValidationMessage(Entry entry) // text f�r till h�rande entry som inte �r valid. 
    {
        return entry == Entry_FirstName ? "First name must contain at least 2 characters." :
               entry == Entry_LastName ? "Last name must contain at least 2 characters." :
               entry == Entry_Email ? "Invalid email format." :
               entry == Entry_Adress ? "Address cannot be empty." :
               entry == Entry_Postal ? "Postal code must only contain numbers." :
               entry == Entry_Locality ? "Locality cannot be empty." :
               entry == Entry_Phone ? "Phone number must only contain numbers." :
               string.Empty;
    }

}