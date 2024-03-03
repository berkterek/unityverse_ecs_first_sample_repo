using EcsGame.Components;
using EcsGame.Controllers;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerVisualCreationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<VisualObjectData>();
        }

        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (visualObjectData,entity) in SystemAPI.Query<VisualObjectData>().WithNone<PlayerVisualReferenceData>().WithEntityAccess())
            {
                var visualObject = Object.Instantiate(visualObjectData.VisualPrefab);
                var playerController = visualObject.GetComponent<PlayerVisualController>();
                entityCommandBuffer.AddComponent<PlayerVisualReferenceData>(entity, new ()
                {
                    Reference = playerController
                });
                
                entityCommandBuffer.SetComponentEnabled<VisualObjectData>(entity, false);
            }
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}