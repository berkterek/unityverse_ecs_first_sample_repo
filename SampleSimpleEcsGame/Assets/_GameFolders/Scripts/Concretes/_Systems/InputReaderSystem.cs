using EcsGame.Abstracts.Inputs;
using EcsGame.Components;
using EcsGame.Inputs;
using Unity.Entities;
using Unity.Mathematics;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial class InputReaderSystem : SystemBase
    {
        IInputReader _inputReader;

        protected override void OnCreate()
        {
            _inputReader = new InputReader();
        }

        protected override void OnUpdate()
        {
            foreach (var (playerTag, inputData) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<InputData>>())
            {
                var newDirection = new float3(_inputReader.Direction.x, 0f, _inputReader.Direction.y);
                inputData.ValueRW.Direction = newDirection;
            }
        }
    }
}