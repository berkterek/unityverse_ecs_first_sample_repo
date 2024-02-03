using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public class EntitySpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject Prefab;
        [SerializeField] float MaxSpawnTime = 3f;
        [SerializeField] Vector3 SpawnPosition;
        
        class EntitySpawnerBaker : Baker<EntitySpawnerAuthoring>
        {
            public override void Bake(EntitySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new SpawnData()
                {
                    CurrentTime = 0f,
                    MaxSpawnTime = authoring.MaxSpawnTime,
                    SpawnPosition = authoring.SpawnPosition
                });
                
                AddComponent(entity, new SpawnEntityData()
                {
                    Entity = GetEntity(authoring.Prefab,TransformUsageFlags.Dynamic)
                });
            }
        }
    }    
}

