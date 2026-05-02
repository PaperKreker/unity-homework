using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using UnityEngine;
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
        
        public void Save()
        {
            JObject gameData = new();
            foreach (ISaveSerializer serializer in _serializers)
            {
                gameData.Add(serializer.Key, serializer.Serialize());
            }

            string raw = gameData.ToString();
            int version = 1;
            if (PlayerPrefs.HasKey("SaveVersion"))
            {
                version = PlayerPrefs.GetInt("SaveVersion") + 1;
            }
            PlayerPrefs.SetString(GetSaveFileName(version), raw);
            PlayerPrefs.SetInt("SaveVersion", version);
            PlayerPrefs.Save();
            
            Debug.Log($"Saved file {version}");
        }

        public void LoadLatest()
        {
            if (!TryGetLatestVersion(out int version))
            {
                Debug.Log("No saves found");
                return;
            }

            Load(version);
        }
        public void Load(int version = 0)
        {
            if (!PlayerPrefs.HasKey(GetSaveFileName(version)))
            {
                Debug.Log($"Save {version} was not found");
                return;
            }
            
            string raw = PlayerPrefs.GetString(GetSaveFileName(version));
            JObject gameData = JObject.Parse(raw);
            foreach (ISaveSerializer serializer in _serializers)
            {
                if (gameData.TryGetValue(serializer.Key, out JToken entityData))
                {
                    serializer.Deserialize(entityData);
                }
            }
            Debug.Log($"Loaded file {version}");
        }

        private bool TryGetLatestVersion(out int version)
        {
            version = 1;
            if (PlayerPrefs.HasKey("SaveVersion"))
            {
                version = PlayerPrefs.GetInt("SaveVersion");
            }

            while (!PlayerPrefs.HasKey(GetSaveFileName(version)) && version > 0)
            {
                --version;
            }

            return version > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string GetSaveFileName(int version)
        {
            return $"GameData_{version}";
        }
    }
}
