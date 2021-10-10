using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject[] Cube; // Mettre les cubes dans le même sens que les inputs[]
    KeyCode[] inputs = new KeyCode[4];
    GameObject currentCube;
    
    void Start()
    {
        inputs[0] = KeyCode.LeftArrow;
        inputs[1] = KeyCode.UpArrow;
        inputs[2] = KeyCode.RightArrow;
        inputs[3] = KeyCode.DownArrow;
    }


    private IEnumerator Instantiate()
    {
        int random = Random.Range(0, 4);
        Debug.Log("Next Cube = " + random);
        currentCube = Instantiate(Cube[random]);

        bool input = false;

       
        GetInput();
        yield return new WaitForEndOfFrame();
        


    }

    private void GetInput()
    {

        foreach(KeyCode key in inputs)
        {
           
        }

        for(int i = 0; i < inputs.Length; i++)
        {
           if (Input.GetKeyDown(inputs[i]))
           {
              if(Cube[i] == currentCube)
              {
                    GameControl.instance.Score(true);
               
              }

           }
        }

        
    }
}
