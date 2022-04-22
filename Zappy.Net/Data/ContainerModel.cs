using System;

namespace Zappy.Net.Data
{
    public class ContainerModel
    {
        public string Uuid { get; set; }
        public string ImageName { get; set; }
        public int CpuCores { get; set; }
        public int Memory { get; set; }
        public int Disk { get; set; }
    }
}
