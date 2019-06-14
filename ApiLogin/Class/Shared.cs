using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogin.Class
{
    public class ConfigurationTocken
    {
        public string SecretKey { get; set; }
        public string host { get; set; }
        public int timeExpire { get; set; }

        public ConfigurationTocken()
        {
            this.SecretKey = "apidemorest@stefanini.com";
            this.host = "https://localhost:44338";
            this.timeExpire = 30;
        }
    }
}
