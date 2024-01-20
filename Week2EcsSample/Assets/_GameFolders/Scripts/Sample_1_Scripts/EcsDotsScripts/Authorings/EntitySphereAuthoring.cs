using Unity.Entities;
using UnityEngine;

namespace Sample_1_Scripts
{
    public class EntitySphereAuthoring : MonoBehaviour
    {
        public float Speed = 2f;
        
        class EntitySphereBaker : Baker<EntitySphereAuthoring>
        {
            public override void Bake(EntitySphereAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<SphereTag>(entity);
                
                AddComponent(entity, new MoveData()
                {
                    Speed = authoring.Speed
                });
            }
        }
    }    
}