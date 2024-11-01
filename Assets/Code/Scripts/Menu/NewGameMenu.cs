using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGameMenu : MonoBehaviour
{

    public void ValidateInput(GameObject errorMsgPanel) 
    {
        TMP_InputField PlayerName = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
        string text = PlayerName.text;
        string pattern = "^[a-zA-Z0-9]*$";
        if (Regex.IsMatch(text, pattern) && text!=string.Empty)
        {
            PlayerProfile.NewGameData(text);
            LoadScene();
        }
        else 
        {
            errorMsgPanel.SetActive(true);
        }
        PlayerName.text = string.Empty;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Forest");
    }

}
