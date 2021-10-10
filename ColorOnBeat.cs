using UnityEngine;




public class ColorOnBeat : MonoBehaviour
{

    [Header("Behaviour Settings")]
    public Transform _target;
    private MeshRenderer _meshRenderer;
    private Renderer lol;
    public UnityEngine.Material _material;
    private UnityEngine.Material _materialInstance;
    
    
    public Color _color;
    public string _colorproperty;

    private float _strenght;
    [Range(0.8f, 0.99f)]
    public float _fallbackFactor;
    [Range(1, 4)]
    public float _colorMultiplier;


    [Header("Beat Settings")]
    [Range(0, 3)]
    public int _onFullBeat;
    [Range(0, 7)]
    public int[] _onBeatD8;
    private int _beatCountFull;

    void Start()
    {
     _target = this.gameObject.transform.GetChild(1).GetChild(0).transform;
        if(_target != null)
        {  
            _meshRenderer = _target.GetComponent<MeshRenderer>();
            lol = _target.GetComponent<Renderer>();
           // 
        }
        else
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            lol = GetComponent<Renderer>();
        }
        _strenght = 0;
        _materialInstance = new UnityEngine.Material(_material);
       _materialInstance.EnableKeyword("_EMISSION");
        _meshRenderer.material = _materialInstance;
      // lol.sharedMaterial = _materialInstance;
    }

    void Update()
    {
        if (_strenght > 0)
        {
            _strenght *= _fallbackFactor;
        }
        else
        {
            _strenght = 0;
        }
        CheckBeat();
        _materialInstance.SetColor(_colorproperty, _color * _strenght * _colorMultiplier);
      //  lol.material.SetColor(_colorproperty, _color * _strenght * _colorMultiplier);
    }

    void Colorize()
    {
        _strenght = 1;
    }

    void CheckBeat()
    {
        _beatCountFull = BeepRM.beatCountFull % 4;
        for (int i = 0; i < _onBeatD8.Length; i++)
        {
            if (BeepRM.beatD8 && _beatCountFull == _onFullBeat && BeepRM.beatCountD8 % 8 == _onBeatD8[i])
            {
                Colorize();
                Debug.Log("Colorize");
            }
        }
    }
}
