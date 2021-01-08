using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Web.Http;

namespace TravelApp.Models
{
    class LoginViewModel
    {
        private async Task Login(String username, String password)
        {
            await ApiMethods.AuthenticateUser(username, password);
        }
    }
}
