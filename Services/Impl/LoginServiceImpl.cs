using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeterApp.Services;

namespace MeterApp.Services.Impl
{
    class LoginServiceImpl : ILoginService
    {

        private bool _isUserLoggedIn = false;

        bool ILoginService.IsUserLoggedIn()
        {
            return _isUserLoggedIn;
        }

        bool ILoginService.Login(string username, string password)
        {
            return true;
        }

        void ILoginService.Logout()
        {
            throw new NotImplementedException();
        }
    }
}
