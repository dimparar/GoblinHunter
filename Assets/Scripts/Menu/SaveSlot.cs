using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    public void DeleteSaveSlot(GameObject saveSlot)
    {
        TextMeshProUGUI[] textFields = saveSlot.GetComponentsInChildren<TextMeshProUGUI>();
        string path = textFields[8].text;
        if (File.Exists(path)) 
        {
            File.Delete(path);
        }
        SaveSystem.savePaths.Remove(path);
        SaveSystem.SavePaths();
        LoadMenu.RefreshSaveList.Invoke();
    }
    public void LoadSlot(TextMeshProUGUI path)
    {
        SaveSystem.Load(path.text);
    }
}
