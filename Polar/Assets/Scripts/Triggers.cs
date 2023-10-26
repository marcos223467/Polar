using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public bool CogePistolas;
    

    private void OnTriggerEnter(Collider other)
    {
        if (CogePistolas && other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Player>().pistolas = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CogePistolas && other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Player>().pistolas = false;
        }
    }
}
