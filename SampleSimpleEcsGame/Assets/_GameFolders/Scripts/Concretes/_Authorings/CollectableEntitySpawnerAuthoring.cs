using EcsGame.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace EcsGame.Authorings
{
    public class CollectableEntitySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float MaxSpawnTime = 3f;
        public Transform[] Points;
        
        class CollectableEntitySpawnerBaker : Baker<CollectableEntitySpawnerAuthoring>
        {
            public override void Bake(CollectableEntitySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                uint seed = (uint)new System.Random().Next(0, int.MaxValue);
                AddComponent(entity, new RandomData()
                {
                    RandomValue = Random.CreateFromIndex(seed)
                });
                
                AddComponent(entity, new SpawnData()
                {
                    Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    MaxSpawnTime = authoring.MaxSpawnTime,
                    CurrentSpawnTime = authoring.MaxSpawnTime
                });

                var positionBlobBuilder = new BlobBuilder(Allocator.Temp);
                ref var positionRoot = ref positionBlobBuilder.ConstructRoot<SpawnPositionBlob>();
                int length = authoring.Points.Length;
                var positionArray = positionBlobBuilder.Allocate(ref positionRoot.Values, length);

                for (int i = 0; i < length; i++)
                {
                    var position = authoring.Points[i].position;
                    positionArray[i] = position;
                }

                BlobAssetReference<SpawnPositionBlob> positionBlobAssetReference =
                    positionBlobBuilder.CreateBlobAssetReference<SpawnPositionBlob>(Allocator.Persistent);

                AddComponent(entity, new SpawnPositionReference()
                {
                    BlobAssetReference = positionBlobAssetReference
                });
                
                positionBlobBuilder.Dispose();
            }
        }
    }
}
