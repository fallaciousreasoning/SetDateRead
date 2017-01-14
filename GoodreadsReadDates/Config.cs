using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace GoodreadsReadDates
{
    public class Config
    {
        public const string CONFIG_FILE = "config.json";

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
        [JsonProperty("api_secret")]
        public string ApiSecret { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("access_token_secret")]
        public string AccessTokenSecret { get; set; }

        public static Config Load()
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(CONFIG_FILE));
        }

        public void Save()
        {
            File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(this));
        }
    }
}
