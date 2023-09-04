using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Deteccion")]
    [SerializeField] private Controlador _controlador;
    [SerializeField] private float minDist;
    [SerializeField] private float maxDist;
    
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;

    [SerializeField] private Material[] _materials;
    
    private string polaridad;

    private Vector3 Fuerza_Magnetica;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        polaridad = "";
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[0];
        Fuerza_Magnetica = Vector3.zero;
    }

    void FixedUpdate()
    {
        ApplyForces();
    }

    public void setPolaridad(string pol)
    {
        polaridad = polaridad == pol ? polaridad = "" : polaridad = pol;
        
        switch (polaridad)
        {
            case "":
                _meshRenderer.material = _materials[0];
                _controlador.RemoveSur(gameObject);
                _controlador.RemoveNorte(gameObject);
                break;
            case "Norte":
                _meshRenderer.material = _materials[1];
                _controlador.RemoveSur(gameObject);
                _controlador.AddNorte(gameObject);
                break;
            case "Sur":
                _meshRenderer.material = _materials[2];
                _controlador.RemoveNorte(gameObject);
                _controlador.AddSur(gameObject);
                break;
            default:
                _meshRenderer.material = _materials[0];
                _controlador.RemoveSur(gameObject);
                _controlador.RemoveNorte(gameObject);
                break;
        }
    }

    private void ApplyForces()
    {
        if (polaridad != "")
        {
            _rb.AddForce(Fuerza_Magnetica, ForceMode.Force);
            Fuerza_Magnetica = Vector3.zero;
        }
        else
        {
            Fuerza_Magnetica = Vector3.zero;
        }
            
        
    }
    

    private string getPolaridad()
    {
        return polaridad;
    }

    public void CalculaFuerzaAtraccion(Rigidbody rb, float fm)
    {
        Vector3 direccion = new Vector3(rb.position.x - _rb.position.x, rb.position.y - _rb.position.y,
            rb.position.z - _rb.position.z).normalized;
        Fuerza_Magnetica += direccion * (fm / Mathf.Clamp(Vector3.Distance(_rb.position, rb.position), minDist, maxDist));
    }
    
    public void CalculaFuerzaRepulsion(Rigidbody rb, float fm)
    {
        Vector3 direccion = new Vector3(rb.position.x - _rb.position.x, rb.position.y - _rb.position.y,
            rb.position.z - _rb.position.z).normalized;
        Fuerza_Magnetica += -direccion * (fm / Mathf.Clamp(Vector3.Distance(_rb.position, rb.position), minDist, maxDist));
    }
    
}
