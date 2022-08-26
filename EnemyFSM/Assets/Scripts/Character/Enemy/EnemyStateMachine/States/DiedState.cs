using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DiedState : State
{
    private const string AnimationBite = "TrexDied";
    private Animator _animator;
    //private Character _self;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(AnimationBite);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }

}
