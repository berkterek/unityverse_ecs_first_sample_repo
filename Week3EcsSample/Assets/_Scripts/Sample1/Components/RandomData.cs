using Unity.Entities;
using Unity.Mathematics;

namespace Sample1
{
    public struct RandomData : IComponentData
    {
        public Random Random;
    }
}