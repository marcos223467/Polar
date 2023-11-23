using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    private float vel;
    [SerializeField] private float Min_vel;
    [SerializeField] private float Max_vel;
    [SerializeField] private float F_salto;
    private bool isGrounded;
    private bool sprint;
    private bool crouch;

    [Header("Mirar")] 
    [SerializeField] private float sensibilidad;
    [SerializeField] private float AnguloVertical;
    private Transform cam;
    
    private Rigidbody _rb;
    
    [Header("Shooter")]
    [SerializeField] private Bala BalaN;
    [SerializeField] private Bala BalaS;
    [SerializeField] private float anguloTiro;
    [SerializeField] private GameObject L, R;
    public bool pistolas;

    public GameObject p1, p2;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").transform;
        isGrounded = true;
        sprint = false;
        crouch = false;
        pistolas = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mirar();
        if (pistolas)
        {
            Disparar();
        }
    }

    private void FixedUpdate()
    {
        Movimiento();
        Salto();
    }

    private void Movimiento()
    {
        if (crouch)
        {
            if ( _rb.velocity.magnitude < Min_vel/2)
            {
                _rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * Min_vel), ForceMode.Force);
                _rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * Min_vel), ForceMode.Force);
            }
        }
        else
        {
            if (Input.GetButton("Sprint"))
            {
                sprint = true;
                vel = Max_vel;
            }
            else
            {
                sprint = false;
                vel = Min_vel;
            }
            if (sprint && _rb.velocity.magnitude < Max_vel)
            {
                _rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * vel), ForceMode.Force);
                _rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * vel), ForceMode.Force);
            }

            if (!sprint && _rb.velocity.magnitude < Min_vel)
            {
                _rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * vel), ForceMode.Force);
                _rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * vel), ForceMode.Force);
            }
        }
        
    }

    private void Mirar()
    {
        AnguloVertical += Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        AnguloVertical = Mathf.Clamp(AnguloVertical, -70, 70);
        
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(-AnguloVertical, 0f, 0f);
        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = !crouch;
        }
    }

    private void Salto()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            _rb.AddForce(transform.up *  F_salto, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    private void Disparar()
    {
        if (Input.GetButtonDown("Fire1") && !BalaN.getShot()) //Disparamos N
        {
            BalaN.Disparar(anguloTiro);
        }

        if (Input.GetButtonDown("Fire2") && !BalaS.getShot())
        {
            BalaS.Disparar(-anguloTiro);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.layer == 3 || collision.gameObject.layer == 6) //3 es el layer del suelo y 6 de interactables
        {
            isGrounded = true;
        }
    }

    private void Print()
    {
        Debug.Log(_rb.velocity.y);
    }

    public void ActivaPistolas()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            L.SetActive(true);
            R.SetActive(true);
            pistolas = true;
            p1.SetActive(false);
            p2.SetActive(false);
        }
        
    }
}
