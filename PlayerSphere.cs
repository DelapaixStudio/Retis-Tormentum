using UnityEngine;
using System.Collections;

public class PlayerSphere : MonoBehaviour
{

    // https://answers.unity.com/questions/1176172/object-follow-mouse-in-radius.html
    //https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#:~:text=In%20Unity%2C%20getting%20the%20mouse,bottom%20left%20of%20the%20screen.
   
    
   
    AudioSource _audio;
    [SerializeField] AudioClip winClip;
    [SerializeField] int flow = 50;
    int combo;
    int fail;
    int gameOver = 1;

    Animator camAnimator;

    [SerializeField] GameObject Tunnel;
    [SerializeField] GameObject FlowFx;
    [SerializeField] ParticleSystem SparklesPS;

    [SerializeField] Niveau LevelScript;
    [SerializeField] GameObject Sparkles;

    [SerializeField] float soundVol;


    void Start()
    {
        camAnimator = Camera.main.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        Cursor.visible = false;

#if UNITY_STANDALONE

        FlowFx.SetActive(false);
        Tunnel.SetActive(true);

#endif
                                  

        Vector3 levelPos = LevelScript.transform.position;
        LevelScript.transform.position = new Vector3(levelPos.x, levelPos.y, transform.parent.position.z);  /// Mettre le groupe niveau au même niveau que la caméra    

    }

    private IEnumerator LittleFlow()
    {
        camAnimator.SetBool("Flow", true);
        yield return new WaitForSeconds(0.2f);
        camAnimator.SetBool("Flow", false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Le cube doit être option trigger pour être detecté.
        //Debug.Log("Col");

        if (other.CompareTag("Rosas") || other.CompareTag("GrosCube")) // GrosCube est le tag du tunnel
        {
            // Le collider de la Rosas sert à declencher le feinte avec le script TriggerLastState.cs sur le collider du joueur

        //    LevelScript.DisplayNextRosas();
            return;
        }

        // DESTROY TRIGGERED CUBE AND ADD PARTICULES
        RosasCubes ParentScript = other.transform.parent.GetComponent<RosasCubes>();
        Instantiate(Sparkles, transform.position, Quaternion.identity);
        MeshDestroy script = other.gameObject.AddComponent<MeshDestroy>();
        script.ExplodeForce = 1000;
        script.CutCascades = 1;
        script.DestroyThis();

        // PLAY AUDIO TRIGEGR SOUND
        /*
        if (_audio == true)
        {
                Destroy(_audio);
            _audio = gameObject.AddComponent<AudioSource>();
            _audio.playOnAwake = true;
            _audio.pitch = 1.4f;
            _audio.priority = 256;
            _audio.volume = soundVol;
            _audio.clip = winClip;
            _audio.Play();
        }

        */



        // ECHEC
        if (other.CompareTag("Cube2"))
        {  // DéSACTIVATION DU FLOW

         //   camAnimator.SetTrigger("Triggered");           
            camAnimator.SetBool("Flow", false);   
           // mainCam.fieldOfView = startFOV;
            LevelScript._audio2.volume = 0;

            combo = 0;
            fail++;
            if(fail >= gameOver)
            {
                AxisPlayer controllerScript = transform.parent.gameObject.GetComponent<AxisPlayer>();
                Destroy(controllerScript);
                Collider box = gameObject.GetComponent<Collider>();
                Destroy(box);
                Destroy(this);
                GameControl.instance.GameOver();
              //  return;
            }


            //  StopCoroutine(ShakeCamera());
            //   StartCoroutine( ShakeCamera() );
#if UNITY_STANDALONE
            FlowFx.SetActive(false);
            Tunnel.SetActive(true);
#endif
            if(ParentScript == null)
            {
                Debug.Log("Rosas Cube Script Missing"); 
                ParentScript = other.transform.parent.GetComponent<RosasCubes>();
            }
            else
            {
               if (!ParentScript.playerCol) 
               {             
                   ParentScript.Fail();
               }
            }

            return; // Le joueur ne peut pas valider une rosas si il touche le mauvais
        }
      
        // REUSSITE
        if (other.CompareTag("Cube1")) 
        {

           // StartCoroutine(LittleFlow());

            combo++;
            if(combo >= flow) // ACTIVATION DU FLOW
            {
              //  camAnimator.SetBool("Flow", true);
              //  mainCam.fieldOfView = 70;

              //  LevelScript._audio2.volume = 1;
#if UNITY_STANDALONE

                Tunnel.SetActive(false);
                FlowFx.SetActive(true);
                SparklesPS.Play();
#endif
            }

            GameControl.instance.Score(true); // Incrémenter le score

            if (!ParentScript.playerCol)
            {
                ParentScript.Success();
            }

            //   other.gameObject.AddComponent<Rigidbody>();
           
        }

      

      // SetCursorPos(808, 457);
       // SetCursorPos(Mathf.RoundToInt(Screen.width / 2), Mathf.RoundToInt(Screen.height / 2));
    }

    
      


}
