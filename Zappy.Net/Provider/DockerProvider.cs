using System;
using System.Collections.Generic;

using Docker.DotNet;
using Docker.DotNet.Models;

namespace Zappy.Net.Provider
{
    public class DockerProvider
    {
        private ZappyController Zappy { get; set; }
        private DockerClient DockerClient { get; set; }

        public DockerProvider(ZappyController zappy)
        {
            Zappy = zappy;
        }

        private void InitIfNeeded()
        {
            if(DockerClient == null)
                DockerClient = new DockerClientConfiguration(new Uri(Zappy.Configuration.DockerUri)).CreateClient();
        }

        public bool CheckConnection()
        {
            InitIfNeeded();

            try
            {
                var res = DockerClient.Containers.ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters()).Result;

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public IList<ContainerListResponse> GetContainers()
        {
            InitIfNeeded();

            var res = DockerClient.Containers.ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters()
            {
                All = true
            }).Result;

            return res;
        }
    }
}
