using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetKeyName : MonoBehaviour
{
    [SerializeField] string inputName;

    Dictionary<string, KeyCode> commandes = new Dictionary<string, KeyCode>();
    GameObject dico;
    void Start()
    {
        dico = GameObject.FindGameObjectWithTag("DicoTouche");
        Debug.Log(dico.name);
        commandes = dico.GetComponent<ParamTouches>().keys; // On récupère les commandes.
        Text textComponent = GetComponent<Text>();
        textComponent.text = commandes[inputName].ToString();
    }
   
}
