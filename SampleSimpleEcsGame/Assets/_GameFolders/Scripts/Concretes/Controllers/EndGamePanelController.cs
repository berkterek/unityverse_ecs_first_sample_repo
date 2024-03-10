using Cysharp.Threading.Tasks;
using EcsGame.Managers;
using EcsGame.Uis;
using TMPro;
using UnityEngine;

namespace EcsGame.Controllers
{
    public class EndGamePanelController : MonoBehaviour
    {
        [SerializeField] CanvasGroupHelper _canvasGroupHelper;
        [SerializeField] TMP_Text _titleText;
        [SerializeField] TMP_Text _messageText;
        [SerializeField] GameObject _nextButton;
        [SerializeField] GameObject _tryAgainButton;

        void OnValidate()
        {
            if (_canvasGroupHelper == null) _canvasGroupHelper = GetComponent<CanvasGroupHelper>();
        }

        async void OnEnable()
        {
            await UniTask.WaitUntil(() => GameManager.Instance != null);
         
            GameManager.Instance.OnFinishedGame += HandleOnGameFinished;
            GameManager.Instance.OnGameOvered += HandleOnGameOvered;
        }

        void OnDisable()
        {
            if (GameManager.Instance == null) return;
            
            GameManager.Instance.OnFinishedGame -= HandleOnGameFinished;
            GameManager.Instance.OnGameOvered -= HandleOnGameOvered;
        }
        
        void HandleOnGameFinished()
        {
            _canvasGroupHelper.OpenCanvas();
            
            _titleText.SetText("Finished!");
            _messageText.SetText("You are awesome");
            _nextButton.gameObject.SetActive(true);
        }
        
        void HandleOnGameOvered()
        {
            _canvasGroupHelper.OpenCanvas();
            
            _titleText.SetText("Game Over");
            _messageText.SetText("Try again?");
            _tryAgainButton.gameObject.SetActive(true);
        }
    }    
}