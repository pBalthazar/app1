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

            catch  (InvalidIdException iie)
            {
                Console.WriteLine(iie);
            }
            catch (PersistenceException pe)
            {
                Console.WriteLine(pe);
                return null;
            }

            return null;
        }

        public PollQuestion GetNextQuestion(int pollId, int currentQuestionId)
        {
            try
            {
                return sondage.GetNextQuestion(pollId, currentQuestionId);
            }
            catch (InvalidIdException iie)
            {
                Console.WriteLine(iie);
            }
            catch (PersistenceException pe)
            {
                Console.WriteLine(pe);
                return null;
            }

            return null;
        }

        public bool SaveQuestionAnswer(int userId, PollQuestion question)
        {
            try
            {
                sondage.SaveAnswer(userId, question);
                return true;
            }

            catch (InvalidIdException iie)
            {
                Console.WriteLine(iie);
            }
            catch (PersistenceException pe)
            {
                Console.WriteLine(pe);
            }

            return false;
        }
    }
}