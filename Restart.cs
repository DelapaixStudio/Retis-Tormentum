using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class Restart : MonoBehaviour
{
    bool done = false;

    void Update()
    {
        if (done)
            return;

        if (GameControl.instance.gameOver == true)
        {       
           if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButton("Fire1"))
           {
                done = true;
                GameControl.instance.Restart();
           }
        }   
    }
}
