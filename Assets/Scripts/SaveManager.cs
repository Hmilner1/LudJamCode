using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static string directory;
    public static string fileName;

    public static void SaveGame(Save save)
    {
        if (!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetFullPath());
        bf.Serialize(file, save);
        file.Close();
    }

    public static Save LoadGame()
    {
        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFullPath(), FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                return save;
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load file");
            }
        }

        return null;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetFullPath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }

    private static string GetFullPath()
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }
}
