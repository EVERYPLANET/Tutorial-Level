using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int priority = 6;
    public CinemachineVirtualCamera changeTo;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeTo.Priority = priority;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeTo.Priority = 0;
        }
    }
}
