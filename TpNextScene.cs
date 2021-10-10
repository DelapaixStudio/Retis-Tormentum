using UnityEngine;
using UnityEngine.SceneManagement;


public class TpNextScene : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GrandHall", LoadSceneMode.Single);
        }
    }
}
