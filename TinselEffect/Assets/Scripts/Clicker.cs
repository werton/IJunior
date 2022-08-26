using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
    }
}
