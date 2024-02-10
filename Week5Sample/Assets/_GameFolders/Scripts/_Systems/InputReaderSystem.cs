using Unity.Entities;
using Unity.Mathematics;

namespace SampleScripts
{
    public partial class InputReaderSystem : SystemBase
    {
        IInputReader _inputReader;

        protected override void OnCreate()
        {
            _inputReader = new InputReader();
        }

        protected override void OnUpdate()
        {
            var oldDirection = _inputReader.Direction;
            var newDirection = new float3(oldDirection.x, 0f, oldDirection.y);

            foreach (var (playerTag, inputData) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<InputData>>())
            {
                inputData.ValueRW.MoveInput = newDirection;
            }
        }
    }
}