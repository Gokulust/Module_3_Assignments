﻿using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNuint
{
    public class PostData
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        


    }
    public class UserDataResponse
    {
        [JsonProperty("data")]

        public PostData Data { get; set; }
    }
}
