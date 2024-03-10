using Unity.Entities;

namespace EcsGame.Components
{
    public struct GameStatusData : IComponentData
    {
        public bool IsGameEnded;
    }

    public struct GameLevelData : IComponentData
    {
        public int CurrentLevel;
        public int MaxLevel;
    }
}