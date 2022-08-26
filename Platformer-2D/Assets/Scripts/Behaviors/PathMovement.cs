using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PathMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private bool _horizontalOnly;

    private SpriteRenderer _spriteRenderer;
    private Transform[] _points;
    private int _currentPoint;
    private bool _isMoving = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        CreatePath();
    }

    private void Update()
    {
        if (_isMoving == true)
        {
            MovePath();
        }
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    public void StartMoving()
    {
        _isMoving = true;
    }

    private void CreatePath()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void MovePath()
    {
        Transform point = _points[_currentPoint];

        Vector3 targetPosition;

        if (_horizontalOnly == true)
        {
            targetPosition = new Vector3(point.position.x, transform.position.y);
        }
        else
        {
            targetPosition = point.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            SwitchToNextPoint();
        }
    }

    private void SwitchToNextPoint()
    {
        _currentPoint++;

        if (_currentPoint >= _points.Length)
        {
            FlipSprite();
            _currentPoint = 0;
        }
        else
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}