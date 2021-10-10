using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPause : MonoBehaviour {

    public static MenuPause instance;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject param;

    [SerializeField] GameSettings settings;

    bool[] ControllerPlugged;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if(settings == null)
        {
            Debug.Log("SETTINGS MISSING");
        }
    }

    void Start()
    {
        settings.invertAxisButton.isOn = SaveManager.SaveInstance.InvertAxis;
        settings.sensitivitySlider.value = SaveManager.SaveInstance.MouseSensitivity;

        settings.gameObject.SetActive(false);

        string[] temp = Input.GetJoystickNames();
        ControllerPlugged = new bool[temp.Length];
    }

    void Update() 
    {
       // Debug.Log(Input.GetJoystickNames().Length);
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        if(ControllerPlugged != null)
        {
           if(temp.Length != ControllerPlugged.Length)
           {
               /// Si il y a une nouvelle manette qui se branche 
               ///il va y avoir une erreur avec les bools dans la partie ci-dessous
               ControllerPlugged = new bool[temp.Length]; 
               
           }
           //Check whether array contains anything
           if (temp.Length > 0)
           {
               //Iterate over every element
               for (int i = 0; i < temp.Length; ++i)
               {
                   //Check if the string is empty or not
                   if (!string.IsNullOrEmpty(temp[i]))
                   {
                       //Not empty, controller temp[i] is connected
                       Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                       ControllerPlugged[i] = true;
                   }
                   else
                   {
                       /// Déconnexion.
                       if (ControllerPlugged[i] == true)
                       {
                           ControllerPlugged[i] = false;
                           Pause();
                       }
                   }
               }
           }
        }




    

        if (GameControl.instance.gameOver == false)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
                 if (GameIsPaused) 
                 {
                     Resume();
                 }
                 else
                 {
                     Pause();
                 }
            }

        }

    }

    public void Pause()
    {
                pauseMenuUI.SetActive(true);
                GameIsPaused = true;
        Cursor.visible = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Resume()
    {
           pauseMenuUI.SetActive(false);  
           GameIsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Cursor.visible = false;
    }

 

    public void QuitGame ()
    {
        Debug.Log("Vous quittez la partie. . . :'( ");
        Application.Quit();
    }
	
}

