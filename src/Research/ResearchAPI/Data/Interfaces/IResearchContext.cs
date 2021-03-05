using MongoDB.Driver;
using ResearchAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Data.Interfaces
{
    public interface IResearchContext
    {
        IMongoCollection<Research> Researches{ get; }

    }
}
