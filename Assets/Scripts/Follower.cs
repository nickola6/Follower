using UnityEngine;

[RequireComponent(typeof(RigidbodyMover))]
public sealed class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingDistance = 2f;

    private RigidbodyMover _mover;
    private float _stoppingDistanceSqr;

    private void Awake()
    {
        _mover = GetComponent<RigidbodyMover>();
        _stoppingDistanceSqr = _stoppingDistance * _stoppingDistance;
    }

    private void Update()
    {
        Vector3 offset = _target.position - transform.position;
        offset.y = 0f;

        if (offset.sqrMagnitude <= _stoppingDistanceSqr)
        {
            _mover.SetDirection(Vector3.zero);
            return;
        }

        _mover.SetDirection(offset.normalized);
    }
}