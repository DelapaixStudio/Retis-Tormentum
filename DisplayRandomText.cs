using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRandomText : MonoBehaviour
{

    [SerializeField] string[] txt;
    Text textComponent;
    void Start()
    {
        textComponent = GetComponent<Text>();

        StartCoroutine( DisplayText());
    }


    private IEnumerator DisplayText()
    {
        bool gameOver = false;
        while (!gameOver)
        {
            if(Time.timeScale != 0)
                textComponent.text = txt[Random.Range(0, txt.Length)];
            yield return new WaitForSecondsRealtime(4);
        }
    }
}
