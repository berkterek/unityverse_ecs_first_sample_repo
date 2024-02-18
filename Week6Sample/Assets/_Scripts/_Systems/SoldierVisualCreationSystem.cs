using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SoldierVisualCreationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SoldierVisualObjectData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (soldierVisualObjectData, entity) in SystemAPI.Query<SoldierVisualObjectData>().WithEntityAccess())
            {
                var newObject = Object.Instantiate(soldierVisualObjectData.PrefabObject);
                var soldierVisualController = newObject.GetComponent<SoldierVisualController>();
                
                entityCommandBuffer.AddComponent(entity, new SoldierVisualReferenceData()
                {
                    Reference = soldierVisualController
                });
                
                entityCommandBuffer.RemoveComponent<SoldierVisualObjectData>(entity);
            }
        }
    }
}