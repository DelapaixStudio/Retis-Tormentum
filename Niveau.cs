using UnityEngine;
using System.Collections;


public class Niveau : MonoBehaviour
{
    [SerializeField] bool FullyLoad; // Si le groupe de pattern est composé à l'avance. 
    [SerializeField] int ScoreToCompleteLevel;
    [SerializeField] float BeatTempo;
    [SerializeField] float Coeff = 1;
    [SerializeField] PlayerSphere scriptPlayer;

    [SerializeField] GameObject RosasPrefab;
    Vector3 currentpos;
    [SerializeField] int gap = 8;
    GameObject lastRosas;
 
    public AudioSource _audio1, _audio2, _audioRedux; // Audio1 = Lead, audio2 = basse

    float readingTime = 0; 
    ///  Mesure le temps de lecture des clips audio pour les mixer
    

    GameObject[] Rosas;
    int currentRosas;
    [SerializeField] int visibleLength;

    float wait;
    [HideInInspector]
    public float nextSpawn;

    bool stopIncreseMusic;


    void Awake()
    { 
        if(scriptPlayer == null)
        {
            Debug.Log("Script Joueur MANAQUANT !!");
        }

        nextSpawn = ( BeatTempo / 60) / 4 ;
       // nextSpawn = ;
        float startGap = BeatTempo / 60f;
        BeatTempo = (BeatTempo * Coeff) / 60f;

        
        wait = nextSpawn;

        if (FullyLoad)
        {
           Rosas = new GameObject[transform.childCount];
           bool getFirst = false;
           for(int i = 0; i < transform.childCount; i++)
           {
               GameObject rosa = transform.GetChild(i).gameObject;
               if (rosa.activeSelf && !getFirst)
               {
                   currentRosas = i;
                   getFirst = true;
               }
               Rosas[i] = transform.GetChild(i).gameObject;
               Rosas[i].SetActive(false);
           }
          
          
           // Activation des premières rosas
           for (int i = 0; i < visibleLength; i++)
           {
               Rosas[currentRosas + i].SetActive(true);
           }
        }
       

        // Mise en array de toutes les rosas et désactivation de celles-ci
   
    }

    private void Start()
    {       
        if (SaveManager.SaveInstance.InfinityMode)
        {
            if(FullGameVerification.instance != null)
            {
               _audio1.loop = true;
             //  _audio2.loop = true;
             //  _audioRedux.loop = true;
            }
        }

        foreach(Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                lastRosas = child.gameObject;
                currentpos = child.position;
              //  break;
            }
        }

        if (!FullyLoad)
        {
           for (int i = 0; i < 4; i++) /// Générer les 4 premières rosas
           {
                InstantRosas();
           }
        }

    }

    void Update()
    {

        transform.position -= new Vector3(0f, 0f, BeatTempo * Time.deltaTime);
        ///Debug.Log(BeatTempo * Time.deltaTime);

        if (GameControl.instance.gameOver == true)
        {
       
                _audio1.volume = 0;
               // _audio2.volume = 0;
                _audioRedux.volume = 0;
            
            return;
        }



        if (readingTime / _audio1.clip.length > 0.8)
            stopIncreseMusic = true;

        if (!stopIncreseMusic)
        {        
            //  _audioRedux.volume = 1 - (readingTime / _audioRedux.clip.length) - 0.4f;
            _audio1.volume = readingTime / _audio1.clip.length;
            readingTime += Time.deltaTime * 2;
        }

        

        if (GameControl.instance.gameFinished == false &&  GameControl.instance.score >= ScoreToCompleteLevel  && SaveManager.SaveInstance.InfinityMode == false)
        {
            Debug.Log("Level Done");
            GameControl.instance.gameFinished = true;
        }
    }

    public void InstantRosas()
    {
       /* 
        GameObject newRosas = new GameObject(); 
        if (newRosas == null)
            return;
        */

       // currentpos += new Vector3(0, 0, gap);
        Vector3 setPos = lastRosas.transform.localPosition + new Vector3(0, 0, gap);
        GameObject newRosas = Instantiate(RosasPrefab, setPos, Quaternion.identity);      
        newRosas.transform.parent = transform;
        newRosas.transform.localPosition = setPos;
        lastRosas = newRosas;
    }
}
