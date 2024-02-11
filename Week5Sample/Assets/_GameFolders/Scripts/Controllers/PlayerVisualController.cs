using System;
using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleScripts
{
    public class PlayerVisualController : MonoBehaviour
    {
        [SerializeField] Transform _transform;
        [SerializeField] Transform _lookPoint;

        InputReaderSystem _inputReaderSystem;
        IInputReader _inputReader;

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        void Awake()
        {
            _inputReader = new InputReader();
            World ecsWorld = World.DefaultGameObjectInjectionWorld;
            _inputReaderSystem = ecsWorld.GetExistingSystemManaged<InputReaderSystem>();
        }

        IEnumerator Start()
        {
            yield return new WaitUntil(() => VirtualCameraController.Instance != null);
            VirtualCameraController.Instance.SetLookPoint(_lookPoint);
        }

        void Update()
        {
            if (Vector3.Distance(_inputReaderSystem.Direction, _inputReader.Direction) < 0.1f) return;
            _inputReaderSystem.Direction = _inputReader.Direction;
        }

        public void SetPosition(float3 position)
        {
            var vectorPosition = new Vector3(position.x, _transform.position.y, position.z);
            _transform.position = vectorPosition;
        }
    }    
}

