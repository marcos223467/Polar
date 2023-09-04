using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    private List<GameObject> Norte;
    private List<GameObject> Sur;

    [SerializeField] private float fm;
    void Start()
    {
        Norte = new List<GameObject>();
        Sur = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        Atraer();
        Repeler();
    }

    public void AddNorte(GameObject inter)
    {
        Norte.Add(inter);
    }
    
    public void AddSur(GameObject inter)
    {
        Sur.Add(inter);
    }

    public void RemoveNorte(GameObject obj)
    {
        if (Norte.Contains(obj))
            Norte.RemoveAt(Norte.IndexOf(obj));
    }
    
    public void RemoveSur(GameObject obj)
    {
        if (Sur.Contains(obj))
            Sur.RemoveAt(Sur.IndexOf(obj));
    }

    private void Atraer()
    {
        if (Norte.Count == 0 || Sur.Count == 0)
        {
            return;
        }
        

        foreach (var N in Norte)
        {
            foreach (var S in Sur)
            {
                N.GetComponent<Interactable>().CalculaFuerzaAtraccion(S.GetComponent<Rigidbody>(), fm);
                S.GetComponent<Interactable>().CalculaFuerzaAtraccion(N.GetComponent<Rigidbody>(), fm);
            }
        }
    }

    private void Repeler()
    {
        if (Norte.Count > 1)
        {
            foreach (var N in Norte)
            {
                foreach (var N2 in Norte)
                {
                    if (N != N2)
                    {
                        N.GetComponent<Interactable>().CalculaFuerzaRepulsion(N2.GetComponent<Rigidbody>(), fm);
                        N2.GetComponent<Interactable>().CalculaFuerzaRepulsion(N.GetComponent<Rigidbody>(), fm);
                    }
                }
            }
        }

        if (Sur.Count > 1)
        {
            foreach (var S in Sur)
            {
                foreach (var S2 in Sur)
                {
                    if (S != S2)
                    {
                        S.GetComponent<Interactable>().CalculaFuerzaRepulsion(S2.GetComponent<Rigidbody>(), fm);
                        S2.GetComponent<Interactable>().CalculaFuerzaRepulsion(S.GetComponent<Rigidbody>(), fm);
                    }
                        
                }
            }
        }
    }

    private void PrintInfo()
    {
        print("Norte: " + Norte.Count);
        print("Sur: " + Sur.Count);
    }
}
