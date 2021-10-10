using System.Collections;
using UnityEngine;

public class LightStatue : MonoBehaviour
{
    [SerializeField] GameObject Light;
    AudioSource _audio;
    bool done = false;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        Light.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (done)
            return;

        if (other.CompareTag("Player"))
        {
            done = true;
            StartCoroutine(DisplayLight());
        }
    }

    private IEnumerator DisplayLight()
    {
            _audio.Play();
        while (!_audio.isPlaying)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForEndOfFrame();
        Light.SetActive(true);
        Destroy(GetComponent<LightStatue>());
    }
}
