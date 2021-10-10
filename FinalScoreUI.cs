using UnityEngine.UI;
using UnityEngine;

public class FinalScoreUI : Menu
{
    [SerializeField]
    Text finalScoreText;
    [SerializeField]
    Text bestScoreText;

    protected override void Start()
    {
        finalScoreText.text = GameControl.instance.score.ToString();
        if(GameControl.instance.levelName == "Level1")
        {
            bestScoreText.text = SaveManager.SaveInstance.scoreLevel1.ToString();
        }
        if (GameControl.instance.levelName == "Level1Hard")
        {
            bestScoreText.text = "Best Score : " + SaveManager.SaveInstance.scoreLevel1Hard.ToString();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

}
