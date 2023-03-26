using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterApp.Services
{
    public interface ILoginService
    {
        bool IsUserLoggedIn();

        bool Login(string username, string password);

        void Logout();
    }
}
