using UnityEngine;

public class AttrapeCube : MonoBehaviour
{

    private GameObject Cube;
    [SerializeField] Manette script;

    Animator MainCamAnim;

    private void Start()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamAnim = cam.GetComponent<Animator>();
        if (MainCamAnim == null)
            Debug.Log("Main Cam Not Found From : " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Cube = other.gameObject;
        Destroy(Cube);
        if(script == null)
        {
            Debug.Log(gameObject.name);
        }

        MainCamAnim.SetTrigger("Fail");
        Debug.Log("SendFail");


        script.DisplayNextHit(false);
        /*Destroy(Cube.GetComponent<Rigidbody>());
        Cube.transform.parent = this.gameObject.transform;*/
    }
}
