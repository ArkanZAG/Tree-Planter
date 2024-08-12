using System.IO;
using System.Net;
using Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        private static string _filePath = Path.Join(Application.persistentDataPath, "SaveData.sav");

        public static bool HasSaveData()
        {
            return File.Exists(_filePath);
        }
            
        public static void Save(PlayerData playerData)
        {
            var json = JsonConvert.SerializeObject(playerData);
            File.WriteAllText(_filePath, json);
        }

        public static PlayerData Load()
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<PlayerData>(json);
        }
    }
}