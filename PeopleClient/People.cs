using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleClient
{
    public class People
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("Sports")]
        public bool Sports { get; set; }

        [JsonProperty("CVV")]
        public int CVV { get; set; }
    }
}
