using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public class EntitySampleAuthoring : MonoBehaviour
    {
        private class EntitySampleBaker : Baker<EntitySampleAuthoring>
        {
            public override void Bake(EntitySampleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new EntitySampleData()
                {
                    Number = 0
                });
            }
        }
    }    
}