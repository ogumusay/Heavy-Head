using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Animator animator;

    [SerializeField] int gate_id = 1;
    [SerializeField] bool isClosed = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isClosed", isClosed);
    }

    private void OnEnable()
    {
        Lever.onToggleLever += ToggleGate;
    }

    private void OnDisable()
    {
        Lever.onToggleLever -= ToggleGate;
    }

    void ToggleGate(int[] gate_ids)
    {
        foreach (int gate_id in gate_ids)
        {
            if (gate_id == this.gate_id)
            {
                animator.SetBool("isClosed", !animator.GetBool("isClosed"));
                break;
            }
        }
    }
}
