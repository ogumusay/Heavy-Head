using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    [SerializeField] GameObject purplePortal;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            audioSource.PlayOneShot(sound);

            collision.gameObject.transform.position = purplePortal.transform.position;

            MainCamera camera = FindObjectOfType<MainCamera>();
            camera.transform.position = new Vector3(purplePortal.transform.position.x, purplePortal.transform.position.y, camera.transform.position.z);

        }
    }

}
