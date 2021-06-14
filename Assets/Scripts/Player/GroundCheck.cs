using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // CHECK THE COLLISION IF IT IS GROUND OR NOT

        if (collision.gameObject.layer == 8)
        {
            // isGrounded IS TRUE IF IT ENTERS GROUND COLLISION
            playerController.isGrounded = true;
            playerController.freeFall = false;
            playerController.animator.SetBool("isGrounded", true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // CHECK THE COLLISION IF IT IS GROUND OR NOT

        if (collision.gameObject.layer == 8)
        {
            // isGrounded IS FALSE IF IT EXITS GROUND COLLISION

            playerController.isGrounded = false;
            playerController.animator.SetBool("isGrounded", false);
        }
    }
}
