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
    //To log informations: _log.LogInformation("Hello, world!");
    public SondageController(ILogger<SondageController> log)
    {
        _log = log;
    }

    [HttpGet]
    public IList<Poll> Get()
    {
        return ServicesContainer.SondageService.GetAvailablePolls(1);
    }

    [HttpGet("question")]
    public PollQuestion Get([FromQuery(Name = "userId")] int userId, [FromQuery(Name = "pollId")] int pollId, [FromQuery(Name = "currentQuestionId")] int currentQuestionId)
    {
        return ServicesContainer.SondageService.GetNextQuestion(userId, pollId, currentQuestionId);
    }

    [HttpPost("question")]
    public bool Post([FromQuery(Name = "userId")] int userId, [FromBody] PollQuestion question)
    {
        return ServicesContainer.SondageService.SaveAnswer(userId, question);
    }
}
