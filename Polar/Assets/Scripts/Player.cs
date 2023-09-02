using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float vel;
    [SerializeField] private float Max_vel;
    [SerializeField] private float F_salto;
    private bool isGrounded;

    [Header("Mirar")] 
    [SerializeField] private float sensibilidad;
    [SerializeField] private float AnguloVertical;
    private Transform cam;
    
    private Rigidbody _rb;

    [SerializeField] private Bala BalaN;
    [SerializeField] private Bala BalaS;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        isGrounded = true;
        
        //InvokeRepeating("Print", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Mirar();
        Disparar();
    }

    private void FixedUpdate()
    {
        Movimiento();
        Salto();
    }

    private void Movimiento()
    {
        if (_rb.velocity.magnitude < Max_vel)
        {
            _rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * vel), ForceMode.Force);
            _rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * vel), ForceMode.Force);
        }
    }

    private void Mirar()
    {
        AnguloVertical += Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        AnguloVertical = Mathf.Clamp(AnguloVertical, -70, 70);
        
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(-AnguloVertical, 0f, 0f);
    }

    private void Salto()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            _rb.AddForce(transform.up *  F_salto, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Disparar()
    {
        if (Input.GetButtonDown("Fire1") && !BalaN.getShot()) //Disparamos N
        {
            BalaN.Disparar();
        }

        if (Input.GetButtonDown("Fire2") && !BalaS.getShot())
        {
            BalaS.Disparar();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.layer == 3) //3 es el layer del suelo
        {
            isGrounded = true;
        }
    }

    private void Print()
    {
        Debug.Log(_rb.velocity.y);
    }
}
