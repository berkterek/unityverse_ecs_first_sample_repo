using Unity.Entities;

namespace EcsGame.Components
{
    public struct GameStatusData : IComponentData
    {
        public bool IsGameEnded;
    }
}