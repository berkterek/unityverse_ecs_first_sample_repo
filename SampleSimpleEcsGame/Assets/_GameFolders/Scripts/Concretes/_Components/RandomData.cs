using Unity.Entities;
using Unity.Mathematics;

namespace EcsGame.Components
{
    public struct RandomData : IComponentData
    {
        public Random RandomValue;
    }
}