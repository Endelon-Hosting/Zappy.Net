using System;
namespace Zappy.Net.API
{
    public class Configuration
    {
        private ZappyController Zappy { get; set; }

        public Configuration(ZappyController zappy)
        {
            Zappy = zappy;
        }

        public string DockerUri { get; set; }
        public string RootPath { get; set; }
    }
}
