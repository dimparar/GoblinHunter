using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private TextMeshProUGUI _notifText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private VisualEffect healthVFX;
    [SerializeField] private VisualEffect healthVFX_0;
    [SerializeField] private Image healthRadialFX;
    [SerializeField] private Image damageRadialFX;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject Parent;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        EventManager.PlayerDeathEvent += ShowDeathScreen;
        EventManager.PlayerHitEvent += PlayDamageVfx;
    }
    private void Start()
    {
        /*StopHealthAnimation();*/
        healthBar.value = 100;
        staminaBar.value = 50;
    }

    void Update()
    {

        healthBar.value = GetComponent<PlayerController>().health;
        staminaBar.value = GetComponent<PlayerController>().stamina;

    }

    public void updateText(string text)
    {
        _notifText.text = text;
    }

    public void crosshairInteraction()
    {
        _crosshair.color = Color.red;
    }

    public void crosshairNoInteraction()
    {
        _crosshair.color = Color.black;
    }

    public void PlayHealthVFX()
    {
        StartCoroutine(Effects.StartFade(healthRadialFX, 0.5f, 1f));
        Invoke("DisableHealthlVfx", 1f);
    }

    public void DisableHealthlVfx()
    {
        StartCoroutine(Effects.StartFade(healthRadialFX, 0.5f, 0f));
        healthVFX.Stop();
        healthVFX_0.Stop();
    }
    private void DisableDamageVfx()
    {
        StartCoroutine(Effects.StartFade(damageRadialFX, 0.5f, 0f));
    }


    private void PlayDamageVfx()
    {
        StartCoroutine(Effects.StartFade(damageRadialFX, 0.5f, 1f));
        Invoke("DisableDamageVfx", 1f);
    }

    public void ShowDeathScreen() 
    {
       GameObject deathScreenObject = Instantiate(deathScreen);
       deathScreenObject.transform.SetParent(Parent.transform);
       deathScreenObject.transform.localPosition = new Vector3(0, 0, 0);
    }

}
