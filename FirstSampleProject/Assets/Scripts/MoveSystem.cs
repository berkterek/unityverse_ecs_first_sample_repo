using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DefaultNamespace
{
    public partial class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (moveData, localTransform) in SystemAPI.
                         Query<RefRO<MoveData>, RefRW<LocalTransform>>())
            {
                var forward = new float3(0f, 0f, 1f);
                
                localTransform.ValueRW.Position += moveData.ValueRO.MoveSpeed * deltaTime * forward;
            }
        }
    }
}