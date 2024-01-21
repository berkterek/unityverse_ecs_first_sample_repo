using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample_2_Scripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct CubeMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            //RequireForUpdate method'u singleton yapilarinda kullandigmiz bir yaklasimdir burda eger singleton kullaniyorsak ve aradigimiz component veya system neyse sahne uzerinde bulabliyorsa ozaman bu Update islemini calistir demis oluyoruz
            state.RequireForUpdate<MoveData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            float3 direction = new float3(0f, 0f, 1f);
            foreach (var (moveData, localTransform) in SystemAPI.Query<RefRO<MoveData>, RefRW<LocalTransform>>())
            {
                localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.Speed * direction;
            }
        }

        private void SingletonEntityProcess(float deltaTime, float3 direction)
        {
            // var entity = SystemAPI.GetSingletonEntity<MoveData>();
            // var localTransform = SystemAPI.GetComponent<LocalTransform>(entity);
            // var moveData = SystemAPI.GetComponent<MoveData>(entity);
            //
            // localTransform.Position += moveData.Speed * deltaTime * direction;
            // SystemAPI.SetComponent(entity, localTransform);
        }
    }
}