using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiverMesh : MonoBehaviour
{

    public bool Actif;
    Renderer[] rs;
    Renderer[] ze;

    public GameObject[] Exception;
    private void Awake()
    {
     
    }

    // Start is called before the first frame update
    void Start()
    {    Renderer[] rs = GetComponentsInChildren<Renderer>();
       foreach (Renderer r in rs) { r.enabled = Actif;}
            
       
        foreach (GameObject yo in Exception)
        {
            Renderer[] ze = GetComponents<Renderer>();
            foreach (Renderer f in ze)
                f.enabled = true;
        }
    }

   
}
