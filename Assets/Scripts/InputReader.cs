using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const float MaxSqrMagnitude = 1f;

    private Vector3 _direction;

    public event Action<Vector3> DirectionChanged;

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0f, Input.GetAxisRaw(VerticalAxis));

        if (direction.sqrMagnitude > MaxSqrMagnitude)
            direction.Normalize();

        if (_direction == direction)
            return;

        _direction = direction;
        DirectionChanged?.Invoke(_direction);
    }
}