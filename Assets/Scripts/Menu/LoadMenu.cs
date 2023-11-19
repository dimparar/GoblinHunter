using System.Data;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;


public class LoadMenu : MonoBehaviour
{
    [SerializeField]GameObject SaveSlotPrefab;
    public static Action RefreshSaveList;
    private void OnEnable()
    {
        RefreshSaveList += ShowSaveList;
    }

    private void OnDisable()
    {
        RefreshSaveList -= ShowSaveList;
    }

    public void ShowSaveList()
    {
        if (SaveSystem.savePaths != null)
        {
            GameObject loadList = GameObject.Find("LoadList");
            foreach (Transform child in loadList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (string path in SaveSystem.savePaths)
            {
                if (File.Exists(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);
                    PlayerData data = bf.Deserialize(stream) as PlayerData;
                    GameObject saveSlot = Instantiate(SaveSlotPrefab);
                    TextMeshProUGUI[] textFields = saveSlot.GetComponentsInChildren<TextMeshProUGUI>();
                    textFields[0].text = data.playerName;
                    textFields[2].text = "1";
                    textFields[4].text = data.location;
                    textFields[6].text = data.kills.ToString();
                    textFields[8].text = path;
                    saveSlot.transform.SetParent(loadList.transform);
                    saveSlot.transform.localScale = Vector3.one;

                    stream.Close();
                }
                else 
                {
                    Debug.Log("File Missing");
                }
            }

        }
        else 
        {
            Debug.Log("List of save paths was null");
        }
    }


    public void DeleteSaveSlot(GameObject saveSlot) 
    {

    }

}
