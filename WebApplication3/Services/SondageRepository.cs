using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USherbrooke.ServiceModel.Sondage;

namespace WebApplication3.Services
{
    public class SondageRepository
    {
        private SimpleSondageDAO sondage;

        public SondageRepository()
        {
            sondage = new SimpleSondageDAO();
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