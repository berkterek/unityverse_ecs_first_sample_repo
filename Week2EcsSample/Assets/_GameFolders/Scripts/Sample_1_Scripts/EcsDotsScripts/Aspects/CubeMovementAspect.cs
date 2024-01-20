using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample_1_Scripts
{
    //readonly olmak zorunda
    //system gibi partial olur
    //icine class yapisi managed data gelemez
    //Aspect'ler entity'ler uzerine burda yazdigmiz component'lara gore hangi entity uzerine gidicegine karar verirler
    //her system icin ayri ayri aspect olusturmayiz sadece ihtiyac aninda aspect olusururuz aspect ihtiyac alani ise farkli systemlerde ayni query'e ihtiyacimiz varsa burda oldugu gibi bu senaryolarda biz aspect kullaniriz onun disinda fazla aspect kullanmak veya her sisytem icin ayri ayri aspect yazmak ecs dots yaklasimina aykiridir ve gerekisiz bir kalabalik hali alicaktir.
    public readonly partial struct CubeMovementAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRO<CubeTag> _cubeTagRO;
        readonly RefRO<MoveData> _moveDataRO;
        readonly RefRW<LocalTransform> _localTransformRW;

        public void MoveProcess(float deltaTime)
        {
            float3 direction = new float3(0f, 0f, 1f);
            _localTransformRW.ValueRW.Position += deltaTime * _moveDataRO.ValueRO.Speed * direction;
        }

        public void RotateProcess(float deltaTime)
        {
            quaternion currentRotation = _localTransformRW.ValueRO.Rotation;
            quaternion increaseRotation = quaternion.Euler(0f, _moveDataRO.ValueRO.Speed * deltaTime, 0f);
            _localTransformRW.ValueRW.Rotation = math.mul(currentRotation, increaseRotation);
        }
    }    
}

