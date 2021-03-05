﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchAPI.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public List<string> Likes { get; set; }
        public List<string> Dislikes { get; set; }
    }
}
