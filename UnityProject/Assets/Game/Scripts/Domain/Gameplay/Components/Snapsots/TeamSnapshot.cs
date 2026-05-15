using SampleGame.Common;

namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class TeamSnapshot
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