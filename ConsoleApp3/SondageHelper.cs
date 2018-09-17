using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using USherbrooke.ServiceModel.Sondage;

namespace ConsoleApp3
{
    static public class SondageHelper
    {
        static public bool ValidateSondages(IList<Poll> sondages)
        {
            bool isValid = true;
            if (sondages == null || sondages.Count == 0)
            {
                Console.WriteLine("No poll available!");
                isValid = false;
            }
            return isValid;
        }

        static public bool ValidatePollId(IList<Poll> sondages, string selectedPoll)
        {
            return !Int32.TryParse(selectedPoll, out int pollId) || sondages.FirstOrDefault(x => x.Id == pollId) == null;
        }

        static public bool ValidateQuestionAnser(string questionAnswer)
        {
            Regex regex = new Regex(@"[a|b|c|d]");
            Match match = regex.Match(questionAnswer.ToLowerInvariant());
            return !string.IsNullOrWhiteSpace(questionAnswer) && questionAnswer.Count() == 1 && match.Success;
        }
    }
}
