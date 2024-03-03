using Unity.Entities;

namespace EcsGame.Components
{
    public struct EnemySpawnEntityBuffer : IBufferElementData
    {
        public Entity EnemyEntity;
    }
}