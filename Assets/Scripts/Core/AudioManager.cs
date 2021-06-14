using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioClip exitSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] float exitSoundVolume = 0.5f;
    [SerializeField] public AudioMixer audioMixer;
    public AudioSource audioSource;



    private void Awake()
    {
        MenuMusic obj = FindObjectOfType<MenuMusic>();
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");

            audioSource.volume = musicVolume;
        }

        SetSFXVolume(GetSFXVolume());
    }

    void PlayExitSound()
    {
        StartCoroutine(WaitForSecondsAndPlay());
    }

    void PlayWinSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
    }

    IEnumerator WaitForSecondsAndPlay()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(exitSound, exitSoundVolume);

    }

    private void OnEnable()
    {
        CollectableHandler.onAllCollected += PlayExitSound;
        ExitLevel.onPassThroughTheDoor += PlayWinSound;
    }

    private void OnDisable()
    {
        CollectableHandler.onAllCollected -= PlayExitSound;
        ExitLevel.onPassThroughTheDoor -= PlayWinSound;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sFxVol", volume);
    }

    public float GetSFXVolume()
    {
        if (PlayerPrefs.HasKey("sFxVolume"))
        {
            float volume = PlayerPrefs.GetFloat("sFxVolume");
            return volume;
        }
        else
        {
            return 0f;
        }
    }


}
