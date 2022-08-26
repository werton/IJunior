using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(GroundDetector))]
public class PlayerMovement : MonoBehaviour
{
    private const float _jumpWalkSpeedDivider = 3.0f;

    [SerializeField] private float _walkSpeed = 60f;
    [SerializeField] private float _jumpForce = 39f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimation _animation;
    private GroundDetector _groundDetector;
    private State _state;
    private float _horizontalDirection;

    private enum State : int
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Hurt,
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animation = GetComponent<PlayerAnimation>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
    }

    private void Update()
    {
        Handleinput();
        UpdateState();
    }

    private void Handleinput()
    {
        _horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_groundDetector.IsGrounded() == true)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetState(State.Hurt);
        }
    }

    private void MoveHorizontal()
    {
        float speed;

        if (_groundDetector.IsGrounded() == true)
        {
            speed = _walkSpeed;
        }
        else
        {
            speed = _walkSpeed / _jumpWalkSpeedDivider;
        }

        _rigidbody2D.AddForce(new Vector2(_horizontalDirection * speed, 0), ForceMode2D.Force);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateHorizontalFlip()
    {
        if (_horizontalDirection > 0f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_horizontalDirection < 0f)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void UpdateState()
    {
        if (_groundDetector.IsGrounded() == true)
        {
            if (_horizontalDirection != 0f)
            {
                SetState(State.Walk);
                UpdateHorizontalFlip();
            }
            else
            {
                SetState(State.Idle);
            }

            if (_state == State.Jump || _state == State.Fall)
            {
                SetState(State.Idle);
            }
        }
        else
        {
            if (_state != State.Fall)
            {
                SetState(State.Jump);
            }

            if (Mathf.Sign(_rigidbody2D.velocity.y) < 0)
            {
                SetState(State.Fall);
            }
        }
    }

    private void SetState(State state)
    {
        if (_state == state)
        {
            return;
        }

        _state = state;

        switch (_state)
        {
            case State.Idle:
                _animation.SetIdle();
                break;

            case State.Walk:
                _animation.SetWalk();
                break;

            case State.Jump:
                _animation.SetJump();
                break;

            case State.Fall:
                _animation.SetFall();
                break;

            case State.Hurt:
                _animation.SetHurt();
                break;
        }
    }
}