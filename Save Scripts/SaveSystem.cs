using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
 
    public static void SavePlayer(SaveManager playerData)
    {

       string path = SaveManager.SaveInstance.savePath;
       BinaryFormatter formatter = new BinaryFormatter();
       FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

       PlayerData data = new PlayerData(playerData);

       formatter.Serialize(stream, data);
       stream.Close();

       Debug.Log("SAVED");
       SaveManager.SaveInstance.destroySavingUI = true;
    }

    public static PlayerData LoadPlayer()
    {

        string path = SaveManager.SaveInstance.savePath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("LOADED");
            return data;
           
        }
        else
        {
            Debug.Log("Save File Not Found in" + path);
            return null;
        }
    }


}
