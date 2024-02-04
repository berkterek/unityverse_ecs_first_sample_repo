using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Sample2
{
    public class EntityCubeSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject Prefab;
        [SerializeField] float MaxSpawnTime = 3f;
        [SerializeField] Transform[] Points;
        
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

                uint seed = (uint)new System.Random().Next(0, int.MaxValue);
                AddComponent(entity, new RandomData()
                {
                    Random = Random.CreateFromIndex(seed)
                });

                //burda bir blob asset builder gecici sureligne olusturuyoruz
                var positionBlobBuilder = new BlobBuilder(Allocator.Temp);
                
                //ardindan constructor root ile builder uzerinden root olusturuyoruz
                ref var positionRoot = ref positionBlobBuilder.ConstructRoot<SpawnPositionBlob>();

                //bu blob asset array'ini olsuturmak icin once kac length olucak onu aliyoruz
                int length = authoring.Points.Length;
                
                //positionBlobBuilder uzerinden root refernece ile bu array'i olusturuyoruz ve length'i veriyoruz
                var positionArray = positionBlobBuilder.Allocate(ref positionRoot.Values, length);

                //olusturudugumuz array uzreine mono uzerindeki collection yapisi icindeki data'yi burda pozisyon bilgisi bunlari for loop ile blob asset array icine tasiyoruz
                for (int i = 0; i < length; i++)
                {
                    float3 position = authoring.Points[i].position;
                    positionArray[i] = position;
                }

                //isleminizi ikinci kisminda ise bir tane blob asset refernce olsuyuruyoruz gene builder'i kullaniyoruz bunun allocator'i persistent yani kalicidir
                BlobAssetReference<SpawnPositionBlob> positionBlobAssetReference =
                    positionBlobBuilder.CreateBlobAssetReference<SpawnPositionBlob>(Allocator.Persistent);
                
                //olsuturdumuz asset reference'i ICompoenent data araciliyla entity uzerini atiyoruz
                AddComponent(entity, new SpawnPositionsReference()
                {
                    BlobValueReference = positionBlobAssetReference
                });
                
                //gecici sureligien olusturdugumuz builder'i ise burda dispose ediyoruz
                positionBlobBuilder.Dispose();
            }
        }
    }    
}