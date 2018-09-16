using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SondageServer.Services
{
    static public class ServicesContainer
    {
        public static SondageService SondageService = new SondageService();
    }
}