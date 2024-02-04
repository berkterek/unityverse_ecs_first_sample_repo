using Unity.Entities;
using Unity.Mathematics;

namespace Sample2
{
    public struct MoveData : IComponentData
    {
        public float Speed;
        public float3 Direction;
    }    
}

