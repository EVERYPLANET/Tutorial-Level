using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    public Canvas hintCanvas;
    public GameObject controls;
    private HingeJoint[] hinges;
    
    void Start()
    {
        hinges = GetComponentsInChildren<HingeJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(hintCanvas);
            Rigidbody2D controlsRB = controls.GetComponent<Rigidbody2D>();
            controlsRB.gravityScale = 1;
            foreach (HingeJoint hinge in hinges)
            {
                Destroy(hinge);
            }
        }
    }
}
