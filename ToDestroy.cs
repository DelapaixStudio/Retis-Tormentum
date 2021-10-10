using System.Collections;
using UnityEngine;

public class ToDestroy : MonoBehaviour
{
    public float time = 2;
    void Start()
    {
        Destroy(gameObject, time);
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            Destroy(gameObject);
        }
    }
}
