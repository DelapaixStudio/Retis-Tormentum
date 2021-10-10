using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorCorridor : MonoBehaviour
{

    [SerializeField] Animator anim;
    AudioSource _audio;
    bool done = false;


    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            Debug.Log("coucou");
            done = true;
            StartCoroutine(Door());
        }
    }


    private IEnumerator Door()
    {
        anim.SetTrigger("Trigger");
        _audio.Play();
        yield return true;
    }

}
