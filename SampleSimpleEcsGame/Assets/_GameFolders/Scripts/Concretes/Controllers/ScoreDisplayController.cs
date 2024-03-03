using EcsGame.Systems;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Controllers
{
    public class ScoreDisplayController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;

        ApplyScoreForPlayerSystem _applyScoreForPlayerSystem;

        void OnValidate()
        {
            if (_scoreText == null) _scoreText = GetComponent<TMP_Text>();
        }

        void Awake()
        {
            _applyScoreForPlayerSystem= World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<ApplyScoreForPlayerSystem>();
        }

        void OnEnable()
        {
            _applyScoreForPlayerSystem.OnScoreChanged += HandleOnScoreChanged;
        }

        void OnDisable()
        {
            _applyScoreForPlayerSystem.OnScoreChanged -= HandleOnScoreChanged;
        }

        void HandleOnScoreChanged(int score)
        {
            _scoreText.SetText(score.ToString());
        }
    }    
}

