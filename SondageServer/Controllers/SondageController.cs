using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondageServer.Services;
using USherbrooke.ServiceModel.Sondage;

[Route("api/[controller]")]
[ApiController]
public class SondageController : Controller
{
    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
        return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }


    /*
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
    */
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