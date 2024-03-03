using UnityEngine;

namespace EcsGame.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _score;
        [SerializeField] int _scoreMax;

        public static GameManager Instance { get; private set; }

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
                //Finish game
                //Stop game
                //Ui show popup and next button 
                Debug.Log("Finish game");
            }
        }

        public void GameOver()
        {
            //Game Over
            //Stop game
            //Ui show popup and restart button 
            Debug.Log("Game over");
            _score = 0;
        }
    }
}

