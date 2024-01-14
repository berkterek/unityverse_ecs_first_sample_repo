using Unity.Entities;
using Unity.Mathematics;

namespace FirstSampleProject.Sample_02_Scripts
{
    public struct PositionData : IComponentData
    {
        public float3 Position;
    }
}