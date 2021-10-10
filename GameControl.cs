using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Steamworks;


public class GameControl : MonoBehaviour
{
    public static GameControl instance;         //A reference to our game control script so we can access it statically.
   // public Text scoreText;                      //A reference to the UI text component that displays the player's score.
    [SerializeField] GameObject FinalScorePrefab;

    [SerializeField] Text ScoreText;

    public string levelName; // Permet au prefab du sore de game over d'afficher les records.
    public int score = 0;                      //The player's score.
    public bool gameOver = false;               //Is the game over?
    public bool gameFinished = false;


    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;


    void Awake()
    {
        //If we don't currently have a game control...

        if (instance == null)
        {
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }


#if UNITY_ANDROID
        Time.fixedDeltaTime = 0.1f;
#endif
        Time.timeScale = 1; // À la fin de la partie timescale = 0 donc il faut le réinitialiser.
        Cursor.visible = false;
        AudioListener.volume = SaveManager.SaveInstance.sessionVolume;

    }

    private void Start()
    {        
        AudioListener.volume = SaveManager.SaveInstance.SoundVolume;

        if(FullGameVerification.instance != null)
        {
            SaveManager.SaveInstance.InfinityMode = true;
        }
        else
        {
            SaveManager.SaveInstance.InfinityMode = false;
        }  
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; // À la fin de la partie timescale = 0 donc il faut le réinitialiser.
        Cursor.visible = false;
        score = 0;
    }


    public void Score(bool kepasa)
    {
        //Can't score if the game is over.
        if (gameOver)
            return;

        if (kepasa)
        {        
        //If the game is not over, increase the score...
            score++;
            ScoreText.text = score.ToString();
        }
        if (!kepasa)
        {
            score--;
        }
        //...and adjust the score text.
      //  scoreText.text = "Score: " + score;
    }

    public void GrosCube (int ScoreGrosCube)
    {      
          score = score + ScoreGrosCube;
      //  scoreText.text = "Score: " + score.toString();
    }

    public void GameOver()
    {
        
        gameOver = true;  

        if (gameFinished && SaveManager.SaveInstance.GameFinished == false)
        {
            StartCoroutine(GameFinished());
            return;
        }

        StartCoroutine(DisplayScore());
       // AudioListener.volume = 0;    

        if(score > SaveManager.SaveInstance.scoreLevel1)
        {
            SaveManager.SaveInstance.scoreLevel1 = score;
            SaveManager.SaveInstance.SaveData();
        }   
        
        

    }

    private IEnumerator DisplayScore()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Instantiate(FinalScorePrefab);
    }

    private IEnumerator SaveBeforeGameOver()
    {
        yield return new WaitForEndOfFrame();   

    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public IEnumerator GameFinished()
    {        
        SaveManager.SaveInstance.GameFinished = true;
        SaveManager.SaveInstance.scoreLevel1 = score;
        SaveManager.SaveInstance.SaveData();

        yield return new WaitForSecondsRealtime(5);

        LoadScene("Conclusion");
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (gameOver)
                return;

        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
            
            MenuPause.instance.Pause();
        }
        else
        {
            if (gameOver) 
                return;

            Debug.Log("Steam Overlay has been closed");
            MenuPause.instance.Resume();
        }
    }

}