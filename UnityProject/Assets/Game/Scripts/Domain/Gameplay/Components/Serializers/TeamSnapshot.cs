using SampleGame.Common;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class TeamSnapshot : ISnapshot<Team>
    {
        public TeamType Team;

        public void Save(Team team)
        {
            Team = team.Type;
        }

        public void Restore(Team team)
        {
            team.Type = Team;
        }
    }
}