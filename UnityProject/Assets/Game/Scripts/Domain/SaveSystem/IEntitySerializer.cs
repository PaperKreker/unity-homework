using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;

namespace SampleGame.SaveSystem
{
    public interface IEntitySerializer
    {
        JToken Serialize(Countdown countdown);
        void Deserialize(Countdown countdown, JToken token);
        
        JToken Serialize(DestinationPoint countdown);
        void Deserialize(DestinationPoint countdown, JToken token);
        
        JToken Serialize(Health health);
        void Deserialize(Health health, JToken token);
        
        JToken Serialize(ProductionOrder productionOrder);
        void Deserialize(ProductionOrder productionOrder, JToken token);

        JToken Serialize(ResourceBag resourceBag);
        void Deserialize(ResourceBag resourceBag, JToken token);

        JToken Serialize(TargetObject targetObject);
        void Deserialize(TargetObject targetObject, JToken token);

        JToken Serialize(Team team);
        void Deserialize(Team team, JToken token);

    }
}