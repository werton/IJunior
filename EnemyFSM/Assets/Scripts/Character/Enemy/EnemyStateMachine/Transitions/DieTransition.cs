using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTransition : Transition
{
    private HealthPoint _health;

    private void Start()
    {
        _health = GetComponentInParent<HealthPoint>();
    }


    private void Update()
    {
        if (_health.Value == 0)
            NeedTransit = true;
    }



}
