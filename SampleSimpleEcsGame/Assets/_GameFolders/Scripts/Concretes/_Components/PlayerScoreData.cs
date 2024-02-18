using Unity.Entities;

namespace EcsGame.Components
{
    public struct PlayerScoreData : IComponentData
    {
        public int Score;
    }
}