using Unity.Burst;
using Unity.Entities;

namespace Sample1
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial struct AnimationPlaySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (soldierTag,moveData,entity) in SystemAPI.Query<RefRO<SoldierTag>, RefRO<MoveData>>().WithEntityAccess())
            {
                if(!SystemAPI.ManagedAPI.HasComponent<SoldierVisualReferenceData>(entity)) continue;

                var soldierVisualReferenceData = SystemAPI.ManagedAPI.GetComponent<SoldierVisualReferenceData>(entity);
                soldierVisualReferenceData.Reference.MoveAnimation(moveData.ValueRO.Velocity);
            }
        }
    }
}