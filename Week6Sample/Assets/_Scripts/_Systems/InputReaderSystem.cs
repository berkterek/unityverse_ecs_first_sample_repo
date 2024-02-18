using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial class InputReaderSystem : SystemBase
    {
        IInputReader _inputReader;

        protected override void OnCreate()
        {
            _inputReader = new InputReader();
        }

        protected override void OnUpdate()
        {
            foreach (var inputData in SystemAPI.Query<RefRW<InputData>>())
            {
                var newDirection = new float3(_inputReader.Direction.x, 0f, _inputReader.Direction.y);
                inputData.ValueRW.Direction = newDirection;
            }           
        }
    }
}