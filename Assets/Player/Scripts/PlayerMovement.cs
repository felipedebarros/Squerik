using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

	enum JumpState
	{
		GROUND, ON_AIR, FALLING
	};

	[Header("Jump Configs")]
	[SerializeField] private float _jumpMinForce;
	[SerializeField] private float _jumpForceIncrements;
	[SerializeField] private float _jumpMaxTime;
	[SerializeField] private float _jumpHorzInertiaMultiplier;

	[Header("Movement Configs")]
	[SerializeField] private float _walkSpeed;
	[SerializeField] private float _airSpeed;

	[Header("Ground Checking")]
	[SerializeField] private Transform _groundDetection;
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private Vector2 _boxSize;

	private Rigidbody2D _rb2d;
	private Animator _animator;
	private float _jumpedTime;
	private JumpState _jumpState = JumpState.GROUND;

	void Start () 
	{
		_rb2d = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
		HandleInput();
	}

	private void HandleInput()
	{
		float horz = Input.GetAxisRaw("Horizontal");
		Move(horz);
		Jump();
	}

	private void Move(float horz)
	{
		float speed =  IsGrounded() ? _walkSpeed : _airSpeed;
		_rb2d.AddForce(transform.right * horz * speed);
	}

	private void Jump()
    {
        bool grounded = IsGrounded();
        switch (_jumpState)
        {
            case JumpState.GROUND:
                if (grounded && Input.GetButtonDown("Jump"))
                {
                    _rb2d.AddForce(Vector2.up * _jumpMinForce + new Vector2(0, _rb2d.velocity.y) * _jumpHorzInertiaMultiplier);
                    _jumpState = JumpState.ON_AIR;
                    _jumpedTime = Time.time;
                }
                else if (!grounded)
                    _jumpState = JumpState.FALLING;
                break;
            case JumpState.ON_AIR:
                if (Input.GetButtonUp("Jump") || Time.time - _jumpedTime > _jumpMaxTime)
                    _jumpState = JumpState.FALLING;
                else
                    _rb2d.AddForce(Vector2.up * _jumpForceIncrements);
                break;
            case JumpState.FALLING:
                if (grounded)
                {
                    _jumpState = JumpState.GROUND;
                    _animator.SetTrigger("land");
                }
                break;
        }
    }

    private bool IsGrounded()
	{
		RaycastHit2D hit = Physics2D.BoxCast(_groundDetection.position, _boxSize, 0f, Vector3.forward, Mathf.Infinity, _groundLayer);
		return hit.collider != null;
	}

}
