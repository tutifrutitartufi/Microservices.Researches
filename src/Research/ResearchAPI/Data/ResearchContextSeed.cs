using MongoDB.Driver;
using ResearchAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UserAPI.Entities;

namespace ResearchAPI.Data
{
    public class ResearchContextSeed
    {


        public static IEnumerable<User> Users { get; set; }

        public static async void SeedData(IMongoCollection<Research> researchCollection, IHttpClientFactory _clientFactory)
        {
            bool existsResearches= researchCollection.Find(p => true).Any();
            if (!existsResearches)
            {
                await GetUsers(_clientFactory);
                await researchCollection.InsertManyAsync(GetPreconfiguredResearches());
            }
        }

        public static async Task GetUsers(IHttpClientFactory _clientFactory)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://userapi:80/api/v1/User");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                Users = await JsonSerializer.DeserializeAsync
                    <IEnumerable<User>>(responseStream, options);
            }
            else
            {
                Users = Array.Empty<User>();
            }
        }

        private static IEnumerable<Research> GetPreconfiguredResearches()
        {

            List<string> IDs = Users.Select((o) => o.Id).ToList();

            return new List<Research>()
            {
                new Research()
                {
                    Members = IDs,
                    Moderator = IDs.FirstOrDefault(),
                    Name = "Test 1",
                    Posts = new List<Post>()
                    {
                        new Post()
                        {
                            Id = "100",
                            Content = "Test Content Post 1",
                            Likes = new List<string>()
                            {
                                IDs.FirstOrDefault()
                            },
                            Dislikes = new List<string>()
                            {
                                IDs.LastOrDefault()
                            },                        }
                    },
                    Canvasess = new List<Canvas>()
                    {
                        new Canvas()
                        {
                            Id = "100",
                            Title = "Canvas Title Test 1",
                            Questions = new List<Question>()
                            {
                                new Question()
                                {
                                    Id = "100",
                                    Title = "Title Test 1",
                                    Answer = "Test 123",
                                    Statistic = "Test 123123123 stat",
                                    Type = QuestionType.Single
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
