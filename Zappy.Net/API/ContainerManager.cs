using System;
using System.Linq;
using System.Collections.Generic;

using Zappy.Net.Data;

namespace Zappy.Net.API
{
    public class ContainerManager
    {
        private ZappyController Zappy { get; set; }

        private Dictionary<string, ContainerState> ContainerStates { get; set; }

        public ContainerManager(ZappyController zappy)
        {
            Zappy = zappy;

            ContainerStates = new Dictionary<string, ContainerState>();
        }

        public void Start()
        {
            var containers = Zappy.DataLoader.LoadData<ContainerModel>("containers").ToList();

            // The following part deletes containers which docker containers have been removed

            var toDel = new List<ContainerModel>();
            var allUuids = new List<string>();

            foreach(var c in Zappy.Docker.GetContainers())
            {
                allUuids.Add(c.ID);
            }

            foreach(var c in containers)
            {
                if (!allUuids.Contains(c.Uuid))
                    toDel.Add(c);
            }

            foreach(var c in toDel)
            {
                containers.Remove(c);
            }

            Zappy.DataLoader.SaveData<ContainerModel>("containers", containers.ToArray());

            LoadStates();
        }

        private void LoadStates()
        {
            ContainerStates.Clear();

            var containers = Zappy.Docker.GetContainers();
            var zappyContainers = Zappy.DataLoader.LoadData<ContainerModel>("containers").ToList();

            foreach(var c in containers)
            {
                var zc = zappyContainers.Find(x => x.Uuid == c.ID);

                if(zc != null)
                {
                    var state = ContainerState.Unknown;

                    switch(c.Status)
                    {
                        case "exited":
                            state = ContainerState.Offline;
                            break;

                        case "running":
                            if (DetectRunning(zc.Uuid))
                                state = ContainerState.Running;
                            else
                                state = ContainerState.Starting;
                            break;

                        default:
                            state = ContainerState.Unknown;
                            break;
                    }

                    ContainerStates.Add(zc.Uuid, state);
                }
            }
        }

        private bool DetectRunning(string uuid)
        {
            return true;
        }
    }
}
