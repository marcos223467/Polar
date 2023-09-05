using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private string _tipo;
    [SerializeField] private float f_disparo;

    private Rigidbody _rb;

    private MeshRenderer _meshRenderer;

    private bool shot;

    [SerializeField] private Transform canon;

    private Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        shot = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        origPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) //Layer de los objetos interactables
        {
            other.gameObject.GetComponent<Interactable>().setPolaridad(_tipo);
            Restart();
        }
    }

    public void Disparar(float anguloTiro)
    {
        shot = true;
        transform.position = canon.position;
        _meshRenderer.enabled = true;
        canon.Rotate(Vector3.up, anguloTiro);
        _rb.AddForce(canon.forward * f_disparo, ForceMode.Impulse);
        
        canon.Rotate(Vector3.up, -anguloTiro);
        
        Invoke("Restart", 2f);
    }

    public bool getShot()
    {
        return shot;
    }

    private void Restart()
    {
        transform.position = origPos;
        _rb.velocity = Vector3.zero;
        _meshRenderer.enabled = false;
        shot = false;
    }
}
