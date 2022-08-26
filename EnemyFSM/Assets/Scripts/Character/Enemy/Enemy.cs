using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int _reward;

    private IDamagebleTarget _target;

    public int Reward => _reward;
    public IDamagebleTarget Target => _target;


    public void Init(IDamagebleTarget target)
    {
        _target = target;
    }

}
