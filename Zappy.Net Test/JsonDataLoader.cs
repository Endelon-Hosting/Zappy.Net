using System;
using System.IO;

using Zappy.Net.API;

using Newtonsoft.Json;

namespace Zappy.Net_Test
{
    public class JsonDataLoader : IUniversalDataLoader
    {
        public string RootPath { get; set; }

        public T[] LoadData<T>(string entryName)
        {
            if(File.Exists(RootPath + "/" + entryName + ".json"))
            {
                return JsonConvert.DeserializeObject<T[]>(File.ReadAllText(
                        RootPath + "/" + entryName + ".json"
                    ));
            }
            else
            {
                return new T[0];
            }
        }
        public void SaveData<T>(string entryName, T[] data)
        {
            File.WriteAllText(RootPath + "/" + entryName + ".json", JsonConvert.SerializeObject(data));
        }
    }
}
