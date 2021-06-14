using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] float[] patrolPoints;
    Animator animator;
    BoxCollider2D boxCollider2D;

    int currentPointIndex = 0;
    public bool isAlive = true;
    float speed = 3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isAlive)
        {
            Vector2 targetVector = new Vector2(patrolPoints[currentPointIndex], transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetVector, Time.deltaTime * 2);

            if (Vector2.Distance(transform.position, targetVector) <= Mathf.Epsilon)
            {
                currentPointIndex = 1 - currentPointIndex;
            }

            transform.localScale = new Vector3(Mathf.Sign(currentPointIndex - 1) * 1f, 1f, 1f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Die();
        }
    }

    public void Die()
    {
        boxCollider2D.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
