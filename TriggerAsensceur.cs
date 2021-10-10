using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerAsensceur : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField] GameObject storyModeMessages;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           StartCoroutine(End());
           Destroy(other.gameObject);            
        }
    }

    private IEnumerator End()
    {
        // Debug.Log("coucou");
        anim.SetTrigger("Close");
        //other.gameObject.SetActive(false);
        yield return new WaitForSeconds(10);
       /* transform.parent = transform.root;
        yield return new WaitForSeconds(2);*/
        if(SaveManager.SaveInstance.IntroFinished == false)
        {
           SaveManager.SaveInstance.IntroFinished = true;
           SaveManager.SaveInstance.SaveData();
        }
        Instantiate(storyModeMessages);  
    }
}
