using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks; // Не забудьте добавить namespace
using UnityEngine;
using UnityEngine.Networking;

namespace SampleGame.SaveSystem
{
    public static class WebRepository
    {
        private const string SERVER_URL = "http://127.0.0.1:8888";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<string> DownloadSaveFile(int version)
        {
            return await SendGetRequest($"{SERVER_URL}/load?version={version}");
        }

        private static async UniTask<string> SendGetRequest(string url)
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
        public static async UniTask<UnityWebRequest.Result> UploadSaveFile(int version, string data)
        {
            return await SendPutRequest($"{SERVER_URL}/save?version={version}", data);
        }

        private static async UniTask<UnityWebRequest.Result> SendPutRequest(string url, string data)
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