using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Helper;
using Business.Interfaces;
using Business.Models;

namespace Business.Factory;

public class UserFactory : IUserFactory
{

    public User CreateUser(User inputUser)
    {
        if (inputUser == null) throw new ArgumentNullException(nameof(inputUser));
        return new User
        {
            UserId = IdGenerator.GenerateShortId(),
            FirstName = inputUser.FirstName,
            LastName = inputUser.LastName,
            Email = inputUser.Email,
            Adress = inputUser.Adress,
            Postal = inputUser.Postal,
            Locality = inputUser.Locality,
            Phonenmbr = inputUser.Phonenmbr
        };
    }
}
