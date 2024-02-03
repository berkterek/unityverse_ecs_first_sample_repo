using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Sample1
{
    public class EntityMoverAuthoring : MonoBehaviour
    {
        class EntityMoverBaker : Baker<EntityMoverAuthoring>
        {
            public override void Bake(EntityMoverAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveData()
                {
                    Speed = 2f,
                    Direction = new float3(0f,0f,1f)
                });
            }
        }
    }    
}