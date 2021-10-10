[System.Serializable] 
//Sauvegarde des données du Script SaveManager.cs à partir de cette classe qui sera converti en fichier de sauvegarde

public class PlayerData    
{
    public bool InvertAxis = true;
    public float MouseSensitivity;
    public float SoundVolume;
    public int scoreLevel1, scoreLevel2, scoreLevel3;
    public int scoreLevel1Hard, scoreLevel2Hard, scoreLevel3Hard;
    public bool GameFinished;
    public bool IntroFinished;
    public bool Demo;

    public PlayerData(SaveManager playerData) // Script sur GameObject avec script SaveManager.cs
    {
        InvertAxis = playerData.InvertAxis;
        MouseSensitivity = playerData.MouseSensitivity;
        SoundVolume = playerData.SoundVolume;
        scoreLevel1 = playerData.scoreLevel1;
        scoreLevel1Hard = playerData.scoreLevel1Hard;
        scoreLevel2 = playerData.scoreLevel2;
        scoreLevel3 = playerData.scoreLevel3;
        scoreLevel2Hard = playerData.scoreLevel2Hard;
        scoreLevel3Hard = playerData.scoreLevel3Hard;
        GameFinished = playerData.GameFinished;
        IntroFinished = playerData.IntroFinished;
    }
}
