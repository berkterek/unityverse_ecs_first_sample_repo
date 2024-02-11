using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class EnemyVisualCreationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (enemyVisualObjectData, entity) in SystemAPI.Query<EnemyVisualObjectData>().WithEntityAccess().WithNone<EnemyVisualObjectReference>())
            {
                var newObject = Object.Instantiate(enemyVisualObjectData.VisualObject);
                var enemyVisualObjectReference = newObject.GetComponent<EnemyVisualController>();
                entityCommandBuffer.AddComponent(entity, new EnemyVisualObjectReference()
                {
                    EnemyVisualController = enemyVisualObjectReference
                });
                
                entityCommandBuffer.RemoveComponent<EnemyVisualObjectData>(entity);
            }
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}