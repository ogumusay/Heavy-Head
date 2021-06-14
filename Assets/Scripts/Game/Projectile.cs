using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LeanTweenType easeType;
    public float speed = 5f;
    public int direction = -1;
    public float waveHeight = 1f;
    public bool isTweening = false;

    private void Start()
    {
        if (isTweening)
        {
            LeanTween.moveY(gameObject, transform.position.y + waveHeight, 1f).setLoopPingPong().setEase(easeType);
        }
    }

    private void Update()
    {
        float stepX = transform.position.x + direction * (Time.deltaTime * speed);
        transform.position = new Vector2(stepX, transform.position.y); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            DamageType type = GetComponent<DealDamage>().type;
            player.StartCoroutine(player.Die(type));
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
