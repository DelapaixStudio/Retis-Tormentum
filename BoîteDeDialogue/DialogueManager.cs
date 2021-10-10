using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    AudioSource _audio;

    [SerializeField] float dialogueSpeed = 0;
    public TextMeshProUGUI nomTxt;
    public TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject TriggerMessage;


    public Queue<string> Sentences;

    void Start()
    {
        Sentences = new Queue<string>();
        _audio = GetComponent<AudioSource>();
    }

   public  void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        TriggerMessage.SetActive(false);
        nomTxt.text = dialogue.name;
        Debug.Log(dialogue.name);
        Sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            Sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        
    }
    public void DisplayNextSentence()
    {
        if(Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            string check = letter.ToString();
            if(check == "*")
            {
                DisplayNextSentence();
                yield break;
            }

            if(check != " ")
            {
               _audio.pitch = Random.Range(0.8f, 1.2f);
               _audio.Play();
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
            yield return null;
        }
    }

    public void EndDialogue()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
