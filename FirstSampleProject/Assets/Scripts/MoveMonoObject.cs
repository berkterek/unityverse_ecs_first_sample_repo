using UnityEngine;

public class MoveMonoObject : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] float _moveSpeed = 5f;

    void OnValidate()
    {
        if (_transform == null) _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _transform.position += Time.deltaTime * _moveSpeed * Vector3.forward;
    }
}