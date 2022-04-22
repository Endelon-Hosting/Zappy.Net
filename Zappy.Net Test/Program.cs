using System.IO;
using Logging.Net;
using Zappy.Net;

namespace Zappy.Net_Test
{
    class Program
    {
        public static ZappyController Zappy { get; set; }

        public static void Main(string[] args)
        {
            Logger.UsedLogger = new Logging.Net.Spectre.SpectreLogger();

            Logger.Info("Starting Zappy.Net Test");

            Zappy = new ZappyController();

            Zappy.Configuration.DockerUri = "tcp://localhost:2375";
            Zappy.Configuration.RootPath = "/var/lib/zappy";

            Zappy.DataLoader = new JsonDataLoader()
            {
                RootPath = Directory.GetCurrentDirectory() + "/data"
            };

            Zappy.Start(); // Start all zappy services
        }
    }
}