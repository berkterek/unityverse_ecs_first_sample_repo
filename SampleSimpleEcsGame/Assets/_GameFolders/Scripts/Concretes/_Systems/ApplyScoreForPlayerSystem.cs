using EcsGame.Components;
using Unity.Entities;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial class ApplyScoreForPlayerSystem : SystemBase
    {
        public event System.Action<int> OnScoreChanged; 
        
        protected override void OnUpdate()
        {
            foreach (var (playerTag, playerScoreData, playerScoreBuffers) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<PlayerScoreData>, DynamicBuffer<PlayerScoreBuffer>>())
            {
                if(playerScoreBuffers.Length == 0) continue;
                
                foreach (var playerScoreBuffer in playerScoreBuffers)
                {
                    playerScoreData.ValueRW.Score += playerScoreBuffer.Value;
                }
                
                playerScoreBuffers.Clear();
                OnScoreChanged?.Invoke(playerScoreData.ValueRO.Score);
            }
        }

        protected override void OnDestroy()
        {
            OnScoreChanged = null;
        }
    }
}