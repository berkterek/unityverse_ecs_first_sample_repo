using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    //[BurstCompile]
    public partial struct CubeMoveSystem : ISystem
    {
        //burda random bir kere olusur ve bu sistem yok olmadigi surece random'da hayatina devam eder ve verdigimzi seed sayisian gore yok olana kadar bize random farkli sayilar doner ama burda ise seed sayisi hep ayni oldugundan dolayi ac kapat yaptigimizda gene ayni sayilar bzie random olarak gelir
        //Random _random;
        
        //[BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //_random = Random.CreateFromIndex(1); // seed 1 => 5, 10, 500, -250
                                                 // seed 1 => 5, 10, 500, -250
                                                 // seed 2 => 2, -120, 540, 25000
                                                 // seed 2 => 2, -120, 540, 25000
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            //Random burda her seferinde bastan olsutugu icin ve ayni seed tohum sayisiyla olusutguu icin bize ilk 5 random sayisini hep ayni doner ama seed sayisini 1 veya 2 yaptigimdizda ise ilk 5 sayi farkli seed oldugunda dolayi farkli 5 sayi doner ama ayni sekilde yok olup tekrar ayni seed ile olsuturuldugudna dolayi gene ayni 5 farkli sayiyi doner bize.
            //Random random = Random.CreateFromIndex(2);
            foreach (var (moveData, entityTag, localTransform, randomData) in SystemAPI.Query<RefRO<MoveData>, RefRO<CubeEntityTag>, RefRW<LocalTransform>, RefRW<RandomData>>())
            {
                //var randomDestination = _random.NextFloat3();
                //var randomDestination = randomData.ValueRW.Random.NextFloat3();
                var randomDestination = new float3(randomData.ValueRW.Random.NextFloat(),
                    randomData.ValueRW.Random.NextFloat(), randomData.ValueRW.Random.NextFloat());
                for (int i = 0; i < 5; i++)
                {
                    randomDestination = randomData.ValueRW.Random.NextFloat3();
                    Debug.Log(randomDestination);
                }

                if (math.distance(randomDestination, localTransform.ValueRO.Position) < 0.1f) return;
                
                 var direction = math.normalize(randomDestination - localTransform.ValueRW.Position);
                // Debug.Log(direction);

                localTransform.ValueRW.Position += moveData.ValueRO.Speed * deltaTime * direction;
            }
        }
    }    
}