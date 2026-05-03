using System;
using System.Net;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace SampleGame.SaveSystem
{
    public class SaveManager
    {
        private readonly ISaveSerializer[] _serializers;

        [Inject]
        public SaveManager(ISaveSerializer[] serializers)
        {
            _serializers = serializers;
        }
        
        public async UniTaskVoid Save(Action<bool, int> callback)
        {
            JObject gameData = new();
            foreach (ISaveSerializer serializer in _serializers)
            {
                gameData.Add(serializer.Key, serializer.Serialize());
            }

            string raw = gameData.ToString();
            if (TryGetLatestVersion(out int version))
            {
                ++version;
            }

            var result = await WebRepository.UploadSaveFile(version, raw);
            if (result == UnityWebRequest.Result.Success)
            {
                PlayerPrefs.SetInt("SaveVersion", version);
                PlayerPrefs.Save();

                callback(true, version);
            }
            else
            {
                callback(false, -1);
            }
        }
        
        public async UniTaskVoid Load(Action<bool, int> callback, int version = 0)
        {
            if (version == 0 && !TryGetLatestVersion(out version))
            {
                Debug.Log("No saves found");
                callback(false, -1);
                return;
            }
            
            string raw = await WebRepository.DownloadSaveFile(version);
            if (raw == null || raw == "")
            {
                Debug.LogError($"Save {version} was not found");
                callback(false, version);
                return;
            }
            
            JObject gameData = JObject.Parse(raw);
            foreach (ISaveSerializer serializer in _serializers)
            {
                if (gameData.TryGetValue(serializer.Key, out JToken entityData))
                {
                    serializer.Deserialize(entityData);
                }
            }
            callback(true, version);
        }

        

        public static bool TryGetLatestVersion(out int version)
        {
            version = 1;
            if (!PlayerPrefs.HasKey("SaveVersion")) return false;
            
            version = PlayerPrefs.GetInt("SaveVersion");
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetSaveFileName(int version)
        {
            return $"GameData_{version}";
        }
    }
}
