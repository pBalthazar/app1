using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SondageServer.Services;
using USherbrooke.ServiceModel.Sondage;

namespace SondageServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {       
        [HttpPost]
        public int? Post([FromBody] SondageUser user)
        {
            return ServicesContainer.LoginService.Connect(user);
        }

    }
}