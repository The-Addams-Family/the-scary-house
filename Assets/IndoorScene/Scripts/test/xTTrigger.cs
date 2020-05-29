﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xTTrigger : MonoBehaviour
{
    [SerializeField] private ThirdPersonController character;
    [SerializeField] private float enterAngle = -90.0f; // 0.0000f;
    [SerializeField] private float exitAngle = 180.0f; // 180.0000f;

    [SerializeField] private float angleA = 0.0000f;
    [SerializeField] private float angleB = 90.000f; // 270.0000f;    

    [SerializeField] private float rangeTolerance = 0.09f;    


    public float EnterAngle
    {
        get { return enterAngle + transform.eulerAngles.y; }
    }

    public float ExitAngle
    {
        get { return exitAngle + transform.eulerAngles.y; }
    }    

    private void Start() {
        PrintVector("EnterDirection", EnterDirection);
        PrintVector("ExitDirection", ExitDirection);

    }

    // Beräkna även enter-vektorn
    public Vector3 EnterDirection
    {
        get { return Quaternion.AngleAxis(EnterAngle, Vector3.up) * Vector3.forward; }
    }

    public Vector3 ExitDirection
    {
        get { return Quaternion.AngleAxis(ExitAngle, Vector3.up) * Vector3.forward; }
    }

    /*
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            character.CornerTurnAngle = enterAngle;
        }
    }
    */

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {

            // Dessa två beräkningar i kombination med skalärprodukten nedan
            // beräknar "mittpunkten" på triggern, så karaktären vet exakt när den ska rotera
            // vilket gör att vi kan använda triggern som en fungerande turn-character system
            // så vi kan gå runt hörn etc. 
            Vector3 v = (character.transform.position - transform.position).normalized;
            Vector3 w = Quaternion.AngleAxis(character.WalkAngle, Vector3.up) * Vector3.forward;

            DrawArrow.ForDebug(transform.position, v, Color.green);
            DrawArrow.ForDebug(character.transform.position, w, Color.red);
            // DrawArrow.ForGizmo(transform.position, )
            float df = Vector3.Dot(v, w);

            // Detta beräknar skalärprodukten och avläser om vi passerat origo av triggerobjektet, 
            // isåfall är ska rotera karaktären.
            if (df > 0.01f) {

                if (Vector3.Dot(w, -EnterDirection) > 0.01f) {

                    if (character.Direction == PlayerDirection.Right) {
                        character.CornerTurnAngle = ExitAngle;
                    } else {
                        character.CornerTurnAngle = ExitAngle + 180.0f;
                    }
                    
                   // Debug.Log("ENTER : " + Vector3.Dot(w, -EnterDirection));
                } else if (Vector3.Dot(w, -ExitDirection) > 0.01f){
                    if (character.Direction == PlayerDirection.Right) {
                        character.CornerTurnAngle = EnterAngle;
                    } else {
                        character.CornerTurnAngle = EnterAngle + 180.0f;
                    }                    
                }
            } else {
                Debug.Log("BBBB");
                if (Vector3.Dot(w, -EnterDirection) > 0.01f) {

                    if (character.Direction == PlayerDirection.Right) {
                        character.CornerTurnAngle = ExitAngle;
                    } else {
                        character.CornerTurnAngle = ExitAngle + 180.0f;
                    }
                    
                   // Debug.Log("ENTER : " + Vector3.Dot(w, -EnterDirection));
                } else if (Vector3.Dot(w, -ExitDirection) > 0.01f){
                    if (character.Direction == PlayerDirection.Right) {
                        character.CornerTurnAngle = EnterAngle;
                    } else {
                        character.CornerTurnAngle = EnterAngle + 180.0f;
                    }                    
                }

            }
        }
    }

    private void OnTriggerExit(Collider other) {
        
    }

    private void PrintVector(string m, Vector3 v) {
        Debug.Log(m + " : " + JsonUtility.ToJson(v));
    }
}
