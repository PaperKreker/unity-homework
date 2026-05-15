using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace SampleGame.SaveSystem
{
    public interface IRepository
    {
        public UniTask<string> LoadFile(int version);
        public UniTask<bool> SaveFile(int version, string data);
    }
}