using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private string _tipo;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private float f_disparo;

    private Rigidbody _rb;

    private MeshRenderer _meshRenderer;

    private bool shot;

    [SerializeField] private Transform canon;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        shot = false;
        _meshRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == interactable) //
        {
            //Hago cosas
            shot = false;
            _meshRenderer.enabled = false;
        }
    }

    public void Disparar()
    {
        shot = true;
        _meshRenderer.enabled = true;
        _rb.AddForce(canon.forward * f_disparo, ForceMode.Impulse);
    }

    public bool getShot()
    {
        return shot;
    }
}
