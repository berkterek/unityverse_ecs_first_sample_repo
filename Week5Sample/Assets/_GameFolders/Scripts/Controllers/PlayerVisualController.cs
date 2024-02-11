using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace SampleScripts
{
    public class PlayerVisualController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _lookPoint;

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        IEnumerator Start()
        {
            yield return new WaitUntil(() => VirtualCameraController.Instance != null);
            VirtualCameraController.Instance.SetLookPoint(_lookPoint);
        }

        public void SetPosition(float3 position)
        {
            var vectorPosition = new Vector3(position.x, _transform.position.y, position.z);
            _transform.position = vectorPosition;
        }
    }    
}

