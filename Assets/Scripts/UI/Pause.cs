using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseText;


    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sFxSlider;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        volumeSlider.value = audioManager.audioSource.volume;
        sFxSlider.value = audioManager.GetSFXVolume();

        Text text = pauseText.GetComponent<Text>();
        SystemLanguage systemLanguage = Application.systemLanguage;

        switch (systemLanguage)
        {
            case SystemLanguage.Turkish:
                text.text = "DURAKLATILDI";
                break;
            case SystemLanguage.Spanish:
                text.text = "PAUSA";
                break;
            case SystemLanguage.German:
                text.text = "PAUSE";
                break;
            case SystemLanguage.French:
                text.text = "PAUSE";
                break;
            default:
                text.text = "PAUSE";
                break;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("sFxVolume", sFxSlider.value);
        PlayerPrefs.Save();
        Debug.Log("saved");
    }

    public void ChangeVolume()
    {
        audioManager.audioSource.volume = volumeSlider.value;
    }

    public void SetSFXVolume()
    {
        audioManager.SetSFXVolume(sFxSlider.value);
    }
}
