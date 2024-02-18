using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    public readonly partial struct SoldierMoveAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<MoveData> _moveDataRW;
        readonly RefRW<LocalTransform> _localTransformRW;
        readonly RefRO<InputData> _inputDataRO;

        public bool VelocityProcess()
        {
            _moveDataRW.ValueRW.Velocity = math.length(_inputDataRO.ValueRO.Direction);
            return _moveDataRW.ValueRO.Velocity != 0f;
        }

        public float3 MoveProcess(float deltaTime)
        {
            var moveDirection = deltaTime * _moveDataRW.ValueRO.MoveSpeed * _inputDataRO.ValueRO.Direction;
            _localTransformRW.ValueRW.Position += moveDirection;

            return moveDirection;
        }

        public void RotationProcess(float3 moveDirection, float deltaTime)
        {
            var targetRotation = quaternion.LookRotation(moveDirection, new float3(0f, 1f, 0f));
            _localTransformRW.ValueRW.Rotation = math.slerp(_localTransformRW.ValueRO.Rotation, targetRotation,
                _moveDataRW.ValueRO.MoveSpeed * deltaTime);
        }
    }
}