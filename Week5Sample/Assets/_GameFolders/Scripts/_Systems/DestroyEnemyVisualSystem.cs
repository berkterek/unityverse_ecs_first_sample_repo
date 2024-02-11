using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SampleScripts
{
    public partial struct DestroyEnemyVisualSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (enemyVisualObjectReference, entity) in SystemAPI.Query<EnemyVisualObjectReference>().WithNone<LocalTransform>().WithEntityAccess())
            {
                Object.Destroy(enemyVisualObjectReference.EnemyVisualController.gameObject);
                entityCommandBuffer.RemoveComponent<EnemyVisualObjectReference>(entity);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}