using Cinemachine;
using UnityEngine;

namespace SampleScripts
{
    class VirtualCameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        public static VirtualCameraController Instance { get; private set; }

        void OnValidate()
        {
            if (_cinemachineVirtualCamera == null) _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void SetLookPoint(Transform lookPoint)
        {
            _cinemachineVirtualCamera.Follow = lookPoint;
        }
    }
}