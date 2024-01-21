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
            foreach (var (spawnEntityData, timeData, localTransform) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnerTimeData>, RefRO<LocalTransform>>())
            {
                //current time burda gelen delta time kadar arttir demis olduk
                timeData.ValueRW.CurrentTime += deltaTime;

                //burda bir if check yaptik eger max time'dan kucukse current time return et bir islem yapma demis olduk
                if (timeData.ValueRO.CurrentTime < timeData.ValueRO.MaxTime) return;

                //eger current time max time'dan buyukse onu tekrar if sorgusuna sokmak icin 0 degeirni atadik
                timeData.ValueRW.CurrentTime = 0f;

                //state.EntityManager ile bir spawnEntityData component'i icinden gelen bir entity'i spawn et demis olduk
                var newEntity = state.EntityManager.Instantiate(spawnEntityData.ValueRO.Entity);
                
                //newEntity yani olusan yeni entity'inin localtransfomr pozisyon rotasyon ve scale bilgilerine spawner'in kendi localTransform bilgilerini entity manager araciliyla atamis olduk
                state.EntityManager.SetComponentData(newEntity, new LocalTransform()
                {
                    Position = localTransform.ValueRO.Position,
                    Rotation = localTransform.ValueRO.Rotation,
                    Scale = localTransform.ValueRO.Scale
                });
            }
        }
    }
}