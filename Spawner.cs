using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject obstacle;
    public GameObject start;
    

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float diminutionTemps;
    public float tempsMin = 0.65f;
    
    

    private void Start()
    {
    }

    


    void Update () {
		
        if(timeBtwSpawn<=0)
        {
            Instantiate(obstacle, start.transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;



            if(startTimeBtwSpawn > tempsMin)
            {

                startTimeBtwSpawn -= diminutionTemps;

            }

            int rand = Random.Range(1, 4); // Donne un nombre entier entre 1 et 3

            if (rand == 1)
            {


            }
            if (rand == 2)
            {

            }
            if (rand == 3)
            {

            }
            if (rand == 4)
            {

            }

        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }

	}
}
