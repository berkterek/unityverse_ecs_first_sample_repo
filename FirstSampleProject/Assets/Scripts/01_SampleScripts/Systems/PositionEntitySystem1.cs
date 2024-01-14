using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//using olarak Unity.Entities kullaniriz
//kendi yazdigmiz system SystemBase uzerinden miras alir ama sadece miras almasi yetmez bunun birde partial class olmasi gerekir
namespace FirstSampleProject.Sample_02_Scripts
{
    //SystemBase ecs dots'in eski system yontemi SystemBase genelde kullanim alani managed data(class, reference tip) yapilarini Mono yapilariyla hibrit yontem ile kullaniriz veya input class'inin icinde instance olusup kullanilmasi gibi
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial class PositionEntitySystem1 : SystemBase
    {
        //mono tarafindaki awake yapimiz
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        //mono tarafinda OnEnable method'uyla ayni calisir
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
        }
        
        //mono tarafinda Update gibi calisir
        protected override void OnUpdate()
        {
            //guncel sorgu yazma yontemi
            //RefRO => bizim calistigimiz tipler deger tip oldugundan dolayi onlara referans tip gibi davrandirtir ve entity uzerinde o degiskene atadimgiz entity degerini referansiyla birlikte RO okumamizi saglar ReadOnly'den gelir
            //RefRW => ayni RefRO gibi calisir refernas tasir tek farki RO ve RW okuma ve yazma islemi yapmamizi saglar
            // int i = 10;
            // int x = i;
            // x++;
            // Debug.Log(i); // 10
            // Debug.Log(x); // 11
            //
            // int[] iArray = { 1, 2, 3 };
            // int[] xArray = iArray;
            //
            // xArray[0] = 100;
            // Debug.Log(iArray[0]); //100
           
            //Query icinde butun entity'leri gezen bir sorgu atiyoruz burda ki sorguda sadece bize icinde PositionData ve PositionEntityTag olan entity'leri don demis oluyoruz
            // foreach (var (positionEntityTag, positionData) in SystemAPI.Query<RefRO<PositionEntityTag>, RefRW<PositionData>>()/*.WithAll<PositionEntityTag>()*//*.WithNone<PositionEntityTag>()*/)
            // {
            //     //ValueRO ile datayi okuruz
            //     Debug.Log(positionData.ValueRO.Position);
            //     
            //     //ValueRW
            //     positionData.ValueRW.Position = new float3(0f, 0f, 0f);
            //     Debug.Log(positionData.ValueRO.Position);
            // }
            
            //eski ecs dots icinde SystemBase bolumunde kullanilan Entity sorgusu
            Entities.WithAll<PositionEntityTag>().WithoutBurst().ForEach((ref PositionData positionData) =>
            {
                Debug.Log("SystemBase " + positionData.Position + "");
                positionData.Position = new float3(0f, 0f, 0f);
                Debug.Log("SystemBase " + positionData.Position + "");
            }).Run();
        }

        //mono tarafinda OnDisable gibi calisir
        protected override void OnStopRunning()
        {
            base.OnStopRunning();
        }

        //mono tarafinda OnDestroy gibi calisir
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    //ecs dots icinde direkt butun yapi hibrit olmadan direkt kendi ecs icinde kullandigimiz sistem ISystem'dir
    //[BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(PositionEntitySystem1))]
    public partial struct PositionEntitySystem2 : ISystem
    {
        //mono awake gibi calisir
        //[BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        //mono update gibi calisir
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (positionEntityTag, positionData) in SystemAPI.Query<RefRO<PositionEntityTag>, RefRW<PositionData>>())
            {
                Debug.Log("ISystem " + positionData.ValueRO.Position);
                positionData.ValueRW.Position = new float3(0f, 0f, 0f);
                Debug.Log("ISystem " + positionData.ValueRO.Position);
            }
        }

        //mono Destroy gibi calisir
        //[BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}

