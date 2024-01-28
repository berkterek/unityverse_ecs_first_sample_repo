using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    public readonly partial struct CubeEntityAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRO<MoveData> _moveDataRO;
        readonly RefRO<CubeEntityTag> _cubeEntityTag;
        readonly RefRW<RandomData> _randomDataRW;
        readonly RefRW<LocalTransform> _localTransformRW;

        public void MoveProcess(float deltaTime)
        {
            var randomDestination = new float3(_randomDataRW.ValueRW.Random.NextFloat(),
                _randomDataRW.ValueRW.Random.NextFloat(), _randomDataRW.ValueRW.Random.NextFloat());

            if (math.distance(randomDestination, _localTransformRW.ValueRW.Position) < 0.1f) return;

            var direction = math.normalize(randomDestination - _localTransformRW.ValueRW.Position);

            _localTransformRW.ValueRW.Position += _moveDataRO.ValueRO.MoveSpeed * deltaTime * direction;
        }

        public void RotateProcess(float deltaTime)
        {
            var currentRotation = _localTransformRW.ValueRW.Rotation;
            var increaseRotationValue = quaternion.Euler(0f, _moveDataRO.ValueRO.RotateSpeed * deltaTime, 0f);
            _localTransformRW.ValueRW.Rotation = math.mul(currentRotation, increaseRotationValue);
        }
    }
}