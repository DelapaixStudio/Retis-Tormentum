using UnityEngine.UI;
using UnityEngine;

public class DetectNextHit : MonoBehaviour
{
    [SerializeField] float distFromOrigin = 1;
    [SerializeField] Transform Origin;
    [SerializeField] Manette script;


    private void Start()
    {
        transform.position = Origin.position - (Vector3.forward * distFromOrigin);
    }

    private void OnTriggerEnter(Collider other)
    {
        script.DisplayNextHit(true);
    }

  
}
