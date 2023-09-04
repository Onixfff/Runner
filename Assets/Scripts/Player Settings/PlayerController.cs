using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    
    [Range(1,100)] 
    [SerializeField] private float _startSpeed;
    [Range(1,100)]
    [SerializeField] private float _speed;
    [Header("")]
    [SerializeField] private float _lineDistance = 3;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = -9.8f;

    private CharacterController _controller;
    private Vector3 _direction;

    private int _lineToMove = 1;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if(_lineToMove < 2)
            {
                _lineToMove++;
            }
        }
        else if (SwipeController.swipeLeft)
        {
            if(_lineToMove > 0) 
            {
                _lineToMove--;
            }
        }

        else if (SwipeController.swipeUp)
        {
            if (_controller.isGrounded)
                Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(_lineToMove == 0)
        {
            targetPosition += Vector3.left * _lineDistance;
        }
        else if(_lineToMove == 2)
        {
            targetPosition += Vector3.right * _lineDistance;
        }

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            _controller.Move(moveDir);
        }
        else
        {
            _controller.Move(diff);
        }

    }

    private void FixedUpdate()
    {
        _direction.z = _startSpeed;
        _direction.y += _gravity * Time.fixedDeltaTime;
        if (_speed > 0)
        {
            _controller.Move(_direction * _speed * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("IsJump");
        _direction.y = _jumpForce;
    }
}
