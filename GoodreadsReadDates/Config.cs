using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodreadsReadDates
{
    public class Config
    {
        public const string CONFIG_FILE = "config.json";

        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public Config Load()
        {

        }

        public void Save()
        {

        }
    }
}
