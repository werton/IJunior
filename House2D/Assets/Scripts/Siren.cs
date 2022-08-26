using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    private readonly float _minVolume = 0.01f;
    private readonly float _maxVolume = 1.0f;
    private readonly float _volumeStep = 0.1f;
    private AudioSource _audioSource;
    private Coroutine _volumeChangerCoroutine;

    private delegate void Callback();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.loop = true;
    }

    private void Update()
    {
    }

    private IEnumerator ChangeVolume(float targetVolume, Callback onReachCallback=null)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeStep * Time.deltaTime);
            yield return null;
        }

        onReachCallback?.Invoke();
    }

    private void StartVolumeChangerCoroutine(IEnumerator coroutine)
    {
        if (_volumeChangerCoroutine != null)
        {
            StopCoroutine(_volumeChangerCoroutine);
        }

        _volumeChangerCoroutine = StartCoroutine(coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }

            StartVolumeChangerCoroutine(ChangeVolume(_maxVolume));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            StartVolumeChangerCoroutine(ChangeVolume(_minVolume, _audioSource.Stop));
        }
    }
}