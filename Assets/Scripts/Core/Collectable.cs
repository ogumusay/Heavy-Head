using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableHandler collectableHandler;
    [SerializeField] GameObject vfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().PlayCollectSound();
        collectableHandler.Collect();
        GameObject vfxObject = Instantiate(vfx, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfxObject, 1f);
        gameObject.SetActive(false);
    }
}
