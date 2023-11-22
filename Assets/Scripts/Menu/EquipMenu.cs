using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EquipMenu : MonoBehaviour
{

    [SerializeField]List<GameObject> weaponButtons;
    [SerializeField]List<Image> weaponIcons;

    public void OnEnable()
    {
        CheckWeapons();
    }

    public void EquipWeapon(int weaponSlot)
    {
        WeaponManager.ChangeWeapon(weaponSlot);
        EventManager.EquipMenuEvent();
    }


    public void CheckWeapons() 
    {
        var enumerator = WeaponManager.weapons.GetEnumerator();
  
        int buttonIndex = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                weaponIcons[buttonIndex].enabled=true;
                weaponButtons[buttonIndex].GetComponent<Button>().enabled = true;
            }
            buttonIndex++;
        }
        enumerator.Dispose();


    }
}
