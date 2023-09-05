using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class PlayerController : MonoBehaviour
{
    [Header("Расстояние между линиями")]
    [SerializeField] private float _lineDistance = 3;
    [Header("Гравитация")]
    [Range(-100f, -1)]
    [SerializeField] private float _gravity = -40f;

    private CharacterController _controller;
    private Vector3 _direction;
    private int _lineToMove = 1;
    private Animator _animator;
    private bool isDead = false;

    public float _jumpForce { get; private set; } = 11;
    public float _speed { get; private set; } = 2;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isDead == true)
            return;

        if (SwipeController.swipeRight)
        {
            if (_lineToMove < 2)
            {
                _lineToMove++;
            }
        }
        else if (SwipeController.swipeLeft)
        {
            if (_lineToMove > 0)
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
        if (_lineToMove == 0)
        {
            targetPosition += Vector3.left * _lineDistance;
        }
        else if (_lineToMove == 2)
        {
            targetPosition += Vector3.right * _lineDistance;
        }

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            _controller.Move(moveDir);
        }
        else
        {
            _controller.Move(diff);
        }
    }

    public void EditSpeed(float speed)
    {
        _speed = speed;
    }

    public void EditJumpForce(float jumpForce)
    {
        _jumpForce = jumpForce;
    }

    public void EditGravity(float gravity)
    {
        _gravity = gravity;
    }

    private void FixedUpdate()
    {
        if(isDead == true) 
            return;

        _direction.z = _speed;
        _direction.y += _gravity * Time.fixedDeltaTime;
        
        if (_speed > 0)
        {
            _controller.Move(_direction * _speed * Time.fixedDeltaTime);
        }

        if (_controller.isGrounded)
        {
            _animator.SetBool("isGrounded", _controller.isGrounded);
            Debug.Log(_controller.isGrounded);
        }

    }

    private void Jump()
    {
        _animator.SetBool("isGrounded", false);
        _direction.y = _jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {
            if(isDead == false)
            {
                _animator.SetTrigger("IsDead");
                isDead = true;
            }
            else
            {
                return;
            }
        }
    }
}
