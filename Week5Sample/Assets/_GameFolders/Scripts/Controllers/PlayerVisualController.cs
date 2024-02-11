using System;
using System.Collections;
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
    }    
}

