using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{

    public GameObject elevator;
    public int elevatorForce = 50;
    private float timer = 0;

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer <= 0 && (other.CompareTag("Player") || other.CompareTag("Ball")))
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, -2.5f, transform.localScale.z);
            elevator.GetComponent<Rigidbody>().AddForce(Vector2.up * elevatorForce,ForceMode.Impulse);
            timer = 6;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.localScale = new Vector3(transform.localScale.x, -3.2f, transform.localScale.z);
    }
}
