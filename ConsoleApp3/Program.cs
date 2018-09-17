using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using USherbrooke.ServiceModel.Sondage;

namespace ConsoleApp3
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static readonly string API_KEY = "A2D3-HTDG-MLU2-3AM5";

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

        
        static async Task<int?> LogInWithUsername()
        {
            int? userId = null;
            
            Console.WriteLine("Enter your username:");
            string _username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string _password = Console.ReadLine();

            var user = new SondageUser(_username, GetHashString(_password));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/login", user);

            if (response.IsSuccessStatusCode)
            {
                userId = await response.Content.ReadAsAsync<int>();
            }
            return userId;
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44328/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(API_KEY);
            bool serverUp = await TryConnection();
            if (serverUp)
            {
                try
                {
                    bool quitPoll = false;
                    /*while (!LogInWithUsername().Result)
                    {
                        Console.WriteLine("Invalid username or password, try again.");
                    };*/
                    int? userId = await LogInWithUsername();

                    if (!userId.HasValue || userId.Value < 1)
                    {
                        Console.WriteLine("Invalid login!");
                        quitPoll = true;
                    }
                    
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

        static private byte[] GetHash(string inputString)
        {
            var algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        static private string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}