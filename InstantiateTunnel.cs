using UnityEngine;

public class InstantiateTunnel : MonoBehaviour
{
    [SerializeField] Niveau niveau;
    [SerializeField] GameObject TunnelPrefab;
    [SerializeField] GameObject FirstTunnel;
    [SerializeField] Color[] TunnelColors;    
    Color levelColor;

    [SerializeField] Mesh[] TunnelMeshes;
    Mesh tunnelMeshLevel;

    [SerializeField] float coeffSpwan = 8;
    [SerializeField] int gap;
    GameObject lastTunnel;
    float time = 0;
    float timeToSpawn;
    Vector3 posFirstTunnel;
    void Start()
    {
        lastTunnel = FirstTunnel;
        timeToSpawn = niveau.nextSpawn * coeffSpwan;
        posFirstTunnel = FirstTunnel.transform.position;

        if(niveau == null)
        {
            Debug.Log("Niveau Script Missing !!!");
        }

        levelColor = TunnelColors[Random.Range(0, TunnelColors.Length)];
        tunnelMeshLevel = TunnelMeshes[Random.Range(0, TunnelMeshes.Length)];
    }

    void Update()
    {
        if (GameControl.instance.gameOver == true)
            return;

        time += Time.deltaTime;
        if (time >= timeToSpawn)
        {
            //GameObject tunnel = Instantiate(TunnelPrefab, lastTunnel.transform.position + new Vector3(15,0,0), Quaternion.identity);
            if(lastTunnel != null)
            {
                GameObject tunnel = Instantiate(TunnelPrefab, lastTunnel.transform.localPosition + new Vector3(0, 0, gap), Quaternion.identity);            
                tunnel.transform.parent = transform;
                tunnel.GetComponentInChildren<Light>().color = TunnelColors[Random.Range(0, TunnelColors.Length)];
                tunnel.GetComponent<MeshFilter>().mesh = tunnelMeshLevel;
                /*if(Random.Range(0, 3) == 1)
                {
                    tunnel.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().enabled = true;
                }*/
                lastTunnel = tunnel;   

            }
            else
            {
                Debug.Log("ATTENTION, DERNIER TUNNEL MANQUANT, IMPOSSIBLE D UTILISER SES COORDONNEES POUR CREER LE SUIVANT");
            }


            time = 0;
        }
    }
}
