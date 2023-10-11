using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Abrir();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Cerrar();
        }
    }

    private void Abrir()
    {
        _animator.SetBool("Abrir", true);
    }

    private void Cerrar()
    {
        _animator.SetBool("Abrir", false);
    }
}
