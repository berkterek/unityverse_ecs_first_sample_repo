using EcsGame.Components;
using Unity.Entities;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct PlayerVisualMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerTag, localTransform,entity) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<LocalTransform>>().WithEntityAccess())
            {
                if(!SystemAPI.ManagedAPI.HasComponent<PlayerVisualReferenceData>(entity)) continue;

                var playerVisualReferenceData = SystemAPI.ManagedAPI.GetComponent<PlayerVisualReferenceData>(entity);
                playerVisualReferenceData.Reference.SetPosition(localTransform.ValueRO.Position);
            }
        }
    }
}