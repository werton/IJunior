using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = .8f;
    [SerializeField] private float _shakeStrength = 50f;
    [SerializeField] private int _shakeCicles = 25;
    [SerializeField] private bool _fadeOut = true;

    private Tweener _tweener = null;

    void Start()
    {
        _tweener = transform.DOShakePosition(duration: _shakeDuration, strength: new Vector3(_shakeStrength, 0f, 0f),
            vibrato: _shakeCicles, randomness: 0, snapping: true, fadeOut: _fadeOut).SetAutoKill(false).Pause();
    }


    void Update()
    {

    }

    public void Shake()
    {
        if (_tweener?.IsPlaying() == false)
        {
            _tweener.Play();
            _tweener.Restart();
        }



    }
}
