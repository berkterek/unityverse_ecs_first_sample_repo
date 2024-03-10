using EcsGame.Components;
using UnityEngine;

namespace EcsGame
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Terek Gaming/Levels/New Level")]
    public class LevelDataContainerSO : ScriptableObject
    {
        [Header("General Info")]
        [SerializeField] int _currentLevel = 1;

        [Header("Enemy Info")] 
        [SerializeField] int[] _enemyCounts;
        [SerializeField] float[] _enemySpawnTimes;

        [Header("Score Info")]
        [SerializeField] int[] _maxScores;

        public int LevelIndex => _currentLevel - 1;
        public float[] EnemyMaxTime => _enemySpawnTimes;
        public int[] EnemyMaxCount => _enemyCounts;

        void OnEnable()
        {
#if UNITY_EDITOR
            _currentLevel = 1;
#endif
        }

        public void IncreaseLevel()
        {
            _currentLevel++;

            if (LevelIndex >= _enemyCounts.Length) _currentLevel = 1;
        }

        public int GetMaxScore() => _maxScores[LevelIndex];
    }    
}

