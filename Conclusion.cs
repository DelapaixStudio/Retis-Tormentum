using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conclusion : MonoBehaviour
{
    [SerializeField] GameObject Arbre, Enfant;
    [SerializeField] AudioSource audioEnfant;
    Vector3 posArbre;
    Image imageArbre, imageEnfant;
    Transform player;
    float startDist;

    [HideInInspector] public bool done = false;

    void Start()
    {
        imageArbre = Arbre.GetComponent<Image>();
        imageEnfant = Enfant.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        posArbre = Arbre.transform.position;
        posArbre.y = 0;
        startDist = Vector3.Distance(posArbre, player.position);

    }

    void Update()
    {
        if (done)
            return;

        float dist = Vector3.Distance(posArbre, player.position);
  
        if(dist < 30)
        {
            audioEnfant.volume = 0;
        }
        else
        {
            audioEnfant.volume = 0.5f;
        }
        /*   Color imageColor = imageArbre.color;
           Color newColor = imageColor;
           newColor.a = (dist / startDist);
           imageArbre.color = newColor;
        */
        //Arbre.GetComponent<Image>().color =newColor;

        dist = dist / startDist;
        dist -= 0.1f;
        imageArbre.color = new Color(imageArbre.color.r, imageArbre.color.g, imageArbre.color.b, dist );
        imageEnfant.color = new Color(imageEnfant.color.r, imageEnfant.color.g, imageEnfant.color.b, dist);
       

    }

    
}
