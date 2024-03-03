using System;
using EcsGame.Systems;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace EcsGame.Controllers
{
    public class PlayerVisualController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] Image _healthBarImage;

        PlayerApplyDamageVisualSystem _playerApplyDamageVisualSystem;
        
        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        void Awake()
        {
            _playerApplyDamageVisualSystem = World.DefaultGameObjectInjectionWorld
                .GetExistingSystemManaged<PlayerApplyDamageVisualSystem>();
        }

        void OnEnable()
        {
            _playerApplyDamageVisualSystem.OnHealthChanged += HandleOnHealthChanged;
        }

        void OnDisable()
        {
            _playerApplyDamageVisualSystem.OnHealthChanged -= HandleOnHealthChanged;
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
        
        void HandleOnHealthChanged(float currentHealth, float maxHealth)
        {
            _healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }    
}