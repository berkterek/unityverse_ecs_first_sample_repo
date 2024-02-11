using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace SampleScripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial class InputReaderSystem : SystemBase
    {
        public Vector2 Direction { get; set; }
        
        protected override void OnUpdate()
        {
            var oldDirection = Direction;
            var newDirection = new float3(oldDirection.x, 0f, oldDirection.y);

            foreach (var (playerTag, inputData) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<InputData>>())
            {
                inputData.ValueRW.MoveInput = newDirection;
            }
        }
    }
}