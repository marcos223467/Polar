using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Referencias")] 
    [SerializeField] private Camera cam;
    
    [Header("Movimiento")]
    [SerializeField] private float vel; //Andar
    [SerializeField] private float Max_vel; //Sprint
    [SerializeField] private float f_salto;
    [SerializeField] private float gravityScale;

    [Header("Rotacion")] 
    [SerializeField] private float sensibilidad;

    private float VerticalAngle = 45f;
    
    private Vector3 moveInput;
    private Vector3 rotationInput;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        moveInput = Vector3.zero; 
        rotationInput = Vector3.zero;
    }

    private void Update()
    {
        Movimiento();
        Look();
    }

    private void Movimiento()
    {
        if (_characterController.isGrounded)
        {
            //Movimiento
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            
            if (Input.GetButton("Sprint"))
                moveInput = transform.TransformDirection(moveInput) * Max_vel;
            else
                moveInput = transform.TransformDirection(moveInput) * vel;
        
            //Salto
            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(f_salto * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        
        
        _characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        VerticalAngle += rotationInput.y;
        VerticalAngle = Mathf.Clamp(VerticalAngle, -70, 70);
        
        transform.Rotate(Vector3.up * rotationInput.x);
        cam.transform.localRotation = Quaternion.Euler(-VerticalAngle, 0f, 0f);
    }
}
