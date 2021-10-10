using UnityEngine;

public class CatchObjects : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Catch");
        if (other.CompareTag("Rosas") ) /// Gros cube est le tag du Tunnel
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("GrosCube")) /// Gros cube est le tag du Tunnel
        {
            Destroy(other.gameObject);
        }        

    }


}
