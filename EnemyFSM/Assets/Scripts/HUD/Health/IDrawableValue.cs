using System;
public interface IDrawableValue
{
    public event Action<int, int> ValueChanged;
}