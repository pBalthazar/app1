using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USherbrooke.ServiceModel.Sondage;

namespace SondageServer.Services
{
    public class LoginService
    {
        private LoginRepository loginRepository;

        public LoginService()
        {
            loginRepository = new LoginRepository();
        }

        public int? Connect(SondageUser user)
        {
            return loginRepository.GetUserId(user);
        }

        public bool checkUserExists(int userId) {
            return loginRepository.checkUserExists(userId);
        }
    }
}
