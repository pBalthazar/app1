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
            try
            {
                return sondage.GetAvailablePolls();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public PollQuestion GetNextQuestion(int pollId, int currentQuestionId)
        {
            try
            {
                return sondage.GetNextQuestion(pollId, currentQuestionId);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool SaveQuestionAnswer(int userId, PollQuestion question)
        {
            try
            {
                sondage.SaveAnswer(userId, question);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}