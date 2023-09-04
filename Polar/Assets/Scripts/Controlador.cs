using System;
using System.Collections;
using System.Collections.Generic;
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

    public int AddNorte(GameObject inter)
    {
        Norte.Add(inter);
        Atraer();
        Repeler();
        return Norte.IndexOf(inter);
    }
    
    public int AddSur(GameObject inter)
    {
        Sur.Add(inter);
        Atraer();
        Repeler();
        return Sur.IndexOf(inter);
    }

    public void RemoveNorte(int index)
    {
        Norte.RemoveAt(index);
    }
    
    public void RemoveSur(int index)
    {
        Sur.RemoveAt(index);
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
            }
        }
    }

    private void Repeler()
    {
        if (Norte.Count <= 1)
        {
            return;
        }
        if (Norte.Count > 1)
        {
            foreach (var N in Norte)
            {
                foreach (var N2 in Norte)
                {
                    if (N != N2)
                        N.GetComponent<Interactable>().CalculaFuerzaRepulsion(N2.GetComponent<Rigidbody>(), fm);
                }
            }
        }
        
        if (Sur.Count <= 1)
        {
            return;
        }

        if (Sur.Count > 1)
        {
            foreach (var S in Sur)
            {
                foreach (var S2 in Sur)
                {
                    if (S != S2)
                        S.GetComponent<Interactable>().CalculaFuerzaRepulsion(S2.GetComponent<Rigidbody>(), fm);
                }
            }
        }
    }
}
