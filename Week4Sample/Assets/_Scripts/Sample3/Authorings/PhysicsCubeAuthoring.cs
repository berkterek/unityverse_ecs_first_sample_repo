using System.Collections;
using System.Collections.Generic;
using _Scripts.Sample3.Components;
using Unity.Entities;
using UnityEngine;

namespace Sample3
{
    public class PhysicsCubeAuthoring : MonoBehaviour
    {
        class PhysicsCubeBaker : Baker<PhysicsCubeAuthoring>
        {
            public override void Bake(PhysicsCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<PhysicsCubeTag>(entity);
            }
        }
    }    
}

