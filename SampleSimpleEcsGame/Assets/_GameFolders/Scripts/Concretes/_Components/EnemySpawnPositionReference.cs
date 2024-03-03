using Unity.Entities;
using Unity.Mathematics;

namespace EcsGame.Components
{
    public struct EnemySpawnPositionReference : IComponentData
    {
        public BlobAssetReference<EnemySpawnPositionBlob> BlobAssetReference;
    }

    public struct EnemySpawnPositionBlob
    {
        public BlobArray<float3> Values;
    }
}