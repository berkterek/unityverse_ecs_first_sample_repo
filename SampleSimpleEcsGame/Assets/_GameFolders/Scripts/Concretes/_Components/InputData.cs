using Unity.Entities;
using Unity.Mathematics;

namespace EcsGame.Components
{
    public struct InputData : IComponentData
    {
        public float3 Direction;
    }
}