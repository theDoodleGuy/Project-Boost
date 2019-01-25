using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] float thrust = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "friendly":
                print("do noting");
                break;
            default:
                print("DIE");
                break;
        }
    }

    private void Thrust()
    {
        var boost = Vector3.up * thrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(boost);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rb.freezeRotation = true;
        var rotationThisFrame = Vector3.forward * rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(rotationThisFrame);
        }
        rb.freezeRotation = false;
    }
}