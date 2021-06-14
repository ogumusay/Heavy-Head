using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Trap
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0]; 

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(collision.collider.IsTouchingLayers(LayerMask.GetMask("Ground")) && contact.separation < -0.03f)
            player.StartCoroutine(player.Die(DamageType.Crush));
        
    }

}
