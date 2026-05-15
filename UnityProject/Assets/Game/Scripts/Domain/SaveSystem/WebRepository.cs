using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks; // Не забудьте добавить namespace
using UnityEngine;
using UnityEngine.Networking;

namespace SampleGame.SaveSystem
{
    public class WebRepository : IRepository
    {
        private const string SERVER_URL = "http://127.0.0.1:8888";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask<string> LoadFile(int version)
        {
            return await SendGetRequest($"{SERVER_URL}/load?version={version}");
        }

        private async UniTask<string> SendGetRequest(string url)
        {
            using var webRequest = UnityWebRequest.Get(url);
            
            try
            {
                await webRequest.SendWebRequest();
            }
            catch (UnityWebRequestException e)
            {
                Debug.LogError($"Error: {e.Message}");
                return null;
            }

            return webRequest.downloadHandler.text;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async UniTask<bool> SaveFile(int version, string data)
        {
            UnityWebRequest.Result result = await SendPutRequest($"{SERVER_URL}/save?version={version}", data);
            return result  == UnityWebRequest.Result.Success;
        }

        private async UniTask<UnityWebRequest.Result> SendPutRequest(string url, string data)
        {
            using var webRequest = UnityWebRequest.Put(url, data);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            try
            {
                await webRequest.SendWebRequest();
                return UnityWebRequest.Result.Success;
            }
            catch (UnityWebRequestException e)
            {
                Debug.LogError($"Error: {e.Message}");
                return e.Result;
            }
        }
    }
}