using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _drag = 0.99f; // Спротив для гальмування руху (імітація космічної невагомості)
    [SerializeField] private float _angularDrag = 0.95f; // Спротив для гальмування обертання

    private Vector3 _currentVelocity;

    private void Start()
    {
        _rigidbody.drag = 0; // Спротив для поступового уповільнення
        _rigidbody.angularDrag = 0; // Спротив для обертання
    }

    private void FixedUpdate()
    {
        _currentVelocity = _rigidbody.velocity;

        Vector3 direction = new Vector3(_joystick.Vertical * _moveSpeed, _rigidbody.velocity.y, _joystick.Horizontal * _moveSpeed * -1).normalized;

        Vector3 directionRotation = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.AddForce(direction * _moveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(directionRotation * -1);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        _rigidbody.velocity *= _drag;

        _rigidbody.angularVelocity *= _angularDrag;
    }

}
