using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] LeanTweenType easeType;


    AudioSource audioSource;
    Vector2 instantiatePosition;
    Animator animator;

    [SerializeField]
    [Range(0.1f, 2f)]
    float animationSpeed = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        animator.speed = animationSpeed;

        if (gameObject.transform.localScale.x > 0)
        {
            instantiatePosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
        }
        else
        {
            instantiatePosition = new Vector2(transform.position.x - xOffset, transform.position.y + yOffset);
        }

    }

    public void Shoot()
    {
        if (gameObject.transform.localScale.x < 0)
        {
            projectilePrefab.direction = 1;
            projectilePrefab.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            projectilePrefab.direction = -1;
            projectilePrefab.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        Projectile projectile = projectilePrefab;
        projectile.easeType = easeType;

        Instantiate(projectile, instantiatePosition, Quaternion.identity);

    }

    public void PlaySoundEffect()
    {
        audioSource.Play();
    }
}
