using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public GameObject VirtualPrefab;
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
                
                AddComponentObject(entity, new EnemyVisualObjectData()
                {
                    VisualObject = authoring.VirtualPrefab
                });

                AddBuffer<DamageBufferData>(entity);
            }
        }
    }
}