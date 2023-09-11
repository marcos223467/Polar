using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Deteccion")]
    [SerializeField] private Controlador _controlador;
    private float minDist, distCuant;
    [SerializeField] private float maxDist;
    
    private Rigidbody _rb;
    private MeshRenderer _meshRenderer;
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    public bool skinned;

    [SerializeField] private Material[] _materials;
    
    private string polaridad;

    private Vector3 Fuerza_Magnetica;
    [SerializeField] private float fm;

    public bool estatico;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        polaridad = "";
        if (!skinned)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = _materials[0];
        }
        else
        {
            _skinnedMesh.material = _materials[0];
        }
        Fuerza_Magnetica = Vector3.zero;
        minDist = 0.1f;
        distCuant = 1000;
    }

    void FixedUpdate()
    {
        if (!estatico)
            ApplyForces();
    }

    public void setPolaridad(string pol)
    {
        polaridad = polaridad == pol ? polaridad = "" : polaridad = pol;
        
        switch (polaridad)
        {
            case "":
                if (skinned)
                    _skinnedMesh.material = _materials[0];
                else
                    _meshRenderer.material = _materials[0];
                
                _controlador.RemoveSur(gameObject);
                _controlador.RemoveNorte(gameObject);
                break;
            case "Norte":
                if (skinned)
                    _skinnedMesh.material = _materials[1];
                else
                    _meshRenderer.material = _materials[1];
                
                _controlador.RemoveSur(gameObject);
                _controlador.AddNorte(gameObject);
                break;
            case "Sur":
                if (skinned)
                    _skinnedMesh.material = _materials[2];
                else
                    _meshRenderer.material = _materials[2];
                
                _controlador.RemoveNorte(gameObject);
                _controlador.AddSur(gameObject);
                break;
            default:
                if (skinned)
                    _skinnedMesh.material = _materials[0];
                else
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

    public void CalculaFuerzaAtraccion(Rigidbody rb)
    {
        if (Vector3.Distance(_rb.position, rb.position) <= maxDist)
        {
            Vector3 direccion = new Vector3(rb.position.x - _rb.position.x, rb.position.y - _rb.position.y,
                rb.position.z - _rb.position.z).normalized;
            Fuerza_Magnetica += direccion * (fm / Mathf.Clamp(Vector3.Distance(_rb.position, rb.position), minDist, distCuant));
        }
    }
    
    public void CalculaFuerzaRepulsion(Rigidbody rb)
    {
        if (Vector3.Distance(_rb.position, rb.position) <= maxDist)
        {
            Vector3 direccion = new Vector3(rb.position.x - _rb.position.x, rb.position.y - _rb.position.y,
                rb.position.z - _rb.position.z).normalized;
            Fuerza_Magnetica += -direccion * (fm / Mathf.Clamp(Vector3.Distance(_rb.position, rb.position), minDist, distCuant));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // Layer del despolarizador
        {
            setPolaridad("");
        }
    }
}
