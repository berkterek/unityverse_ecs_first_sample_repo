using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public EntityStats EntityStats;
        
        class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<EnemyTag>(entity);
                
                AddComponent(entity, new HealthData()
                {
                    CurrentHealth = authoring.EntityStats.MaxHealth,
                    MaxHealth = authoring.EntityStats.MaxHealth
                });

                AddBuffer<DamageBufferData>(entity);
            }
        }
    }
}