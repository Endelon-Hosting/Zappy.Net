using System;
namespace Zappy.Net.API
{
    public interface IUniversalDataLoader
    {
        T[] LoadData<T>(string entryName);
        void SaveData<T>(string entryName, T[] data);
    }
}
