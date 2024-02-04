using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Sample2
{
    public class CubeEntityAuthoring : MonoBehaviour
    {
        class CubeEntityBaker : Baker<CubeEntityAuthoring>
        {
            public override void Bake(CubeEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveData()
                {
                    Speed = 2f,
                    Direction = new float3(0f, 0f, 1f)
                });
            }
        }
    }    
}