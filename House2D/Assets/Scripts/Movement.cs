using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Vector3 _moveDirection;

    private void Update()
    {
        Handleinput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Handleinput()
    {
        float shiftX = Input.GetAxisRaw("Horizontal");
        float shiftY = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector3(shiftX, shiftY).normalized;
    }

    private void Move()
    {
        _rigidbody2D.velocity = _moveDirection * _speed;
    }
}