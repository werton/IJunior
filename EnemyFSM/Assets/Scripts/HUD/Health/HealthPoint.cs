using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDrawableValue
{
    [SerializeField]
    private int _maxValue = 100;

    public event Action<int, int> ValueChanged;

    public int Value { get; private set; }

    public int MaxValue
    {
        get { return _maxValue; }
        private set { _maxValue = value; }
    }

    public float PreviousValue { get; private set; }

    private void Start()
    {
        SetValue(MaxValue);
    }

    public void Add(int addingValue)
    {
        ThrowExecptionOnNegative(addingValue);

        if (Value == MaxValue || addingValue == 0)
        {
            return;
        }

        SetValue(Value + addingValue);
    }

    public void Reduce(int reducingValue)
    {
        ThrowExecptionOnNegative(reducingValue);

        if (Value == 0 || reducingValue == 0)
        {
            return;
        }

        SetValue(Value - reducingValue);
    }

    private static void ThrowExecptionOnNegative(int value)
    {
        if (value < 0)
        {
            throw new Exception("Value can't be negative");
        }
    }

    private void SetValue(int newValue)
    {
        PreviousValue = Value;
        Value = newValue;
        Value = Mathf.Clamp(Value, 0, MaxValue);

        if (PreviousValue != Value)
        {
            ValueChanged?.Invoke(Value, MaxValue);
        }
    }
}