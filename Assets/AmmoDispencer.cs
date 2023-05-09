using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoDispencer : MonoBehaviour
{
    public GameObject tooltip;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        tooltip.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tooltip.SetActive(false);
    }
}
