using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cruceta : MonoBehaviour
{
    [SerializeField] private Texture2D cruceta;
    [SerializeField] private Rect posicion;

    private void Start(){
        Screen.lockCursor = true;
        posicion = new Rect((Screen.width-50)/2,(Screen.height-50)/2,50, 50 );
    }

    private void OnGUI(){
        GUI.DrawTexture( posicion, cruceta);
    } 
}
