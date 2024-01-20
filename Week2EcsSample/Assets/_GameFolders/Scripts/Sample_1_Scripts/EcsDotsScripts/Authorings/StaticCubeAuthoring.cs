using Unity.Entities;
using UnityEngine;

namespace Sample_1_Scripts
{
    public class StaticCubeAuthoring : MonoBehaviour
    {
        class StaticCubeBaker : Baker<StaticCubeAuthoring>
        {
            public override void Bake(StaticCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
            }
        }
    }    
}

