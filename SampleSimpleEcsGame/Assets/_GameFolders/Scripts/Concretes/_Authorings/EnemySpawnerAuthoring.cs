using EcsGame.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public Transform[] Points;
        
        class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                var positionBlobBuilder = new BlobBuilder(Allocator.Temp);
                ref var positionRoot = ref positionBlobBuilder.ConstructRoot<EnemySpawnPositionBlob>();
                int length = authoring.Points.Length;
                var positionArray = positionBlobBuilder.Allocate(ref positionRoot.Values, length);

                for (int i = 0; i < length; i++)
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