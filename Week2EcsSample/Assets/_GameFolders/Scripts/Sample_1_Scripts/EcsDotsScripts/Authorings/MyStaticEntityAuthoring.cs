using Unity.Entities;
using UnityEngine;

namespace Sample_1_Scripts
{
    public class MyStaticEntityAuthoring : MonoBehaviour
    {
        class MyStaticEntityBaker : Baker<MyStaticEntityAuthoring>
        {
            public override void Bake(MyStaticEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
            }
        }
    }    
}

