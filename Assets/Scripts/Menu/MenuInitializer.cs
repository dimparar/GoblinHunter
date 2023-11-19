using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    [SerializeField] GameObject startingMenuPanel;
        
    void Start()
    {
        startingMenuPanel.SetActive(true);
    }

}
