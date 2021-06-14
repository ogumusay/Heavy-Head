using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    GameObject player;

    [SerializeField] float[] movePoints;
    Vector2 groundCheckOffset = new Vector2(0.055f, -2.57f);
    Vector2 targetVector;
    int currentPointIndex = 0;

    [SerializeField]
    [Range(min: 1, max: 3)]
    float speed = 2f;

    public bool moveVertical;
    Vector2 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!moveVertical)
        {
            targetVector = new Vector2(movePoints[currentPointIndex], transform.position.y);
        }
        else
        {
            targetVector = new Vector2(transform.position.x, movePoints[currentPointIndex]);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetVector, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, targetVector) <= Mathf.Epsilon)
        {
            currentPointIndex = 1 - currentPointIndex;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground Check"))
        {
            if (collision.attachedRigidbody.velocity.x <= 1f * speed)
            {
                collision.transform.parent.parent = transform;
            }
            else
            {
                collision.transform.parent.parent = null;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.GetChild(1).GetComponent<BoxCollider2D>().offset = new Vector2(groundCheckOffset.x, groundCheckOffset.y - 0.2f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<BoxCollider2D>().offset = groundCheckOffset;
        collision.transform.parent.parent = null;
    }
}
