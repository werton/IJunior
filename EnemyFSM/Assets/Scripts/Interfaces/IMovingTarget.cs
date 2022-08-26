using System;
using UnityEngine;

public interface IMovingTarget
{
    public event Action<Vector3> PositionChanged;

    public Vector3 GetPosition();
}