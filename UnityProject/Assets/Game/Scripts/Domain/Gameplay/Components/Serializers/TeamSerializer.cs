using System.Collections.Generic;
using Modules.Entities;
using SampleGame.Common;
using SampleGame.SaveSystem;
using Zenject;

namespace SampleGame.Gameplay.Serializers
{
    public readonly struct TeamSerializer : ISaveSerializer<TeamSerializer.Snapshot>
    {
        public string Key => "Team";
        private readonly Team _team;

        public TeamSerializer(Team team)
        {
            _team = team;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_team);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(_team);
        }
        
        public struct Snapshot
        {
            public TeamType Team;

            public Snapshot(Team team)
            {
                Team = team.Type;
            }

            public void Restore(Team team)
            {
                team.Type = Team;
            }
        }
    }
}