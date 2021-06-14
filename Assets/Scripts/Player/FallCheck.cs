using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    [SerializeField] GameObject camera;

    [SerializeField] public float yPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.StartCoroutine(player.Die(DamageType.Pierce));
        }
    }

    private void Update()
    {
        transform.position = new Vector2(camera.transform.position.x, yPosition);
    }
}
