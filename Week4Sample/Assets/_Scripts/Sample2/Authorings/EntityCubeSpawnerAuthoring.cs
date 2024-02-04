using Unity.Entities;
using UnityEngine;

namespace Sample2
{
    public class EntityCubeSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject Prefab;
        [SerializeField] float MaxSpawnTime = 3f;
        
        class EntityCubeSpawnerBaker : Baker<EntityCubeSpawnerAuthoring>
        {
            public override void Bake(EntityCubeSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new SpawnTimeData()
                {
                    MaxSpawnTime = authoring.MaxSpawnTime,
                    CurrentSpawnTime = authoring.MaxSpawnTime
                });

                AddComponent(entity, new SpawnEntityData()
                {
                    Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }    
}