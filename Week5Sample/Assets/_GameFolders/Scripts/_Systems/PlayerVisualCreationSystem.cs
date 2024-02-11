using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SampleScripts
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerVisualCreationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerVisualObjectData>();
        }

        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (localTransform, playerTag, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<PlayerTag>>().WithEntityAccess().WithNone<PlayerVisualReference>())
            {
                if (SystemAPI.ManagedAPI.HasComponent<PlayerVisualObjectData>(entity))
                {
                    var playerVisualObjectData = SystemAPI.ManagedAPI.GetComponent<PlayerVisualObjectData>(entity);
                    var newObject = Object.Instantiate(playerVisualObjectData.VisualObject);
                    var playerVisualController = newObject.GetComponent<PlayerVisualController>();
                    entityCommandBuffer.AddComponent(entity, new PlayerVisualReference()
                    {
                        PlayerVisualController = playerVisualController
                    });
                    
                    entityCommandBuffer.RemoveComponent<PlayerVisualObjectData>(entity);
                }
            }
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}