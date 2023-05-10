using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactable : MonoBehaviour
{
    public UnityEvent enteredInteraction, exitedInteraction, interacted;
    private bool interactionEnabled;


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            enteredInteraction.Invoke();
            interactionEnabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            exitedInteraction.Invoke();
            interactionEnabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionEnabled && Input.GetKeyDown(KeyCode.E))
        {
            exitedInteraction.Invoke();
            interacted.Invoke();
        }
    }
}
