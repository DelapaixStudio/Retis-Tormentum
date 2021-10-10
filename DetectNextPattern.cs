using UnityEngine;

public class DetectNextPattern : MonoBehaviour
{


    [SerializeField] float distFromOrigin = 1;

    Transform OriginHG, OriginHD, OriginBG, OriginBD;
    GameObject DetectHG, DetectHD, DetectBG, DetectBD;



    // 0 = HG
    // 1 = HD
    // 2 = BG 
    // 3 = BD
    [SerializeField] Transform[] Origin = new Transform[4];
    [SerializeField] GameObject[] Detect = new GameObject[4];
    bool[] col = new bool[4];

    void Start()
    {
        /*
        DetectHG = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DetectHD = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DetectBG = GameObject.CreatePrimitive(PrimitiveType.Cube);
        DetectBD = GameObject.CreatePrimitive(PrimitiveType.Cube);

        DetectHG.AddComponent(typeof(BoxCollider));
        DetectHD.AddComponent(typeof(BoxCollider));
        DetectBG.AddComponent(typeof(BoxCollider));
        DetectBD.AddComponent(typeof(BoxCollider));
        */

        //DetectionCube = new GameObject();

        int i = 0;
        foreach(Transform child in transform)
        {
            Detect[i] = child.gameObject;
            i++;
        }

        for(i = 0; i < Origin.Length; i++)
        {                        
            Detect[i].transform.position = Origin[i].position - (Vector3.forward * distFromOrigin);            
        }
    }

    // ENVOYER MESSAGE DEPUIS LES CUBES DE DETECTIONS VERS CE SCRIPT EN CAS DE TRIGGER OU VERS LE SCRIPT UI DIRECTEMENT ?


    void Update()
    {
        
    }
}
