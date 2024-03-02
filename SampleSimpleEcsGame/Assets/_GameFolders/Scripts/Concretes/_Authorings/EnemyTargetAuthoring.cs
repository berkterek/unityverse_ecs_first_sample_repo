using Unity.Entities;
using UnityEngine;
using EcsGame.Components;

namespace EcsGame.Authorings
{
    public class EnemyTargetAuthoring : MonoBehaviour
    {
        class EnemyTargetBaker : Baker<EnemyTargetAuthoring>
        {
            public override void Bake(EnemyTargetAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<EnemyTargetTag>(entity);
            }
        }
    }    
}

