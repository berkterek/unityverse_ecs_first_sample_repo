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

        public void MoveAnimation(float velocity)
        {
            _animator.SetFloat(AnimatorConstHelper.Speed, velocity,0.1f, Time.deltaTime);
        }

        public void SetPositionAndRotation(float3 position, quaternion rotation)
        {
            _transform.position = position;
            _transform.rotation = rotation;
        }
    }
}