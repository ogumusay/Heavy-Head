using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableHandler : MonoBehaviour
{
    List<RectTransform> objects = new List<RectTransform>();

    int collectableCount = 0;
    int totalCollectable = 5;
    public delegate void Collectable();
    public static event Collectable onAllCollected;


    private void Start()
    {
        foreach (RectTransform image in transform)
        {
            objects.Add(image);
        }
    }


    public void Collect()
    {
        RectTransform gameObject = objects[collectableCount];

        Image image = gameObject.GetComponent<Image>();

        Color color = image.color;

        color.a = 1;

        image.color = color;

        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setLoopPingPong(1);

        collectableCount++;

        if (collectableCount >= totalCollectable)
        {
            onAllCollected?.Invoke();
        }
    }

    public void SetInactiveLast()
    {
        RectTransform gameObject = objects[collectableCount - 1];

        Image image = gameObject.GetComponent<Image>();

        Color color = image.color;

        color.a = 0.6f;

        image.color = color;

        collectableCount -= 1;
    }
}
