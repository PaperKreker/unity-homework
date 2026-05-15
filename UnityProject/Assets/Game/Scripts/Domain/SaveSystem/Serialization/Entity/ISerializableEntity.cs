using Newtonsoft.Json.Linq;

namespace SampleGame.SaveSystem
{
    public interface ISerializableEntity
    {
        string Key { get; }
        JToken Serialize(IEntitySerializer serializer);
        void Deserialize(IEntitySerializer serializer, JToken token);
    }
}