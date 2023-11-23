using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public bool CogePistolas;

    private void OnTriggerStay(Collider other)
    {
        if (CogePistolas && other.gameObject.layer == 8 && other.gameObject.GetComponent<Player>().pistolas == false)
        {
            other.gameObject.GetComponent<Player>().ActivaPistolas();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.GetComponent<Player>().pistolas)
        {
            Desactivar();
        }
    }

    private void Desactivar()
    {
        this.gameObject.SetActive(false);
    }
}
