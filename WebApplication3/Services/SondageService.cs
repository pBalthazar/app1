using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USherbrooke.ServiceModel.Sondage;

namespace WebApplication3.Services
{
    public class SondageService : ISondageService
    {
        private SondageRepository sondageRepository;

        public SondageService()
        {
            sondageRepository = new SondageRepository();
        }

        public int Connect()
        {
            throw new NotImplementedException();
        }
        public IList<Poll> GetAvailablePolls(int userId)
        {
            return sondageRepository.GetSondages();
        }

        public PollQuestion GetNextQuestion(int userId, int pollId, int currentQuestionId)
        {
            return sondageRepository.GetNextQuestion(pollId, currentQuestionId);
        }

        public bool SaveAnswer(int userId, PollQuestion question)
        {
            return sondageRepository.SaveQuestionAnswer(userId, question);
        }
    }
}