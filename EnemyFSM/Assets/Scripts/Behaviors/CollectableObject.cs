using System.Collections;
using UnityEngine;

public class CollectableObject : TouchableObject
{
    [SerializeField] private AudioSource _audioSource;

    public override void OnTouch()
    {
        _audioSource.Play();
        StartCoroutine(DestroyOnSoundStop());
    }

    private IEnumerator DestroyOnSoundStop()
    {
        while (_audioSource.isPlaying == true)
        {
            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }
}