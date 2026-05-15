using UnityEngine;

namespace SampleGame.SaveSystem
{
    public class SaveVersion
    {
        private const string VERSION_KEY = "SaveVersion";
        
        public static bool TryGetLatest(out int version)
        {
            version = 1;
            if (!PlayerPrefs.HasKey(VERSION_KEY)) return false;
            
            version = PlayerPrefs.GetInt(VERSION_KEY);
            return true;
        }
        
        public static int GetNext()
        {
            if (TryGetLatest(out int version))
            {
                return version + 1;
            }

            return version;
        }

        public static void RefreshLatest(int newVersion)
        {
            PlayerPrefs.SetInt(VERSION_KEY, newVersion);
            PlayerPrefs.Save();
        }
    }
}