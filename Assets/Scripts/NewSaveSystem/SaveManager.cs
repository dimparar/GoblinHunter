using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public sealed class SaveManager
{
    private static SaveManager instance;
    private static List<string> saveGamePaths;

    private SaveManager()
    {
        saveGamePaths = new List<string>();
    }

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveManager();
            }
            return instance;
        }
    }

    public static void SaveGame() 
    {
        //SaveGame Logic
        if (saveGamePaths == null)
        {
            saveGamePaths = new List<string>();
        }
        Debug.Log("Saving");
        GameData data = PlayerProfile.gameData;
        data.time = DateTime.Now.ToString("dd/MM HH:mm:ss"); ;
        string path = Application.persistentDataPath + "/" + data.playerData.playerName + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".json";

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        Debug.Log(json);
        /*string json = JsonUtility.ToJson(data, true);*/


        File.WriteAllText(path, json);
        /*BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + data.playerData.playerName + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();*/
        saveGamePaths.Add(path);
        StoreSavePaths();
    }

    public static void StoreSavePaths()
    {
        if (saveGamePaths == null)
        {
            Debug.Log("No existing saves found");
            saveGamePaths = new List<string>();
            return;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveLocations.bin";
            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, saveGamePaths);
            stream.Close();
        }
    }

    public static void LoadGame(string path) 
    {
        string loadedJsonData = File.ReadAllText(path);
/*
        GameData data = JsonUtility.FromJson<GameData>(loadedJsonData);*/
        Debug.Log(loadedJsonData);
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new Vector3Converter());
        GameData data = JsonConvert.DeserializeObject<GameData>(loadedJsonData,settings);

        PlayerProfile.LoadGameData(data);
        SceneManager.LoadScene(data.currentArea.GetAreaName());
        //isos kai kati allo?
        /*        BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                GameData data = bf.Deserialize(stream) as GameData;*/
/*
        PlayerProfile.LoadGameData(data);
        SceneManager.LoadScene(data.currentArea.GetAreaName());*/

    }

    public static List<string> GetSaveFilePaths() 
    {
        return saveGamePaths;
    }

    public static void InitializePaths(List<string> paths)
    {
        saveGamePaths = paths;
    }
}
