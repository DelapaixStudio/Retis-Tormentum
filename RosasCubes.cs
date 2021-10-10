using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class RosasCubes : MonoBehaviour
{
    GameObject[] Cubes;
    int blueCube;
    [SerializeField] GameObject BlueCubePrefab;
    [SerializeField] RuntimeAnimatorController cubeAvatar;


    [SerializeField] Vector3 testVector;

    public bool playerCol = false;
    bool allColsDestroyed = false;

    [SerializeField] AutoMoveAndRotate rotationScript;


    void Start()
    {
        rotationScript = GetComponent<AutoMoveAndRotate>();
       
        int willRotate = Random.Range(0, 2);
        if (willRotate == 1)
        {
            rotationScript.enabled = false;
           ///     Debug.Log("DestroyMoveAndRotate");

            /*
            script = GetComponent<AutoMoveAndRotate>();
            if(script != null)
            {
               script.rotateDegreesPerSecond.value = new Vector3(0, 0, 5f);
            }
            */
        }
        else
        {
         ///   Debug.Log("Activate MoveAndRotate");

            if(GameControl.instance.score > 20)
            {
               rotationScript.enabled = true;
               rotationScript.rotateDegreesPerSecond.value = new Vector3(0, 0, Random.Range(-90, 90));
            }

        }
        
        Cubes = new GameObject[transform.childCount];

        for(int i = 0; i < Cubes.Length; i++)
        {
            Cubes[i] = transform.GetChild(i).gameObject;
        }

        // Ajouter un cube bleu
        blueCube = Random.Range(0, Cubes.Length);
        Transform DeleteCube = Cubes[blueCube].transform;
        GameObject newCube = Instantiate(BlueCubePrefab);
        newCube.transform.position = DeleteCube.position;
        newCube.transform.parent = DeleteCube.parent;
        Cubes[blueCube] = newCube;
        Destroy(DeleteCube.gameObject);
        float scale = Random.Range(0.5f, 1.6f);
        Cubes[blueCube].transform.localScale = new Vector3(scale, scale, scale);


        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Fail()
    {
        if (playerCol)
            return;
        Common();

        foreach (GameObject cube in Cubes)
        {
            if(cube != null) // Le cube contact a été détruit.
            {
               Animator anim = cube.GetComponent<Animator>();
               if(anim == null)
               {
                    anim = cube.AddComponent<Animator>();
                    anim.runtimeAnimatorController = cubeAvatar;
               }             
               anim.SetBool("Touch", true);
            }          
        }

        ToDestroy destroyScript = gameObject.AddComponent<ToDestroy>();
        destroyScript.time = 0.2f;
    }

    public void Success()
    {
        if (playerCol)
            return;
        Common();
        StartCoroutine( Eject() );
      //  Debug.Log("Success");
    }

    private void Common()
    {
        playerCol = true;
        StopCoroutine(InvertCubes());
        

        if (GameControl.instance.gameOver == true)
            Destroy(this);

        transform.parent = null;
        
    }

    private void FixedUpdate()
    {
        if (playerCol)
        {
            if (!allColsDestroyed)
            {
                foreach (GameObject cube in Cubes)
                {
                    if (cube != null) // Le cube contact a été détruit.
                    {
                        Collider col = cube.GetComponent<Collider>();
                        Destroy(col);                      
                    }

                }

                allColsDestroyed = true;
            }
        }
    }

    private IEnumerator Eject()
    {    
        for (int i = 0; i < Cubes.Length; i++) /// Looking for farest distance from statue to wall
        {
            if(Cubes[i] != null)
            {
               var rb= Cubes[i].GetComponent<Rigidbody>();
               rb.constraints = RigidbodyConstraints.FreezePositionZ;              
               
               Vector3 direction = Cubes[i].transform.position - transform.position;
               rb.AddForce(direction * 400);
            }
          
        }

        ToDestroy destroyScript = gameObject.AddComponent<ToDestroy>();
        destroyScript.time = 1;
        yield return true;
    }

    public void Trick()
    {    
        float scale = 1.6f;
        Cubes[blueCube].transform.localScale = new Vector3(scale, scale, scale);
        Cubes[blueCube].GetComponent<Animator>().SetTrigger("StartBlinking");

        int willTrick = Random.Range(0, 3);

        if (willTrick == 3)
            return;

        if (GameControl.instance.score < 30)
            return;

        if(willTrick == 0)
        {

            rotationScript.enabled = true;


            rotationScript.rotateDegreesPerSecond.value = new Vector3(0, 0, -rotationScript.rotateDegreesPerSecond.value.z);
        }

        if (GameControl.instance.score < 75)
            return;

        if (willTrick == 1)
        {
            StartCoroutine(InvertCubes());
        }
    }

    private IEnumerator InvertCubes()
    {
        
        GameObject blueCubeObj = Cubes[blueCube];
        GameObject cubeToInvert = Cubes[Random.Range(0, Cubes.Length)];
        while(Cubes[blueCube] == cubeToInvert)
        {
            cubeToInvert = Cubes[Random.Range(0, Cubes.Length)];
        }


        Vector3 blueCubePos = blueCubeObj.transform.localPosition;
        Vector3 cubeToInvertPos = cubeToInvert.transform.localPosition;     
      

        
        float time = 0;
        while (time <= 1)
        {
            if (blueCubeObj == null)
            {
               yield  return false;
            }

            cubeToInvert.transform.localPosition = Vector3.Lerp(cubeToInvertPos, blueCubePos, time);
            blueCubeObj.transform.localPosition = Vector3.Lerp(blueCubePos, cubeToInvertPos, time);
            time += Time.deltaTime * 2;           
            yield return new WaitForEndOfFrame();
        }
        
    }

}
