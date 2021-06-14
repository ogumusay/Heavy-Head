using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip sound;
    [SerializeField] float jumpSpeed = 25f;
    float direction;

    private void Start()
    {
        animator = GetComponent<Animator>();
        direction = transform.localScale.y;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground Check"))
        {
            animator.SetTrigger("Bounce");
            audioSource.PlayOneShot(sound);
            collision.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction < 0)
        {
            if (collision.collider.CompareTag("Player"))
            {
                ContactPoint2D contactPoint2D = collision.contacts[0];

                if (contactPoint2D.normal == new Vector2(0, 1))
                {
                    AudioSource.PlayClipAtPoint(sound, transform.position);
                    collision.collider.attachedRigidbody.velocity = new Vector2(0f, -jumpSpeed);
                }
            }
        }
    }
}
