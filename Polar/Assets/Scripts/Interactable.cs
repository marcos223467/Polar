using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Deteccion")] 
    [SerializeField] private float RadioDetector;
    [SerializeField] private LayerMask interactable;
    
    private Rigidbody _rb;

    [SerializeField] private float f_atraccion, f_repelente;

    private string polaridad;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        polaridad = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Accion();
    }

    public void setPolaridad(string pol)
    {
        polaridad = polaridad == pol ? polaridad = "" : polaridad = pol;
    }

    private void Accion()
    {
        if (polaridad == "")
        {
            return;
        }
        
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, RadioDetector, transform.forward, out hit, 0, interactable))
        {
            if (hit.collider.gameObject.GetComponent<Interactable>().getPolaridad() == "")
            {
                return;
            }

            if (polaridad == hit.collider.gameObject.GetComponent<Interactable>().getPolaridad())
            {
                Repeler(hit.collider.gameObject.GetComponent<Rigidbody>());
            }
            else
            {
                Atraer(hit.collider.gameObject.GetComponent<Rigidbody>());
            }
        }

        
    }

    private void Atraer(Rigidbody objeto2)
    {
        Vector3 direccion = new Vector3(objeto2.position.x - _rb.position.x, objeto2.position.y - _rb.position.y,
            objeto2.position.z - _rb.position.z).normalized;
        if (_rb.mass == objeto2.mass)
        {
            _rb.AddForce(direccion * f_atraccion, ForceMode.Force);
        }
        //Hacer los otros casos
    }

    private void Repeler(Rigidbody objeto2)
    {
        
    }

    private string getPolaridad()
    {
        return polaridad;
    }
}
