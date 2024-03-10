using Cysharp.Threading.Tasks;
using EcsGame.Systems;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcsGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelDataContainerSO _levelDataContainer;
        [SerializeField] int _score;
        [SerializeField] SubScene[] _subScenes;

        int _maxScore;
        CheckSpawnerLevelSystem _checkSpawnerLevel;
        
        public static GameManager Instance { get; private set; }
        public LevelDataContainerSO LevelDataContainer => _levelDataContainer;

        public event System.Action OnFinishedGame;
        public event System.Action OnGameOvered;

        void Awake()
        {
            Singleton();
            _checkSpawnerLevel =
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<CheckSpawnerLevelSystem>();
        }

        void Start()
        {
            ChangeMaxScore();
        }

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void SetScore(int score)
        {
            _score = score;

            if (_score >= _maxScore)
            {
                Debug.Log("Finish game");
                OnFinishedGame?.Invoke();
            }
        }

        public void GameOver()
        {
            Debug.Log("Game over");
            _score = 0;
            OnGameOvered?.Invoke();
        }

        public void LoadScene()
        {
            LoadSceneAsync();
        }

        private async void LoadSceneAsync()
        {
            await SceneManager.LoadSceneAsync(1);
            await UniTask.Delay(3000);
            await SceneManager.LoadSceneAsync(0);
            _checkSpawnerLevel.Enabled = true;
        }

        public void ChangeMaxScore()
        {
            _maxScore = _levelDataContainer.GetMaxScore();
        }
    }
}

