using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class SaveSystem
{
    public static void SaveLevelReached(int levelReached, bool editionUnlocked)
    {
        string path = GeneralManager.binaryDataPath;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(levelReached, editionUnlocked);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData GetBinarySavedData()
    {
        GameData data = null;
        string path = GeneralManager.binaryDataPath;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as GameData;

            stream.Close();

        }
        else
        {
            Debug.LogWarning("Binary file not found");
        }

        return data;
    }
}