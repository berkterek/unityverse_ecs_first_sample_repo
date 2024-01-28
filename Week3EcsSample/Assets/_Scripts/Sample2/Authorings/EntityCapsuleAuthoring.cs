using Unity.Entities;
using UnityEngine;

namespace Sample2
{
    public class EntityCapsuleAuthoring : MonoBehaviour
    {
        private class EntityCapsuleBaker : Baker<EntityCapsuleAuthoring>
        {
            public override void Bake(EntityCapsuleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<MoveTag>(entity);
                AddComponent<AttackTag>(entity);
                
                AddComponent(entity, new MoveData
                {
                    MoveSpeed = 1.5f
                });
                
                AddComponent(entity, new AttackData()
                {
                    Damage = 1f,
                    Timer = 0f
                });
            }
        }
    }    
}