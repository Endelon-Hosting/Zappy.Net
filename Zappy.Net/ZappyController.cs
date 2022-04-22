using System;
using System.IO;

using Zappy.Net.API;
using Zappy.Net.Provider;
using Zappy.Net.Exceptions;

namespace Zappy.Net
{
    public class ZappyController
    {
        public ContainerManager Containers { get; set; }
        public Configuration Configuration { get; set; }
        public DockerProvider Docker { get; set; }
        public IUniversalDataLoader DataLoader { get; set; }

        public ZappyController()
        {
            // Init all zappy components

            Configuration = new Configuration(this);
            Containers = new ContainerManager(this);
            Docker = new DockerProvider(this);
        }

        public void Start()
        {
            #region Files and stuff

            Directory.CreateDirectory(Configuration.RootPath);
            Directory.CreateDirectory(Configuration.RootPath + "/backups");
            Directory.CreateDirectory(Configuration.RootPath + "/volumes");
            Directory.CreateDirectory(Configuration.RootPath + "/transfer");

            #endregion

            if(!Docker.CheckConnection())
            {
                throw new DockerConnectException("Unable to connect to the docker instance using the url provided");
            }


        }
    }
}
