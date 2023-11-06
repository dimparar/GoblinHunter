using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bread : Interactable
{
    private PlayerUI _playerUI;

    private int maxHealthRegen = 10; //TODO

    private void Start()
    {
        _playerUI = GameObject.Find("Player").GetComponent<PlayerUI>();
    }

    public override string GetPromptMessage()
    {
        return "";

    }

    public override void Interact()
    {
        _playerUI.PlayHealthVFX();

        // Increase Health

        // Destroy Item

    }

}
