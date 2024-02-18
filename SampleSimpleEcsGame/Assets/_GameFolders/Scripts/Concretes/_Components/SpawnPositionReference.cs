using Unity.Entities;
using Unity.Mathematics;

namespace EcsGame.Components
{
    public struct SpawnPositionReference : IComponentData
    {
        public BlobAssetReference<SpawnPositionBlob> BlobAssetReference;
    }

    public struct SpawnPositionBlob
    {
        public BlobArray<float3> Values;
    }
}