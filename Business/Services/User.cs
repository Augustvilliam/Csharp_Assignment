using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    internal class User
    {
        public string UserData { get; set; }

        public User(string userdata)
        {
            UserData = userdata;
        }
    }
}
