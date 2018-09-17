using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using USherbrooke.ServiceModel.Sondage;

namespace ConsoleApp3
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<bool> TryConnection()
        {
            bool result = false;
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/values");
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        static async Task<IList<Poll>> GetSondages()
        {
            IList<Poll> result = null;

            HttpResponseMessage response = await client.GetAsync("api/Sondage");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<IList<Poll>>();
            }
            return result;
        }

        static async Task<PollQuestion> GetPollQuestion(int userId, int pollId, int currentQuestionId)
        {
            PollQuestion result = null;

            HttpResponseMessage response = await client.GetAsync($"api/Sondage/question?userId={userId}&pollId={pollId}&currentQuestionId={currentQuestionId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<PollQuestion>();
            }
            return result;
        }

        static async Task<bool> AnswerQuestion(int userId, PollQuestion question)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/Sondage/question?userId={userId}", question);
            response.EnsureSuccessStatusCode();

            bool succes = await response.Content.ReadAsAsync<bool>();
            return succes;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static private void LogInWithUserId()
        {
            //On demande le nom d'utilisateur
            Console.WriteLine("Enter your userId:");
            string _userId = Console.ReadLine();
            int userId;

            while (!Int32.TryParse(_userId, out userId))
            {
                Console.WriteLine("Enter a valid number...! Try again");
                userId = -1;
                _userId = Console.ReadLine();
            }
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44328/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            bool serverUp = await TryConnection();
            if (serverUp)
            {
                try
                {
                    //Authentification
                    LogInWithUserId();

                    bool quitPoll = false;

                    while (!quitPoll)
                    {
                        //Get the availables polls
                        IList<Poll> sondages = await GetSondages();

                        if (!SondageHelper.ValidateSondages(sondages))
                        {
                            quitPoll = true;
                        }
                        
                        ConsoleDisplayHelper.ShowSondages(sondages);

                        //Select the desire poll
                        string selectedPoll = Console.ReadLine();
                        int pollId = -1;

                        while (SondageHelper.ValidatePollId(sondages, selectedPoll))
                        {
                            Console.WriteLine("Choose a valid number...! Try again");
                            selectedPoll = Console.ReadLine();
                        }

                        Int32.TryParse(selectedPoll, out pollId);                        

                        //Get the first question
                        PollQuestion question = await GetPollQuestion(1, pollId, -1);
                        string questionAnswer = string.Empty;

                        while (question != null)
                        {
                            ConsoleDisplayHelper.ShowPollQuestion(question);

                            questionAnswer = Console.ReadLine();
                            while (!SondageHelper.ValidateQuestionAnser(questionAnswer))
                            {
                                Console.WriteLine("Invalid answer...! Try again");
                                questionAnswer = Console.ReadLine();
                            }                         
                            
                            question.Text = questionAnswer;

                            //Save the answer
                            await AnswerQuestion(1, question);

                            //Get the next question
                            question = await GetPollQuestion(1, pollId, question.QuestionId);
                        }

                        Console.WriteLine("Thank you. You have answered all the questions!");
                        Console.WriteLine("Do you want to answer an another poll? (Y/N)");
                        string quitOrNot = Console.ReadLine();
                        while (quitOrNot.ToLowerInvariant() != "y" && quitOrNot.ToLowerInvariant() != "n")
                        {
                            Console.WriteLine("Enter 'Y' or 'N'");
                            quitOrNot = Console.ReadLine();
                        }
                        if (quitOrNot.ToLowerInvariant() == "n")
                        {
                            quitPoll = true;
                        }
                    }
                    Console.WriteLine("GoodBye!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Server not responding...");
            }
            Console.ReadLine();
        }
    }
}