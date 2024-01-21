using Unity.Entities;
using UnityEngine;

namespace Sample_2_Scripts
{
    public class SpawnerEntityAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float MaxTime = 3f;
        
        class SpawnerEntityBaker : Baker<SpawnerEntityAuthoring>
        {
            public override void Bake(SpawnerEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new SpawnEntityData()
                {
                    Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });

                AddComponent(entity, new SpawnerTimeData()
                {
                    MaxTime = authoring.MaxTime,
                    CurrentTime = 0f
                });
            }
        }
    }    
}