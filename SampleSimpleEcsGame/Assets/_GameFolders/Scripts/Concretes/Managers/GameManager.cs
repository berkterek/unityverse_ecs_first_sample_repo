using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcsGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _score;
        [SerializeField] int _scoreMax;

        public static GameManager Instance { get; private set; }

        public event System.Action OnFinishedGame;
        public event System.Action OnGameOvered;

        void Awake()
        {
            Singleton();
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

            if (_score >= _scoreMax)
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
    }
}

