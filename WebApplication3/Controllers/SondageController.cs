using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using USherbrooke.ServiceModel.Sondage;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class SondageController : ApiController
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
