using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Entities
{
    public class Canvas
    {
        public string Id { get; set; }
        public string Title{ get; set; }
        public List<Question> Questions{ get; set; }
    }
}
