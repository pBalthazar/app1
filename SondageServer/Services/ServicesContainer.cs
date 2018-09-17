using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USherbrooke.ServiceModel.Sondage;

namespace SondageServer.Services
{
    static public class ServicesContainer
    {
        static private SimpleSondageDAO sondage;

        public static SondageService SondageService = new SondageService();
        public static LoginService LoginService = new LoginService();

        static public SimpleSondageDAO GetSondageMemory()
        {
            if (sondage == null)
            {
                sondage = new SimpleSondageDAO();
            }
            return sondage;
        }
    }
}