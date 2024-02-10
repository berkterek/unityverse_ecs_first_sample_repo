using UnityEngine;

namespace SampleScripts
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "Unity Verse/Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] float _moveSpeed = 2f;
        [SerializeField] float _damage;
        [SerializeField] float _maxHealth;

        public float MoveSpeed => _moveSpeed;
        public float MaxHealth => _maxHealth;
        public float Damage => _damage;
    }
}