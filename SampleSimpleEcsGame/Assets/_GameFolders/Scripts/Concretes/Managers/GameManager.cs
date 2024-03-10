using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcsGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelDataContainerSO _levelDataContainer;
        [SerializeField] int _score;

        int _maxScore;
        
        public static GameManager Instance { get; private set; }
        public LevelDataContainerSO LevelDataContainer => _levelDataContainer;

        public event System.Action OnFinishedGame;
        public event System.Action OnGameOvered;

        void Awake()
        {
            Singleton();
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
            await SceneManager.LoadSceneAsync(0);
        }

        public void ChangeMaxScore()
        {
            _maxScore = _levelDataContainer.GetMaxScore();
        }
    }
}

