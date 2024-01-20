using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample_1_Scripts
{
    public readonly partial struct SphereMovementAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRO<MoveData> _moveDataRO;
        readonly RefRO<SphereTag> _sphereTagRO;
        readonly RefRW<LocalTransform> _localTransformRW;

        public void MoveProcess(float deltaTime)
        {
            float3 direction = new float3(1f, 0f, 0f);
            _localTransformRW.ValueRW.Position += deltaTime * _moveDataRO.ValueRO.Speed * direction;
        }

        public void JumpProcess()
        {
            
        }
        
        
    }
}