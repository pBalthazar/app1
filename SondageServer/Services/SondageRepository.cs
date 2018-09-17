using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USherbrooke.ServiceModel.Sondage;

namespace SondageServer.Services
{
    public class SondageRepository
    {
        private SimpleSondageDAO sondage;

        public SondageRepository()
        {
            sondage = ServicesContainer.GetSondageMemory();
        }

        public IList<Poll> GetSondages()
        {
            return sondage.GetAvailablePolls();
        }

        public PollQuestion GetNextQuestion(int pollId, int currentQuestionId)
        {
            return sondage.GetNextQuestion(pollId, currentQuestionId);
        }

        public bool SaveQuestionAnswer(int userId, PollQuestion question)
        {
            sondage.SaveAnswer(userId, question);
            return true;
        }
    }
}