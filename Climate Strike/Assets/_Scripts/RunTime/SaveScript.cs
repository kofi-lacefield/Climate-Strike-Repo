using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveScript
{
    public static void savePlayer(OmnisceneScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.saveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerDataScript data = new PlayerDataScript(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataScript loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.saveData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerDataScript data = formatter.Deserialize(stream) as PlayerDataScript;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
