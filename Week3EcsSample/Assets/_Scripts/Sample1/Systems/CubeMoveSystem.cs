using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    //BurstCompile bizim kodlarimizin daha hizli calismaisini saglar ve sadece struct yapilarinda burst acabilirz class yapilarinda acamayiz eger class yapilarinda acmaya calisirsak bize uyari verir ya class sil yada burst kapa diye uyari verir. [BurstCompile] ile struct ve calismaisni istedigimiz method'larin basina yazariz kendisi bir attribute tur ve basina yazmaamiz yeterlidir normal sartlarda sadece struct basina yazdigimizda otomatik butum method'lara dagitmasi gerekir ama bu yapi normal bir yapi olmadigindan dolayi ve butun yapilan orneklerde struct basina ve tum calismaisni istedigmiz method'larin basina [BurstCompile] yazilir 
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct CubeMoveSystem : ISystem
    {
        //burda random bir kere olusur ve bu sistem yok olmadigi surece random'da hayatina devam eder ve verdigimzi seed sayisian gore yok olana kadar bize random farkli sayilar doner ama burda ise seed sayisi hep ayni oldugundan dolayi ac kapat yaptigimizda gene ayni sayilar bzie random olarak gelir
        //Random _random;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //_random = Random.CreateFromIndex(1); // seed 1 => 5, 10, 500, -250
            // seed 1 => 5, 10, 500, -250
            // seed 2 => 2, -120, 540, 25000
            // seed 2 => 2, -120, 540, 25000
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            //Random burda her seferinde bastan olsutugu icin ve ayni seed tohum sayisiyla olusutguu icin bize ilk 5 random sayisini hep ayni doner ama seed sayisini 1 veya 2 yaptigimdizda ise ilk 5 sayi farkli seed oldugunda dolayi farkli 5 sayi doner ama ayni sekilde yok olup tekrar ayni seed ile olsuturuldugudna dolayi gene ayni 5 farkli sayiyi doner bize.
            //Random random = Random.CreateFromIndex(2);
            // foreach (var (moveData, entityTag, localTransform, randomData) in SystemAPI.Query<RefRO<MoveData>, RefRO<CubeEntityTag>, RefRW<LocalTransform>, RefRW<RandomData>>())
            // {
            //     //var randomDestination = _random.NextFloat3();
            //     //var randomDestination = randomData.ValueRW.Random.NextFloat3();
            //     var randomDestination = new float3(randomData.ValueRW.Random.NextFloat(),
            //         randomData.ValueRW.Random.NextFloat(), randomData.ValueRW.Random.NextFloat());
            //     for (int i = 0; i < 5000; i++)
            //     {
            //         randomDestination = randomData.ValueRW.Random.NextFloat3();
            //         //Debug.Log(randomDestination);
            //     }
            //
            //     if (math.distance(randomDestination, localTransform.ValueRO.Position) < 0.1f) return;
            //     
            //      var direction = math.normalize(randomDestination - localTransform.ValueRW.Position);
            //     //Debug.Log(direction);
            //
            //     localTransform.ValueRW.Position += moveData.ValueRO.Speed * deltaTime * direction;
            // }

            var job = new CubeMoveJob()
            {
                DeltaTime = deltaTime
            };

            //Run main thread uzerinde calisir
            //Schedule main thread disinda ayri bir cekirdek veya worker uzerinde calisir eger uc kere arka arkaya caliscaksa veya tum islerini bir worker'a yukler islem bitince main thread'e doner 
            //ScheduleParallel ise main thread'den ayri bir cekirdek uzerinde calir schedule'dan farki bir worker uzerinde degil bircok worker uzeridne calisir islem bitince main thread'e doner
            // job.Run();
            //job.Schedule();
            job.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CubeMoveJob : IJobEntity
    {
        public float DeltaTime;

        //RefRO => in
        //RefRW => ref
        //SystemAPI.Query<RefRO<MoveData>, RefRO<CubeEntityTag>, RefRW<LocalTransform>, RefRW<RandomData>>()
        [BurstCompile]
        private void Execute(in MoveData moveData, in CubeEntityTag cubeEntityTag, ref LocalTransform localTransform,
            ref RandomData randomData)
        {
            var randomDestination = new float3(randomData.Random.NextFloat(),
                randomData.Random.NextFloat(), randomData.Random.NextFloat());
            for (int i = 0; i < 5000; i++)
            {
                randomDestination = randomData.Random.NextFloat3();
            }

            if (math.distance(randomDestination, localTransform.Position) < 0.1f) return;

            var direction = math.normalize(randomDestination - localTransform.Position);

            localTransform.Position += moveData.Speed * DeltaTime * direction;
        }
    }
}