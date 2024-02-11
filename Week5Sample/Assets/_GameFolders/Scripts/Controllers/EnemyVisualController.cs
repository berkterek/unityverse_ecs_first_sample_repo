using Unity.Mathematics;
using UnityEngine;

namespace SampleScripts
{
    public class EnemyVisualController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] HealthBarController _healthBar;

        public Transform Transform { get; private set; }

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        public void SetPosition(float3 valueROPosition)
        {
            _transform.position = new Vector3(valueROPosition.x, _transform.position.y, valueROPosition.z);
        }

        public void SetHealthValues(float currentHealth, float maxHealth)
        {
            _healthBar.SetHealthValues(currentHealth,maxHealth);
        }
    }
}