using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Entities
{
    public class Research
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Canvas> Canvasess{ get; set; }
        public List<Post> Posts { get; set; }
        public string Moderator { get; set; }
        public List<string> Members { get; set; }
    }
}
