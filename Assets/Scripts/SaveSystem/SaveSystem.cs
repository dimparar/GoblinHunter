using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using TMPro;

public static class SaveSystem 
{

    public static List<string> savePaths;

    public static int counter = 0;
    /// <summary>
    /// Used To Test the Saving System
    /// </summary>
    public static void Save() 
    {
        Debug.Log("Saving");

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerName" + DateTime.Now.ToString("MM-dd-yyyy HH-mm") + counter+".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData();
        counter++;
        bf.Serialize(stream, data);
        stream.Close();
        savePaths.Add(path);
        SavePaths();
    }
    public static void SavePaths() 
    {
        if (savePaths == null)
        {
            Debug.Log("No existing saves found");
            savePaths = new List<string>();
            return;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveLocations.bin";
            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, savePaths);
            stream.Close();
        }

    }
    public static PlayerData Load()
    {
        return null;
    }

     public static void Initialize(List<string> savePathsList)
    {
        savePaths = savePathsList; 
    }

    public static void InitializeSave(string playerName)
    {
        if (savePaths == null)
        {
            Debug.Log("No existing saves found");
            savePaths = new List<string>();
        }
        Debug.Log("Saving");
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+ playerName + DateTime.Now.ToString("MM-dd-yyyy HH-mm") + counter + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(playerName);
        counter++;
        bf.Serialize(stream, data);
        stream.Close();
        savePaths.Add(path);
        SavePaths();
    }

    public static void Load(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        PlayerData data = bf.Deserialize(stream) as PlayerData;
        
        stream.Close();

        //PlayerData fortoma se game manager kai loadNextScene

    }
}
