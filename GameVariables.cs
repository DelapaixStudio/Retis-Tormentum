using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables instance;


    bool InvertAxis = true;
    float MouseSensitivity;
    float SoundVolume;
    int score;


    void Awake()
    {
        if(GameVariables.instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }


    public void Save()
    {

    }
   
}
