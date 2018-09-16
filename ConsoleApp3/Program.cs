﻿using System;
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

            HttpResponseMessage response = await client.GetAsync($"api/Sondage?userId={userId}&pollId={pollId}&currentQuestionId={currentQuestionId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<PollQuestion>();
            }
            return result;
        }

        static async Task<bool> AnswerQuestion(int userId, PollQuestion question)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/Sondage?userId={userId}", question);
            response.EnsureSuccessStatusCode();

            bool succes = await response.Content.ReadAsAsync<bool>();
            return succes;
        }

        static void ShowSondages(IList<Poll> sondages)
        {
            Console.WriteLine("Listing sondages...");
            foreach (Poll sondage in sondages)
            {
                Console.WriteLine($"Id: {sondage.Id} \t Description: {sondage.Description}");
            }
        }

        static void ShowPollQuestion(PollQuestion question)
        {
            if (question != null)
            {
                Console.WriteLine("The next question is: ");
                Console.WriteLine($"{question.QuestionId}: {question.Text}");
            }
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

        static async void ValidateSondages(IList<Poll> sondages)
        {
            string tryAgain;
            while (sondages == null || sondages.Count == 0)
            {
                Console.WriteLine("No poll available! Try again (Y/N)?");
                tryAgain = Console.ReadLine();
                while (tryAgain != "Y" || tryAgain != "N")
                {
                    tryAgain = Console.ReadLine();
                }
                if (tryAgain == "Y")
                {
                    sondages = await GetSondages();
                }
                else
                {
                    Environment.Exit(0);
                }
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
                    LogInWithUserId();

                    IList<Poll> sondages = await GetSondages();
                    ValidateSondages(sondages);
                    ShowSondages(sondages);

                    string selectedSondage = Console.ReadLine();
                    int pollId = -1;

                    while (!Int32.TryParse(selectedSondage, out pollId) || sondages.FirstOrDefault(x => x.Id == pollId) == null)
                    {
                        Console.WriteLine("Choose a valid number...! Try again");
                        pollId = -1;
                        selectedSondage = Console.ReadLine();
                    }

                    PollQuestion question = await GetPollQuestion(1, pollId, -1);
                    string questionAnswer = string.Empty;

                    while (question != null)
                    {
                        ShowPollQuestion(question);
                        questionAnswer = Console.ReadLine();
                        question.Text = questionAnswer;

                        await AnswerQuestion(1, question);
                        question = await GetPollQuestion(1, pollId, question.QuestionId);
                    }
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