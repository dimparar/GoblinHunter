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
        SaveManager.GetSaveFilePaths().Remove(path);
        SaveManager.StoreSavePaths();
        LoadMenu.RefreshSaveList.Invoke();
    }
    public void LoadSlot(TextMeshProUGUI path)
    {
        SaveManager.LoadGame(path.text);
    }
}
