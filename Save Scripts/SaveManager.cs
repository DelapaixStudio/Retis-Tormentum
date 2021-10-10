using UnityEngine;
using System.IO;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public static SaveManager SaveInstance;
    [HideInInspector]
    public string savePath;

    [SerializeField] bool Test;

    // Variables à Sauvegarder.
    // Attention : À chaque ajout de variable ne pas oublier de l'ajouter dans PlayerData.cs en tant que variable et dans la fonction de transfert.
    // Idem dans la fonction LoadPlayer il faut penser à repasser toutes les valeurs.
    public bool InvertAxis = true;
    public float MouseSensitivity;
    public float SoundVolume;
    public int scoreLevel1, scoreLevel2, scoreLevel3;
    public int scoreLevel1Hard, scoreLevel2Hard, scoreLevel3Hard;
    public bool GameFinished;
    public bool IntroFinished;
    // -------------  

    public float sessionVolume = 1;
    public bool Demo;
    public bool InfinityMode; // Passer valeur pour charger la scène avec les paramètres infinis.
    [SerializeField] bool alreadyPlayOnceInSession; // Sert à zapper les messages lorsque le joueur à déjà lancer une partie dans sa session

    [SerializeField] GameObject SavingUiPrefab;
    [HideInInspector]
    public bool destroySavingUI = false;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/rco.dfdt"; // On met le chemin ici pour éviter qu'une erreur de modification nhe l'enregistre comme variable dans un objet.
        Debug.Log(savePath);

        if (SaveInstance == null)
        {
           SaveInstance = this;
           DontDestroyOnLoad(gameObject);           
        }
        else
        {
           Destroy(gameObject);
           return;
        }

        if (SavingUiPrefab == null)
        {
            Debug.Log("WARNING, SAVING UI MISSING");
        }


        if (Test)
        {
            IntroFinished = true;
            GameFinished = true;
            Demo = false;
            return;
        }

      

        CheckSave(out bool save);
        if (save) // Si il ya un fichier de sauvegarde
        { 
           
            LoadPlayer();
         
        }
        else // Sinon on en crée un.
        {
            SaveData();
        }       
        
    }


    public void SaveData()
    {
        StartCoroutine(InstantSavingUI());
        SaveSystem.SavePlayer(this);
    }

    private IEnumerator InstantSavingUI()
    {
        GameObject ui = Instantiate(SavingUiPrefab);

      //  yield return new WaitForSeconds(1);

        while(destroySavingUI == false)
        {
            yield return new WaitForEndOfFrame();

        }
        destroySavingUI = false;
        
        Destroy(ui);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        // La seule variable non transférée est celle de démo car c'est le ^premier gameobject SaveManager qui va réercrire ce paramètre (Car le joueur aurait pu jouer à la démo)
        InvertAxis = data.InvertAxis;
        MouseSensitivity = data.MouseSensitivity;
        SoundVolume = data.SoundVolume;
        scoreLevel1 = data.scoreLevel1;
        scoreLevel1Hard = data.scoreLevel1Hard;
        scoreLevel2 = data.scoreLevel2;
        scoreLevel3 = data.scoreLevel3;
        scoreLevel2Hard = data.scoreLevel2Hard;
        scoreLevel3Hard = data.scoreLevel3Hard;
        IntroFinished = data.IntroFinished;
        GameFinished = data.GameFinished;
    }

    public void CheckSave(out bool save)
    {
       
        if (File.Exists(savePath))
        {
            save = true; 
        }
        else
        {
            Debug.Log("Save File Not Found in" + savePath);
            save = false;
        }
    }

}
