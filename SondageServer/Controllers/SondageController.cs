using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SondageServer.Services;
using USherbrooke.ServiceModel.Sondage;

[Route("api/[controller]")]
[ApiController]
public class SondageController : Controller
{
    readonly ILogger<SondageController> _log;

    public SondageController(ILogger<SondageController> log)
    {
        _log = log;
    }

    [HttpGet]
    public IList<Poll> Get()
    {
        _log.LogInformation("Hello, world!");
        return ServicesContainer.SondageService.GetAvailablePolls(1);
    }
    [HttpGet("question")]
    //public IActionResult Get([FromQuery(Name = "page")] string page)
    public PollQuestion Get([FromQuery(Name = "userId")] int userId, [FromQuery(Name = "pollId")] int pollId, [FromQuery(Name = "currentQuestionId")] int currentQuestionId)
    {
        return ServicesContainer.SondageService.GetNextQuestion(userId, pollId, currentQuestionId);
    }

    public bool Post(int userId, [FromBody] PollQuestion question)
    {
        return ServicesContainer.SondageService.SaveAnswer(userId, question);
    }
}





/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using USherbrooke.ServiceModel.Sondage;
using SondageServer.Services;

namespace SondageServer.Controllers
{
    public class SondageController : Controller
    {
        public IList<Poll> Get()
        {
            return ServicesContainer.SondageService.GetAvailablePolls(1);
            //return sondageRepository.GetSondages();
        }

        public PollQuestion Get(int userId, int pollId, int currentQuestionId)
        {
            return ServicesContainer.SondageService.GetNextQuestion(userId, pollId, currentQuestionId);
            //return sondageRepository.GetNextQuestion(pollId);
        }

        public bool Post(int userId, [FromBody] PollQuestion question)
        {
            return ServicesContainer.SondageService.SaveAnswer(userId, question);
            //return sondageRepository.SaveQuestionAnswer(userId, question);
        }
    }
}

*/
