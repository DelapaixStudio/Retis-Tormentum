using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FxsRotation : MonoBehaviour
{

    [SerializeField] ParticleSystem leftPs, rightPs;
    [SerializeField] ParticleSystem ps;

    [SerializeField] float angle = 25;

    void Start()
    {
        leftPs.transform.eulerAngles = new Vector3(0, angle, 0);
        return;
        rightPs = Instantiate(leftPs);
        rightPs.transform.parent = leftPs.transform.parent;
        rightPs.transform.position = leftPs.transform.position;
        rightPs.transform.eulerAngles = new Vector3(0, -angle, 0);
        leftPs.gameObject.SetActive(false);
        rightPs.gameObject.SetActive(false);

    }

    void Update()
    {
        float rot = CrossPlatformInputManager.GetAxis("Mouse X");
        float target = leftPs.transform.eulerAngles.y;
        var main = ps.main;
        float speed = 20;
        if(rot > 0) // Rotation à droite
        {           
            main.startSpeed = speed;
            target = angle;
            /*
            leftPs.gameObject.SetActive(true);
            rightPs.gameObject.SetActive(false);*/
        }
        if(rot < 0) // Rotation à gauche
        {
            main.startSpeed = -speed;

            target = -angle;
            /*
            leftPs.gameObject.SetActive(false);
            rightPs.gameObject.SetActive(true);*/
        }

        //leftPs.transform.eulerAngles = new Vector3(0, Mathf.Lerp(leftPs.transform.eulerAngles.y, target, Time.deltaTime), 0);
    }
}
