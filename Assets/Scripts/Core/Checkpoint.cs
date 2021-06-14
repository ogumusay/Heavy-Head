using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    Transform position;
    CheckpointHandler handler;
    bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            handler = FindObjectOfType<CheckpointHandler>();
            handler.GetObjects();
        }

        isTriggered = true; ;
        handler.lastPosition = position.position;
        //gameObject.SetActive(false);
    }
}
