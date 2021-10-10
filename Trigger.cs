using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public GameObject CamParano;
    public GameObject CamStudio;
    public GameObject[] cameras;


    void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("Camera");


    }

   
    void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {

            DeactivateAllCameras();
            CamParano.SetActive(true);

        }



    }

    public void DeactivateAllCameras()
    {

        for (int i = 0; i < cameras.Length; i++)
        {

            cameras[i].SetActive(false);
        }

       
        


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            CamStudio.SetActive(true); }
    }
}
