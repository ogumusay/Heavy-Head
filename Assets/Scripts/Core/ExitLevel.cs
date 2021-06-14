using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] GameObject closedDoor;
    [SerializeField] GameObject openedDoor;
    [SerializeField] Warning warningObject;

    public delegate void Door();
    public static event Door onPassThroughTheDoor;


    private void OnEnable()
    {
        CollectableHandler.onAllCollected += OpenDoor;
    }

    private void OnDisable()
    {
        CollectableHandler.onAllCollected -= OpenDoor;
    }

    private void OpenDoor()
    {
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);
    }

    public void CloseDoor()
    {
        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
        warningObject.boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (openedDoor.activeSelf)
        {
            onPassThroughTheDoor?.Invoke();
        }       
    }
}
