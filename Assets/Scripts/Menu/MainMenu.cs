using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        if (SaveSystem.savePaths != null) 
        {
            if (SaveSystem.savePaths.Count == 0)
            {
                GameObject loadButton = GameObject.Find("LoadButton");
                Color transparentColor = loadButton.GetComponentInChildren<TextMeshProUGUI>().color;
                transparentColor.a = 0.5f;
                loadButton.GetComponentInChildren<TextMeshProUGUI>().faceColor = transparentColor;
                loadButton.GetComponent<Button>().enabled = false;
                transparentColor = loadButton.GetComponent<Image>().color;
                transparentColor.a = 0f;
                loadButton.GetComponent<Image>().color = transparentColor; 
            }
            else
            {
                GameObject loadButton = GameObject.Find("LoadButton");
                Color fullColor = loadButton.GetComponentInChildren<TextMeshProUGUI>().color;
                fullColor.a = 1f;
                loadButton.GetComponentInChildren<TextMeshProUGUI>().faceColor = fullColor;
                loadButton.GetComponent<Button>().enabled = true;
                fullColor = loadButton.GetComponent<Image>().color;
                fullColor.a = 1f;
                loadButton.GetComponent<Image>().color = fullColor;
            }
        }
    }


    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
