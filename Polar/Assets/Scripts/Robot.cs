using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [Header("AI detection")]
    public bool detectado;
    public LayerMask layer_player, layer_obstaculo;
    public Transform vision;
    [Range(0f, 360f)]
    public float visionAngle;
    public float visionDistance;
    
    [Header("Gizmo Parameters")]
    public Color seek = Color.green;
    public Color find = Color.red;
    public bool mostrar = true;

    [SerializeField] private float vel;
    [SerializeField] private int vida;
    
    public Transform target;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        detectado = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        DetectaJugador();

    }
    
    public void DetectaJugador()
    {
        Vector3 dist = (target.position - vision.position);
        float _dist = dist.magnitude;
        if (Vector3.Angle(dist.normalized, vision.forward) < visionAngle / 2)
        {
            if (_dist < visionDistance)
            {
                RaycastHit hit;
                if (!Physics.Raycast(vision.position, target.position - vision.position, out hit, visionDistance, 9) && Physics.Raycast(vision.position, target.position - vision.position, out hit, visionDistance, 8))
                {
                    detectado = true;
                    Debug.DrawRay(vision.position, vision.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
                }
                else
                {
                    Debug.DrawRay(vision.position, vision.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
                    detectado = false;
                }
            }
            else
            {
                detectado = false;
            }
        }
        else
        {
            detectado = false;
        }
    }

    Vector3 PointForAngle(float ang, float dist)
    {
        return vision.TransformDirection(new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad))) * dist;
    }
    
    private void OnDrawGizmos()
    {
        if (mostrar)
        {
            Gizmos.color = seek;
            if (detectado)
                Gizmos.color = find;
            
            if (visionAngle >= 0f)
            {
                float halfVisionAngle = visionAngle / 2;
                Vector3 p1, p2;
                p1 = PointForAngle(halfVisionAngle, visionDistance);
                p2 = PointForAngle(-halfVisionAngle, visionDistance);

                Gizmos.DrawLine(vision.position, vision.position + p1);
                Gizmos.DrawLine(vision.position, vision.position + p2);
            }
        }
    }
}
