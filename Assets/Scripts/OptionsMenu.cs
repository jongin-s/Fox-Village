using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Graphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void Volume(float vol)
    {
        mixer.SetFloat("vol", vol);
    }
}
