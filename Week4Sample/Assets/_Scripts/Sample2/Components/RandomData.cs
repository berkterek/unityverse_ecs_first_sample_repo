using Unity.Entities;
using Unity.Mathematics;

namespace Sample2
{
    public struct RandomData : IComponentData
    {
        public Random Random;
    }
}