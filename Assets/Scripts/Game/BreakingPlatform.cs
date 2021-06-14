using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    [SerializeField] Collider2D boxCollider;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip fallSound;
    AudioSource audioSource;

    [SerializeField] float crumbleTime = 0.5f;
    [SerializeField] float respawnTime = 0.5f;
    [SerializeField] float animationSpeed = 1f;

    bool isStarted = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator.speed = animationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground Check"))
        {
            if (!isStarted)
            {
                audioSource.Play();
                animator.SetTrigger("Shake");
                StartCoroutine(SetInactiveDelay(crumbleTime, respawnTime));
                isStarted = true;
            }
        }
    } 

    IEnumerator SetInactiveDelay(float crumbleTime, float respawnTime)
    {
        yield return new WaitForSeconds(crumbleTime);

        boxCollider.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(fallSound);
        animator.SetTrigger("Crumble");

        yield return new WaitForSeconds(respawnTime);

        boxCollider.enabled = true;
        animator.SetTrigger("Reassemble");

        isStarted = false;
    }
}
