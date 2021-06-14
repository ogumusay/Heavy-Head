using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBouncePlatform : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip sound;
    [SerializeField] float jumpSpeed = 50f;
    [SerializeField] float verticalSpeed = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float direction = transform.localScale.y;

        if (collision.contacts[0].normal == new Vector2(-1f * direction, 0))
        {
            audioSource.PlayOneShot(sound);
            animator.SetTrigger("Bounce");
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            player.freeFall = true;
            player.rigidbody.velocity = new Vector2(direction * jumpSpeed, /*player.rigidbody.velocity.y*/ verticalSpeed);
        }
    }
}
