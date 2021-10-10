using UnityEngine;

public class Allumage : MonoBehaviour
{
    /// <summary>
    /// ANIMATION DES NEONS
    /// </summary>

    [SerializeField] Animator[] anim;

    private bool aclique;


    void Start()
    {
        anim = GetComponentsInChildren<Animator>();
        aclique = true;
        foreach (Animator play in anim)
            play.SetBool("Fin", true);

    }
    /*
    void Update()
    {

        if (Input.anyKeyDown) 
        {
           if (aclique) 
           {            
           
               foreach (Animator play in anim)
               play.Play("Neon");
               aclique = false;
           }
           
           else
           {
                
              foreach (Animator play in anim)
              play.SetBool("Fin", false);
           
               
           }
        }
    }
    */
}
