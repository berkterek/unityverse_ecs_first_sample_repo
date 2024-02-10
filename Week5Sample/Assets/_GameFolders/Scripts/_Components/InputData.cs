using Unity.Entities;
using Unity.Mathematics;

namespace SampleScripts
{
    public struct InputData : IComponentData
    {
        public float3 MoveInput;
    }
}