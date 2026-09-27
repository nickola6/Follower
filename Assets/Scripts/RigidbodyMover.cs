using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public sealed class RigidbodyMover : MonoBehaviour
{
    private const float MinContactDirectionDot = 0f;
    private const float MaxWalkableContactNormalY = 0.5f;
    private const float GravityMultiplier = 2f;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _stepOffset = 0.7f;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;

        velocity.x = _direction.x * _speed;
        velocity.z = _direction.z * _speed;

        _rigidbody.velocity = velocity;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_direction == Vector3.zero)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, _direction) >= MinContactDirectionDot)
                continue;

            if (contact.normal.y > MaxWalkableContactNormalY)
                continue;

            float heightDifference = contact.point.y - _collider.bounds.min.y;

            if (heightDifference <= MinContactDirectionDot || heightDifference > _stepOffset)
                continue;

            float stepUpVelocity = Mathf.Sqrt(GravityMultiplier * Mathf.Abs(Physics.gravity.y) * heightDifference);

            Vector3 velocity = _rigidbody.velocity;
            velocity.y = Mathf.Max(velocity.y, stepUpVelocity);

            _rigidbody.velocity = velocity;

            break;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}