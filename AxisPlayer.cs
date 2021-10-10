using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class AxisPlayer : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float sensitivity = 1;
    float distFromCam = 6;
    [SerializeField] float radius = 2f;


    Vector3 mouseDir; /// Non utilisé mais présent dans MouseDirection()

    float yRot;
    float xRot; /// Non utilisé mais présent dans JoystickDirection()

    float h1;
    float v1;

    Transform sphere;

    [SerializeField] bool rotateWithHorizontalAxis = false;
    public float yAxisSpeed = 1;
    public bool invertAxis = true;
    [SerializeField] float MobileSpeed = 1;

    Vector3 startPos, startSpherePos;

    Vector3 MouseDirOnClick;


    private void Awake()
    {   
        sphere = transform.GetChild(0);
        
        transform.position = Camera.main.transform.position + new Vector3(0, 0, distFromCam);
        sphere.localPosition = new Vector3(0, radius, 0);

        startPos = transform.position;
        startSpherePos = sphere.localPosition;
    }

    private void Start()
    {
        yAxisSpeed = SaveManager.SaveInstance.MouseSensitivity;
        invertAxis = SaveManager.SaveInstance.InvertAxis;
    }


    void Update()
    {
        if (Time.timeScale == 0)
            return;


        transform.position = startPos;
        sphere.localPosition = startSpherePos;


#if UNITY_ANDROID

        MobileRotation();

#endif

#if UNITY_STANDALONE
            
        Cursor.visible = false;


        MouseDirection();
        if (yRot != 0)
        {
            RotateWithHorizontalInput();
        }

        JoystickDirection();
        if (h1 != 0 || v1 != 0)
        {
            JoystickRotation();
        }

#endif


    }    

    void JoystickDirection()
    {
         h1 = -CrossPlatformInputManager.GetAxis("RightJoystickY");
         v1 = CrossPlatformInputManager.GetAxis("RightJoystickX");
    }

    void JoystickRotation()
    {       
        if (h1 != 0 || v1 != 0) // Manette
        {          
            float target = Mathf.Atan2(v1, h1) * 180 / Mathf.PI;
            float curZRot = transform.parent.localEulerAngles.z;
            transform.localEulerAngles = new Vector3(0f, 0, -target);
            ///Debug.Log(target);
        }
    }   

    void RotateWithHorizontalInput()
    {      
        if (yRot != 0)
        {
                float direction = yRot * yAxisSpeed;
                if (invertAxis)
                    direction = -direction;
            transform.Rotate(0, 0, direction);
            return;
        }    
    }

    void MouseDirection()
    {
        yRot = CrossPlatformInputManager.GetAxis("Mouse X") * speed;
       // xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * speed;
       // mouseDir = new Vector3(yRot, xRot, 0);
    }


    /// MobileRot()
#if UNITY_ANDROID
    void MobileRotation()
    {
        if (Input.touchCount == 1)
        {
            float direction = 1;
            var touch = Input.touches[0];
            if (touch.position.x < Screen.width / 2) /// Doigt sur la partie GAUCHE
            {
                direction = -1;
            }
            else if (touch.position.x > Screen.width / 2) /// Doigt sur la partie DROITE
            {
                
            }

            direction *= MobileSpeed;
            
            if (invertAxis)
                direction = -direction;
            transform.Rotate(0, 0, direction);
        }
    }

#endif
     
}
