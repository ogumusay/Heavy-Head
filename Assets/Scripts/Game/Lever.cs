using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public delegate void LeverEvent(int[] gate_ids);
    public static event LeverEvent onToggleLever;

    [SerializeField] int[] gate_ids;
    [SerializeField] Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("ToggleLever");        
    }

    public void InvokeEvent()
    {
        onToggleLever?.Invoke(gate_ids);
    }

    public void PlaySoundEffect()
    {
        audioSource.Play();
    }
}
