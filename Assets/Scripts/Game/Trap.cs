using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected AudioSource audioSource;
    Animator animator;
    [SerializeField] float delayTime;
    [SerializeField] float animationSpeed = 1f;
    float startTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;

        if (delayTime != 0)
        {
            animator.enabled = false;
        }
    }

    private void Update()
    {
        if (!animator.enabled)
        {
            startTime += Time.deltaTime;

            if (startTime >= delayTime)
            {
                animator.enabled = true;
            }
        }
    }

    public void PlaySoundEffect()
    {
        audioSource.Play();
    }

}
