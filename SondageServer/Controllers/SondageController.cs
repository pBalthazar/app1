using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SondageServer.Services;
using USherbrooke.ServiceModel.Sondage;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SondageController : Controller
{
    private const int MAX_POLL_NUMBER = 500;
    private const int MAX_QUESTION_NUMBER = 500;

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
        PollQuestion result = null;

        if (ValidateUserId(userId) && ValidatePollId(pollId) && ValidateQuestionId(currentQuestionId))
        {
            result = ServicesContainer.SondageService.GetNextQuestion(userId, pollId, currentQuestionId);
        }

        return result;
    }

    [HttpPost("question")]
    public bool Post([FromQuery(Name = "userId")] int userId, [FromBody] PollQuestion question)
    {
        bool result = false;

        if (ValidateUserId(userId) && ValidateQuestionToSave(question))
        {
            result = ServicesContainer.SondageService.SaveAnswer(userId, question);
        }
        return result;
    }

    private bool ValidateQuestionAnswer(string answer)
    {
        Regex regex = new Regex(@"[a|b|c|d]");
        Match match = regex.Match(answer.ToLowerInvariant());
        return !string.IsNullOrWhiteSpace(answer) && answer.Count() == 1 && match.Success;
    }

    private bool ValidateQuestionToSave(PollQuestion question)
    {
        return ValidatePollId(question.PollId) && ValidateQuestionId(question.QuestionId) && ValidateQuestionAnswer(question.Text);
    }

    private bool ValidateUserId(int userId)
    {
        return userId > 0;
    }

    private bool ValidatePollId(int pollId)
    {
        return pollId > 0 && pollId < MAX_POLL_NUMBER;
    }

    private bool ValidateQuestionId(int questionId)
    {
        return questionId > -2 && questionId < MAX_QUESTION_NUMBER;
    }

}
