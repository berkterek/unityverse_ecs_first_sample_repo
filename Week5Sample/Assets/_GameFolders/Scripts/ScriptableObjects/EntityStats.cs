using UnityEngine;

namespace SampleScripts
{
    [CreateAssetMenu(fileName = "New Stats", menuName = "Unity Verse/Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] float _moveSpeed = 2f;

        public float MoveSpeed => _moveSpeed;
    }
}