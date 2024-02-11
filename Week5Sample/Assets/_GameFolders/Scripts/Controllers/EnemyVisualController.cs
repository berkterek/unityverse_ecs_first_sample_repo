using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class EnemyVisualController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] HealthBarController _healthBar;

        World _ecsWorld;

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }
        
        void Awake()
        {
            _ecsWorld = World.DefaultGameObjectInjectionWorld;
        }
    }
}