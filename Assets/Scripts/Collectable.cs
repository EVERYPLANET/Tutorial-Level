using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    //public UnityEvent enteredInteraction, exitedInteraction, interacted;
    private bool interactionEnabled;

    private PlayerController playerC;

    public string itemID;
    public int quantity;
    public GameObject tooltip;

    public bool destroy;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            interactionEnabled = true;
            playerC = col.gameObject.GetComponent<PlayerController>();
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            interactionEnabled = false;
            tooltip.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionEnabled && Input.GetKeyDown(KeyCode.E))
        {
            playerC.addInventory(itemID, quantity);
            if(destroy) Destroy(this.gameObject);
            if(destroy) Destroy(tooltip);
        }
    }
}