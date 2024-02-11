using Unity.Entities;
using Unity.Transforms;

namespace SampleScripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct EnemyVisualMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (localTransform, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithEntityAccess())
            {
                if (SystemAPI.ManagedAPI.HasComponent<EnemyVisualObjectReference>(entity))
                {
                    var enemyVisualObjectReference = SystemAPI.ManagedAPI.GetComponent<EnemyVisualObjectReference>(entity);
                    enemyVisualObjectReference.EnemyVisualController.SetPosition(localTransform.ValueRO.Position);
                }
            }
        }
    }
}