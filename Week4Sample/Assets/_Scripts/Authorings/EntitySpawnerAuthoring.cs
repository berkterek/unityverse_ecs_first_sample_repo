using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Sample1
{
    public class EntitySpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject[] Prefabs;
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

                uint seed = (uint)new System.Random().Next(0, int.MaxValue);
                AddComponent(entity, new RandomData()
                {
                    Random = Random.CreateFromIndex(seed)
                });

                //AddBuffer ile biz bu entity'e SpawnEntityBufferData'i eklemis olduk
                var buffer = AddBuffer<SpawnEntityBufferData>(entity);
                
                //burdaki foreach ile Prefab array icindeki datayi entity icindeki dynamic buffer data'ya tasimis olduk
                foreach (var prefab in authoring.Prefabs)
                {
                    var prefabEntity = GetEntity(prefab, TransformUsageFlags.Dynamic);
                    buffer.Add(new SpawnEntityBufferData()
                    {
                        Entity = prefabEntity
                    });
                }
            }
        }
    }    
}

