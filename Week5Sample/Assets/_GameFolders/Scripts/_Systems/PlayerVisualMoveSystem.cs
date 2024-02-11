using Unity.Entities;
using Unity.Transforms;

namespace SampleScripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerVisualMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (localTransform, playerTag, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerTag>>().WithEntityAccess())
            {
                if (!SystemAPI.ManagedAPI.HasComponent<PlayerVisualReference>(entity)) continue;

                var playerVisualReference = SystemAPI.ManagedAPI.GetComponent<PlayerVisualReference>(entity);
                playerVisualReference.PlayerVisualController.SetPosition(localTransform.ValueRO.Position);
            }
        }
    }
}