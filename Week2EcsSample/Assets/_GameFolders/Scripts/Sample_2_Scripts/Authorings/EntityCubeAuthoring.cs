using Unity.Entities;
using UnityEngine;

namespace Sample_2_Scripts
{
    public class EntityCubeAuthoring : MonoBehaviour
    {
        public float Speed = 2f;
        
        class EntityCubeBaker : Baker<EntityCubeAuthoring>
        {
            public override void Bake(EntityCubeAuthoring authoring)
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