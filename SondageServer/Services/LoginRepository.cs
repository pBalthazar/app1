using System.Collections.Generic;
using System.Linq;
using USherbrooke.ServiceModel.Sondage;

namespace SondageServer.Services
{
    public class LoginRepository
    {
        private readonly IList<SondageUser> availablesUsers = new List<SondageUser>();

        public LoginRepository()
        {
            availablesUsers.Add(new SondageUser("Phil", "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08", 55));
            availablesUsers.Add(new SondageUser("Ben", "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08", 1));
        }

        public int? GetUserId(SondageUser user)
        {
            int? userId = null;
            SondageUser repoUser = availablesUsers.FirstOrDefault(x => x.Username == user.Username);
                                 
            if (repoUser != null && user.Password == repoUser.Password)
            {
                userId = repoUser.UserId;
            }

            return userId;
        }

        public bool checkUserExists(int userId) {
            foreach (var user in availablesUsers)
            {
                if (user.UserId == userId) {
                    return true;
                }
            }
            return false;
        }
    }
}
