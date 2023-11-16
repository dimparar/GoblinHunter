using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play() 
    {
        SceneManager.LoadScene("Forest");
    }


    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
