using UnityEngine;

public class TriggerLastState : MonoBehaviour
{
    [SerializeField] Niveau niveauScript;
    private void OnTriggerEnter(Collider other)
    {
        if (GameControl.instance.gameOver == false && other.CompareTag("Rosas"))
        {     

            niveauScript.InstantRosas();

            RosasCubes rosasScript = other.GetComponent<RosasCubes>();
            rosasScript.Trick();
        }
    }
}
