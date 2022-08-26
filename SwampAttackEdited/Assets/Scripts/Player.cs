using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Weapon _startWeapon;

    private const string _idleAnimation = "Idle";
    private const string _rifleShootAnimation = "RifleShoot";
    private const string _shotgunShootAndLoadAnimation = "ShotgunShootAndLoad";

    private LinkedList<Weapon> _weapons = new LinkedList<Weapon>();
    private string _weaponShootAnimation;
    private Animator _animator;
    private AudioSource _audioSource;
    private Weapon _currentWeapon;
    private int _currentHealth;

    public int Money { get; private set; } = 200;

    public event UnityAction<int, int> HealthChanged;

    public event UnityAction<int> MoneyChanged;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AddWeapon(_startWeapon);
        _currentHealth = _health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimation))
            {
                DoAction();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimation))
            {
                _audioSource.Play();
            }
        }
    }

    public void DoAction()
    {
        _currentWeapon.Shoot(_shootPoint);
        _animator.StopPlayback();
        _animator.Play(_weaponShootAnimation);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        AddWeapon(weapon);
    }

    public void NextWeapon()
    {
        LinkedListNode<Weapon> current = _weapons.Find(_currentWeapon);
        SetWeapon(current.Next == null ? _weapons.First.Value : current.Next.Value);
    }

    public void PreviousWeapon()
    {
        LinkedListNode<Weapon> current = _weapons.Find(_currentWeapon);
        SetWeapon(current.Next == null ? _weapons.Last.Value : current.Previous.Value);
    }

    private void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

        if (_currentWeapon is Pistol)
        {
            _weaponShootAnimation = _rifleShootAnimation;
        }
        else if (_currentWeapon is Shotgun)
        {
            _weaponShootAnimation = _shotgunShootAndLoadAnimation;
        }
    }

    private void AddWeapon(Weapon weaponPrefab)
    {
        if (HasWeapon(weaponPrefab) == true)
        {
            return;
        }

        Weapon weapon = Instantiate(weaponPrefab);
        _weapons.AddLast(weapon);

        if (_currentWeapon == null)
        {
            SetWeapon(weapon);
        }
    }

    private bool HasWeapon(Weapon weaponPrefab)
    {
        foreach (Weapon weapon in _weapons)
        {
            if (weapon.GetType() == weaponPrefab.GetType())
                return true;
        }

        return false;
    }
}