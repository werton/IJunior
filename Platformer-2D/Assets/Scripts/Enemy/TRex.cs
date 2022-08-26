using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PathMovement))]
[RequireComponent(typeof(DamageableObject))]
[RequireComponent(typeof(Spawner))]
public class TRex : MonoBehaviour
{
    private const string AnimationBite = "bite";
    private const string AnimationDied = "died";
    private const float BitingDistance = 6f;

    [SerializeField] private EnemyTarget _player;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PathMovement _pathMovement;
    private DamageableObject _damageble;
    private Spawner _spawner;
    private State _state;

    private enum State : int
    {
        Walk,
        Bite,
        Died,
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pathMovement = GetComponent<PathMovement>();
        _damageble = GetComponent<DamageableObject>();
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        SetState(State.Walk);
        _damageble.Died += Die;
    }

    private void OnDisable()
    {

        _damageble.Died -= Die;
    }

    private void Update()
    {
        if (_state != State.Died)
        {
            if (GetTargetDistance(_player).magnitude < BitingDistance)
            {
                _spriteRenderer.flipX = transform.position.x < _player.transform.position.x;
                SetState(State.Bite);
            }
            else
            {
                SetState(State.Walk);
            }
        }
    }

    private void Die()
    {
        if (_state != State.Died)
        {
            SetState(State.Died);
            _spawner.SpawnInAllPoints();
        }
    }

    private Vector2 GetTargetDistance(EnemyTarget target)
    {
        return transform.position - target.transform.position;
    }

    private void SetState(State state)
    {
        _state = state;

        switch (state)
        {
            case State.Walk:
                _pathMovement.StartMoving();
                break;

            case State.Bite:
                _pathMovement.StopMoving();
                break;

            case State.Died:
                _pathMovement.StopMoving();
                break;
        }

        ChangeAnimation(_state);
    }

    private void ChangeAnimation(State state)
    {
        switch (state)
        {
            case State.Walk:
                _animator.SetBool(AnimationBite, false);
                break;

            case State.Bite:
                _animator.SetBool(AnimationBite, true);
                break;

            case State.Died:
                _animator.SetTrigger(AnimationDied);
                break;
        }
    }
}