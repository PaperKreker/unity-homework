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
        private readonly ISerializer[] _serializers;
        private readonly IRepository _repository;

        public SaveManager(ISerializer[] serializers, IRepository repository)
        {
            _serializers = serializers;
            _repository = repository;
        }
        
        public async UniTaskVoid Save(Action<bool, int> callback)
        {
            JObject gameData = new();
            foreach (ISerializer serializer in _serializers)
            {
                gameData.Add(serializer.Key, serializer.Serialize());
            }

            string raw = gameData.ToString();
            Debug.Log(raw);
            if (SaveVersion.TryGetLatest(out int version))
            {
                ++version;
            }

            bool isSuccess = await _repository.SaveFile(version, raw);
            if (isSuccess)
            {
                SaveVersion.RefreshLatest(version);
                callback(true, version);
            }
            else
            {
                callback(false, -1);
            }
        }
        
        public async UniTaskVoid Load(Action<bool, int> callback, int version = 0)
        {
            if (version == 0 && !SaveVersion.TryGetLatest(out version))
            {
                Debug.Log("No saves found");
                callback(false, -1);
                return;
            }
            
            string raw = await _repository.LoadFile(version);
            if (raw == null || raw == "")
            {
                Debug.LogError($"Save {version} was not found");
                callback(false, version);
                return;
            }
            
            JObject gameData = JObject.Parse(raw);
            foreach (ISerializer serializer in _serializers)
            {
                if (gameData.TryGetValue(serializer.Key, out JToken entityData))
                {
                    serializer.Deserialize(entityData);
                }
            }
            callback(true, version);
        }
    }
}
