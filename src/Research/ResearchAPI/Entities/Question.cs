using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Entities
{

    public enum QuestionType { Text, Single, Multy }

    public class Question
    {
        public string Id{ get; set; }
        public string Title { get; set; }
        public string Answer{ get; set; }
        public string Statistic{ get; set; }
        public QuestionType Type { get; set; }
    }
}
