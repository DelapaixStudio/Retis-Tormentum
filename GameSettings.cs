using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    AxisPlayer playerScript;

    [HideInInspector] public Toggle invertAxisButton;
    [HideInInspector] public Slider sensitivitySlider;
    [SerializeField] Slider volumeSlider;

    bool mustSave;


    private void OnEnable()
    {
        volumeSlider.value = AudioListener.volume;
    }

    private void OnDisable()
    {
        if (mustSave)
        {
            mustSave = false;
            SaveManager.SaveInstance.SaveData();
        }
    }

    public void ChangeAxis(bool invert)
    {
        playerScript.invertAxis = invert;
        if(invert != SaveManager.SaveInstance.InvertAxis)
        {
            mustSave = true;  
            SaveManager.SaveInstance.InvertAxis = invert;
        }
    }

    public void ChangeSensitivity(float value)
    {
        Debug.Log("Change Sensitiv" + value);
        playerScript.yAxisSpeed = value;
        if (value != SaveManager.SaveInstance.MouseSensitivity)
        {
            mustSave = true;
            SaveManager.SaveInstance.MouseSensitivity = value;
        }
    }

    public void SetVolume(float value)
    {
        Debug.Log("Change Volume" + value);

        SaveManager.SaveInstance.sessionVolume = value;
        AudioListener.volume = value;        
    }
}
