using Unity.Entities;
using UnityEngine;

namespace Sample_2_Scripts
{
    public class EntityMoverAuthoring : MonoBehaviour
    {
        public float Speed = 2f;
        
        class EntityMoverBaker : Baker<EntityMoverAuthoring>
        {
            public override void Bake(EntityMoverAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveData()
                {
                    Speed = authoring.Speed
                });
            }
        }
    }
}