using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USherbrooke.ServiceModel.Sondage;

namespace ConsoleApp3
{
    static public class ConsoleDisplayHelper
    {
        static public void ShowSondages(IList<Poll> sondages)
        {
            Console.WriteLine("Listing sondages...");
            foreach (Poll sondage in sondages)
            {
                Console.WriteLine($"Id: {sondage.Id} \t Description: {sondage.Description}");
            }
        }

        static public void ShowPollQuestion(PollQuestion question)
        {
            if (question != null)
            {
                Console.WriteLine("The next question is: ");
                Console.WriteLine($"{question.QuestionId}: {question.Text}");
            }
        }
    }
}
