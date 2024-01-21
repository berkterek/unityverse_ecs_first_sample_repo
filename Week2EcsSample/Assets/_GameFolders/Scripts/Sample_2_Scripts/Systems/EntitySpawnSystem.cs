using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Sample_2_Scripts
{
    //OrderLast = true ile biz bu system group icinde en son caliscak olan system olarak bunu set'lemis olduk
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial struct EntitySpawnSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            //EntityCommandBuffer ecs dots yaklasimda paralel yaklasimlar oldugundan dolayi add component set component veya yeni spawner ile yeni bir entity olusturmak datayi kaybetmeden direkt dogru bir sekilde main thread'e gondermemiz gerekir ve direkt Update icinde bu islemleri direkt kullanmak cok dogru degildir bunun yerine ister paralel ister normal Run veya Update icinde tavsiye edilen yontem EntityCommandBuffer'dir bu bizim icin islemleri bir anlik veya kalici bir memory tampon bolgeisi acar islemleri direkt main thread uzeride degil once bu memory doldurur ornegin yeni entity olusrumak gibi ve bu memory aliani Update islemini bitirince direkt memory icindeki tum islemleri tek seferde calistirir ve main thread uzeride dogru ve tek seferde calistirir isi bitmis olan entity command buffer'i ise dispose ederiz memory leak olmamamsi icin
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (spawnEntityData, timeData, localTransform) in SystemAPI
                         .Query<RefRO<SpawnEntityData>, RefRW<SpawnerTimeData>, RefRO<LocalTransform>>())
            {
                timeData.ValueRW.CurrentTime += deltaTime;

                if (timeData.ValueRO.CurrentTime < timeData.ValueRO.MaxTime) return;

                timeData.ValueRW.CurrentTime = 0f;

                for (int i = 0; i < 10; i++)
                {
                    var newEntity = entityCommandBuffer.Instantiate(spawnEntityData.ValueRO.Entity);
                    entityCommandBuffer.SetComponent(newEntity, new LocalTransform()
                    {
                        Position = localTransform.ValueRO.Position,
                        Rotation = localTransform.ValueRO.Rotation,
                        Scale = localTransform.ValueRO.Scale
                    });    
                }
            }
            
            //playback method'u ile bu yapilan islemleri calisitir demis olduk
            entityCommandBuffer.Playback(state.EntityManager);
            //dispose ile memory leak olmamasi icin bu method'u caligiriyoruz
            entityCommandBuffer.Dispose();
        }

        private void NotRecommendedWay()
        {
            // foreach (var (spawnEntityData, timeData, localTransform) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnerTimeData>, RefRO<LocalTransform>>())
            // {
            //     //current time burda gelen delta time kadar arttir demis olduk
            //     timeData.ValueRW.CurrentTime += deltaTime;
            //
            //     //burda bir if check yaptik eger max time'dan kucukse current time return et bir islem yapma demis olduk
            //     if (timeData.ValueRO.CurrentTime < timeData.ValueRO.MaxTime) return;
            //
            //     //eger current time max time'dan buyukse onu tekrar if sorgusuna sokmak icin 0 degeirni atadik
            //     timeData.ValueRW.CurrentTime = 0f;
            //
            //     //state.EntityManager ile bir spawnEntityData component'i icinden gelen bir entity'i spawn et demis olduk
            //     var newEntity = state.EntityManager.Instantiate(spawnEntityData.ValueRO.Entity);
            //     
            //     //newEntity yani olusan yeni entity'inin localtransfomr pozisyon rotasyon ve scale bilgilerine spawner'in kendi localTransform bilgilerini entity manager araciliyla atamis olduk
            //     state.EntityManager.SetComponentData(newEntity, new LocalTransform()
            //     {
            //         Position = localTransform.ValueRO.Position,
            //         Rotation = localTransform.ValueRO.Rotation,
            //         Scale = localTransform.ValueRO.Scale
            //     });
            // }
        }
    }
}