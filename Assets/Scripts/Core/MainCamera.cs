using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Animator animator;
    float minYPos;

    Vector3 targetVector;

    float cameraXOffset = 7f;
    float cameraYOffset = 3f;


    private void Start()
    {
        minYPos = FindObjectOfType<FallCheck>().yPosition + 15f;
    }
    void Update()
    {
        if (transform.position.y <= minYPos)
        {
            transform.position = new Vector3(transform.position.x, minYPos, transform.position.z);
        }
        else
        {
            float cameraXPos = player.transform.position.x + cameraXOffset * player.transform.localScale.x;
            float cameraYPos = player.transform.position.y - cameraYOffset;

            targetVector = new Vector3(cameraXPos, cameraYPos, transform.position.z);

            float timeStepX = Mathf.Abs(targetVector.x - transform.position.x) * Time.deltaTime * 2f;
            float timeStepY = Mathf.Abs(targetVector.y - transform.position.y) * Time.deltaTime * 4f;

            float timeStep = (timeStepX + timeStepY) / 2;

            float playerVelocity = player.rigidbody.velocity.magnitude;

            if (playerVelocity > 30f)
            {
                timeStep *= playerVelocity / 15f;
            }
            else if (playerVelocity > 60f)
            {
                timeStep *= 3f;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetVector, timeStep);
        }
    }

    void HammerShake()
    {
        animator.SetTrigger("HammerShake");
    }

    void ExplosionShake()
    {
        animator.SetTrigger("ExplosionShake");
    }

    private void OnEnable()
    {
        PlayerController.onAttack += HammerShake;
        PlayerController.onBlown += ExplosionShake;
    }

    private void OnDisable()
    {
        PlayerController.onAttack -= HammerShake;
        PlayerController.onBlown -= ExplosionShake;
    }
}
