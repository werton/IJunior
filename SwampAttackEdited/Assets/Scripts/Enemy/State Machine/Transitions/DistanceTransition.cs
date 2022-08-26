﻿using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangetSpread;

    private void Start()
    {
        _transitionRange += Random.Range(-_rangetSpread, _rangetSpread);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}