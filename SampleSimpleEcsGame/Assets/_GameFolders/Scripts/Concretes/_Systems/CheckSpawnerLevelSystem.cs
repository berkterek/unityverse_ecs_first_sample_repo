using EcsGame.Components;
using EcsGame.Managers;
using Unity.Entities;

namespace EcsGame.Systems
{
    public partial class CheckSpawnerLevelSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<GameLevelData>();
            RequireForUpdate<EnemyMaxCountBuffer>();
            RequireForUpdate<EnemyMaxTimeBuffer>();
            RequireForUpdate<EnemySpawnData>();
            RequireForUpdate<CanSpawnData>();
        }
        
        protected override void OnUpdate()
        {
            if (GameManager.Instance == null) return;
            
            var levelData = SystemAPI.GetSingleton<GameLevelData>();
            levelData.CurrentLevel = GameManager.Instance.LevelDataContainer.LevelIndex + 1;
            var enemyMaxCountBuffer = SystemAPI.GetSingletonBuffer<EnemyMaxCountBuffer>();
            var enemyMaxTimeBuffer = SystemAPI.GetSingletonBuffer<EnemyMaxTimeBuffer>();
            var enemyMaxCount = enemyMaxCountBuffer[levelData.CurrentLevel - 1];
            var enemyMaxTime = enemyMaxTimeBuffer[levelData.CurrentLevel - 1];
            var enemySpawnData = SystemAPI.GetSingleton<EnemySpawnData>();
            var canSpawnData = SystemAPI.GetSingleton<CanSpawnData>();
            enemySpawnData.MaxCount = enemyMaxCount.Value;
            enemySpawnData.MaxTime = enemyMaxTime.Value;
            SystemAPI.SetSingleton(enemySpawnData);
            canSpawnData.CanSpawn = true;
            SystemAPI.SetSingleton(canSpawnData);
            this.Enabled = false;
        }
    }
}