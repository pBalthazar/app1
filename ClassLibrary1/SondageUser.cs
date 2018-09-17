using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace USherbrooke.ServiceModel.Sondage
{
    [DataContract]
    public class SondageUser
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int UserId { get; set; }


        public SondageUser(string username, string password, int userId = -1)
        {
            Username = string.IsNullOrWhiteSpace(username) ? string.Empty : username;
            Password = password;
            UserId = userId;
        }
    }
}
