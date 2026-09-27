using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class CharacterControllerMover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _speed = 5f;

    private CharacterController _characterController;
    private Vector3 _direction;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _inputReader.DirectionChanged += OnDirectionChanged;
    }

    private void OnDisable()
    {
        _inputReader.DirectionChanged -= OnDirectionChanged;
    }

    private void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _direction;

        if (_characterController.isGrounded)
        {
            _characterController.Move(movement + Vector3.down);
            return;
        }

        _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
    }

    private void OnDirectionChanged(Vector3 direction)
    {
        _direction = direction;
    }
}