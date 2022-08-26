using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;

    private int _health;


    public event UnityAction DamageReceived;

    public event UnityAction Died;

    public bool IsAlive => (_health > 0);
    

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<KineticWeapon>(out KineticWeapon weapon))
        {
            ReceiveDamage(weapon.GetDamage());
        }
    }

    private void ReceiveDamage(int damageValue)
    {
        if (damageValue <= 0)
        {
            return;
        }

        _health -= damageValue;

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        DamageReceived?.Invoke();

        if (_health == 0)
        {
            Died?.Invoke();
        }
    }
}