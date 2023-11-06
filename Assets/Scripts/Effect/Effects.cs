using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public static class Effects
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    public static IEnumerator StartFade(UnityEngine.UI.Image img, float duration, float targetVolume)
    {
        float currentTime = 0;
        UnityEngine.Color start = img.color;
/*        float start = color;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            start.a = Mathf.Lerp(start.a, targetVolume, currentTime / duration);
            img.color = start;
            yield return null;
        }
        yield break;
    }
    public static IEnumerator Flash(SpriteRenderer sprite, float duration) 
    {
        float currentTime = 0;
        Material originalMaterial = sprite.material;
        Material flashMaterial = Object.Instantiate(sprite.material);
        Color flashColor = flashMaterial.color * 4;
        flashMaterial.color = flashColor;
        bool flash = true;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (flash)
            {
                sprite.material= flashMaterial;
                flash = false;
            }
            else 
            {
                sprite.material = originalMaterial;
                flash = true;
            }
            yield return null;
        }
        sprite.material = originalMaterial;
        yield break;
    }

}
