using MongoDB.Driver;
using ResearchAPI.Data.Interfaces;
using ResearchAPI.Entities;
using ResearchAPI.Settings;
using System.Collections.Generic;
using System.Net.Http;
using UserAPI.Entities;

namespace ResearchAPI.Data
{
    public class ResearchContext : IResearchContext
    {
        private readonly IHttpClientFactory _clientFactory;

        public ResearchContext(IResearchDatabaseSettings settings, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Researches = database.GetCollection<Research>(settings.CollectionName);
            ResearchContextSeed.SeedData(Researches, _clientFactory);
        }
        

        public IMongoCollection<Research> Researches{ get; }
        public IEnumerable<User> Users { get; set; }

    }
}
