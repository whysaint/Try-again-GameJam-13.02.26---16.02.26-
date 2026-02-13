using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Cup : MonoBehaviour
{
    public AudioSource cupDropSound;
    public float speedDropingCup;
    private Rigidbody rb;
    public bool IsReady { get; private set; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (cupDropSound != null)
        {
            cupDropSound = GetComponent<AudioSource>();
        }
    }

    public void MarkReady()
    {
        IsReady = true;
    }

    public void MarkNotReady()
    {
        IsReady = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb.linearVelocity.magnitude > speedDropingCup)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        cupDropSound.volume = Mathf.Clamp01(rb.linearVelocity.magnitude);
        Debug.Log(cupDropSound.volume);
        cupDropSound.Play();
    }
}
