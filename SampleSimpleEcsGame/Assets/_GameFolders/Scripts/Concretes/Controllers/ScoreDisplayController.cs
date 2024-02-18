using System;
using EcsGame.Systems;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Controllers
{
    public class ScoreDisplayController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreText;

        void OnValidate()
        {
            if (_scoreText == null) _scoreText = GetComponent<TMP_Text>();
        }

        void Start()
        {
            World ecsWorld = World.DefaultGameObjectInjectionWorld;
            var applyScoreForPlayerSystem = ecsWorld.GetExistingSystemManaged<ApplyScoreForPlayerSystem>();
            applyScoreForPlayerSystem.OnScoreChanged += HandleOnScoreChanged;
        }

        void HandleOnScoreChanged(int score)
        {
            _scoreText.SetText(score.ToString());
        }
    }    
}

