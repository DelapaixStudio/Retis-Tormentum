using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DisplayScreenMessages : MonoBehaviour
{

    AudioSource _audio;

    [SerializeField] string sceneToLoadAtEnd;
    [SerializeField] bool mustConfirm, demoMessage;
    [SerializeField] GameObject player;
    bool confirmed = false;

    GameObject NextText;
    Animator anim;
    int index; // Position dans les messages
    bool done = false;

    void Awake()
    {
        // Dernier message 
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false; // Si il y a un GO Joueur on le desactive pour pouvoir voir les messages normalement 


        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();


        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform)
                index = i;
        }

        if (demoMessage)
        {
            if(SaveManager.SaveInstance.Demo == false)
            {
                SetNext();
                return;
            }
        }

         StartCoroutine(Transition());

    }

    

    private IEnumerator Transition()
    {
        done = true;
        if(_audio != null)
            _audio.Play();


        if (mustConfirm)
        {
            anim.SetTrigger("ShowConfirmButton");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            while (confirmed == false)
            {        
              //  Debug.Log("Coucou");
            
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {       
            yield return new WaitForSeconds(6);
            anim.SetTrigger("End");
        }
      

      

        if (index + 1 >= transform.parent.childCount) // Dernier Message.
        {
            yield return new WaitForSeconds(2);


            SetNext();

        }
        else // Afficher prochain message
        {
            Debug.Log("Salut");
            yield return new WaitForSeconds(2);

            SetNext();
        }
    }

    public void Confirm() /// Appel depuis boutton.
    {
        confirmed = true;
    }


    void SetNext()
    {

        if (index + 1 >= transform.parent.childCount) // Dernier Message.
        {        Debug.Log("End");

            SceneManager.LoadScene(sceneToLoadAtEnd);
            return;

        }

        NextText = transform.parent.GetChild(index + 1).gameObject;
        gameObject.SetActive(false);
        NextText.SetActive(true);
    }

}
