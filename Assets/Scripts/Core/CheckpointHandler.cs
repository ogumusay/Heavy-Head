using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    public Vector3 lastPosition;
    List<Collectable> collectables = new List<Collectable>();

    private void Start()
    {
        lastPosition = FindObjectOfType<PlayerController>().transform.position;
    }

    void RespawnPlayer()
    {
        CheckCollectables();

        PlayerController player = FindObjectOfType<PlayerController>(true);
        MainCamera camera = FindObjectOfType<MainCamera>();


        player.transform.position = lastPosition;
        player.isBusy = false;
        player.boxCollider2D.enabled = true;
        player.rigidbody.isKinematic = false;


        Vector3 playerPos = player.transform.position;
        camera.transform.position = new Vector3(playerPos.x, playerPos.y, camera.transform.position.z);
    }

    private void CheckCollectables()
    {
        if (collectables.Count < 5)
        {
            FindObjectOfType<ExitLevel>().CloseDoor();
        }

        if (collectables.Count != GetLastObjects().Count)
        {
            foreach (Collectable collectable in GetLastObjects())
            {
                if (!collectables.Contains(collectable))
                {
                    collectable.gameObject.SetActive(true);
                    FindObjectOfType<CollectableHandler>().SetInactiveLast();
                }
            }
        }
    }

    public void GetObjects()
    {
        foreach (Collectable item in FindObjectsOfType<Collectable>(true))
        {
            if (!item.gameObject.activeSelf && !collectables.Contains(item))
            {
                collectables.Add(item);
            }
        }
    }

    public List<Collectable> GetLastObjects()
    {
        List<Collectable> collectables = new List<Collectable>();

        foreach (Collectable item in FindObjectsOfType<Collectable>(true))
        {
            if (!item.gameObject.activeSelf)
            {
                collectables.Add(item);
            }
        }

        return collectables;

    }

    private void OnEnable()
    {
        PlayerController.onDie += RespawnPlayer;
    }

    private void OnDisable()
    {
        PlayerController.onDie -= RespawnPlayer;
    }

}
