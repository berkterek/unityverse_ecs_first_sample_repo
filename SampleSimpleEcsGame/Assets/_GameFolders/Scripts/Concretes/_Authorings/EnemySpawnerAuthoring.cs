using EcsGame.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public float MaxTime;
        public int MaxCount;
        public GameObject[] Prefabs;
        public Transform[] Points;
        
        class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent<GameOverDestroyTag>(entity);
                AddComponent<EnemySpawnData>(entity, new()
                {
                    CurrentCount = 0,
                    CurrentTime = 0f,
                    MaxCount = authoring.MaxCount,
                    MaxTime = authoring.MaxTime
                });
                
                uint seed = (uint)new System.Random().Next(0, int.MaxValue);
                AddComponent(entity, new RandomData()
                {
                    RandomValue = Unity.Mathematics.Random.CreateFromIndex(seed)
                });

                var buffer = AddBuffer<EnemySpawnEntityBuffer>(entity);
                int enemyLength = authoring.Prefabs.Length;
                for (int i = 0; i < enemyLength; i++)
                {
                    buffer.Add(new EnemySpawnEntityBuffer()
                    {
                        EnemyEntity = GetEntity(authoring.Prefabs[i],TransformUsageFlags.Dynamic)
                    });
                }

                var positionBlobBuilder = new BlobBuilder(Allocator.Temp);
                ref var positionRoot = ref positionBlobBuilder.ConstructRoot<EnemySpawnPositionBlob>();
                int pointsLength = authoring.Points.Length;
                var positionArray = positionBlobBuilder.Allocate(ref positionRoot.Values, pointsLength);

                for (int i = 0; i < pointsLength; i++)
                {
                    var position = authoring.Points[i].position;
                    positionArray[i] = position;
                }

                BlobAssetReference<EnemySpawnPositionBlob> positionAssetReference =
                    positionBlobBuilder.CreateBlobAssetReference<EnemySpawnPositionBlob>(Allocator.Persistent);

                AddComponent(entity, new EnemySpawnPositionReference()
                {
                    BlobAssetReference = positionAssetReference
                });
                
                positionBlobBuilder.Dispose();
            }
        }
    }    
}