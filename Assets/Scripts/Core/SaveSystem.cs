using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
#if UNITY_ANDROID
    private static readonly string SAVE_FOLDER = Application.persistentDataPath;
#else
    private static readonly string SAVE_FOLDER = Application.dataPath;
#endif

    public static void Save(SaveObject saveObject)
    {
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);
    }

    public static SaveObject Load()
    {
        if (File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string json = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
            return saveObject;
        }
        else
        {
            return new SaveObject();
        }
    }

}
