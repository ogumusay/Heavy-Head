using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    float speed = 90f;

    [SerializeField]
    [Range(min: -1, max: 1)]
    int direction = 1;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, direction * speed * Time.deltaTime));
    }
}
