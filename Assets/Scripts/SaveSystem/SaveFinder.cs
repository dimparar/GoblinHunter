using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveFinder : MonoBehaviour
{
    private void Awake()
    {
        string savesPath  = Application.persistentDataPath + "/saveLocations.bin";
        if (File.Exists(savesPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(savesPath, FileMode.Open);
            List<string> paths = bf.Deserialize(stream) as List<string>;

            SaveSystem.Initialize(paths);
        }

    }
}
