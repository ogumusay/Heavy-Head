using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MenuMusic : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        MenuMusic[] objects = FindObjectsOfType<MenuMusic>(true);

        if (objects.Length > 1)
        {
            MenuMusic obj = objects.Where(x => x.gameObject.activeSelf == false).FirstOrDefault();
            obj?.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            audioSource.volume = musicVolume;
        }
    }
}
