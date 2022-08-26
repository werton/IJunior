using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet Bullet;

    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private AudioSource _audioSource;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;

    public event UnityAction<Weapon> Reloaded;

    public event UnityAction<Transform> Shot;

    public abstract void OnShot(Transform shootPoint);

    public virtual void OnEnable()
    {
        Shot += OnShot;
    }

    public void Shoot(Transform shootPoint)
    {
        Shot?.Invoke(shootPoint);
        _audioSource.Play();
        OnShot(shootPoint);
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}