using Unity.Entities;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct SoldierVisualMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (soldierTag,localTransform, entity) in SystemAPI.Query<RefRO<SoldierTag>, RefRO<LocalTransform>>().WithEntityAccess())
            {
                if(!SystemAPI.ManagedAPI.HasComponent<SoldierVisualReferenceData>(entity)) continue;

                var soldierVisualReferenceData = SystemAPI.ManagedAPI.GetComponent<SoldierVisualReferenceData>(entity);
                soldierVisualReferenceData.Reference.SetPositionAndRotation(localTransform.ValueRO.Position,localTransform.ValueRO.Rotation);
            }
        }
    }
}