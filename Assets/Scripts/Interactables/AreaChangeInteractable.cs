using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class AreaChangeInteractable : Interactable
{
    [SerializeField] string targetLocation;
    public override string GetPromptMessage()
    {
       return "Press E to Enter"+ targetLocation;
    }

    public override void Interact()
    {
        SceneManager.LoadScene(targetLocation);
    }
}
