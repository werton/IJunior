using System;

public interface IDamageble
{
    public event Action<int> Damaged;

    public event Action<IDamageble> Died;

    public void ReciveDamage(int damageValue);
}