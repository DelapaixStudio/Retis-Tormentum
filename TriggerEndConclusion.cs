using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndConclusion : MonoBehaviour
{
    [SerializeField] GameObject ConclusionText;
    [SerializeField] AudioSource audioEnfant, audioMusic;
    [SerializeField] GameObject ConclusionGroup;
    [SerializeField] Conclusion scriptConclu;

    bool done;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && done == false)
        {
            done = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(8);
        scriptConclu.done = true;
        //audioMusic.Stop();
        // Destroy(ConclusionGroup);
        Destroy( GameObject.FindGameObjectWithTag("Player"));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ConclusionText.SetActive(true);
        audioEnfant.Stop();
    }
}
