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
    public User CreateDefultUser()
    {
        return new User()
        {
            UserId = IdGenerator.GenerateShortId(),
            FirstName = "sven",
            LastName = "Svensson",
            Email = "ex@ex.ex",
            Adress = "gatan",
            Postal = "00000",
            Locality = "stad",
            Phonenmbr = "0000000000",

        };
    }

    public User CreateUser(string firstName, string lastName, string email, string adress, string postal, string locality, string phonenmbr)
    {
        return new User
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Adress = adress,
            Postal = postal,
            Locality = locality,
            Phonenmbr = phonenmbr
        };
    }
}
