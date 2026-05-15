using Newtonsoft.Json.Linq;

namespace SampleGame.SaveSystem
{
    public interface ISerializer
    {
        public string Key { get; }
        public JToken Serialize();
        public void Deserialize(JToken data);
    }
    
    public interface ISerializer<T> : ISerializer
    {
        JToken ISerializer.Serialize() => JToken.FromObject(this.Serialize());
        void ISerializer.Deserialize(JToken data) => this.Deserialize(data.ToObject<T>());
        
        public new T Serialize();
        public void Deserialize(T data);
    }
}