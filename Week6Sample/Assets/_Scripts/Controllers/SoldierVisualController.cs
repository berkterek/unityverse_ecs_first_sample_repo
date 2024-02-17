using Unity.Mathematics;
using UnityEngine;

namespace Sample1
{
    public class SoldierVisualController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Transform _transform;

        void OnValidate()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        public void MoveAnimation(float speed)
        {
            _animator.SetFloat(AnimatorConstHelper.Speed, speed);
        }

        public void SetPositionAndRotation(float3 position, quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
        }
    }
}