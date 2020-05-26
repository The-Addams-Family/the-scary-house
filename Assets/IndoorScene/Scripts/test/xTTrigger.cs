﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xTTrigger : MonoBehaviour
{
    [SerializeField] private xTPController character;
    [SerializeField] private float enterAngle = 0.0000f;
    [SerializeField] private float exitAngle = 180.0000f;

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

            // Detta beräknar skalärprodukten och avläser om vi passerat origo av triggerobjektet, 
            // isåfall är ska rotera karaktären.
            if (Vector3.Dot(v, w) > 0.01f) {

                //PrintVector("w -EnterDirection", -EnterDirection);
                //PrintVector("w -ExitDirection",  -ExitDirection);

                Debug.Log("hit");

                if (Vector3.Dot(w, -EnterDirection) > 0.01f) {
                    Debug.Log("ENTER");
                } else if (Vector3.Dot(w, -ExitDirection) > 0.01f){
                    Debug.Log("EXIT");
                }
            } else {
                //Debug.Log("nop");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        
    }

    private void PrintVector(string m, Vector3 v) {
        Debug.Log(m + " : " + JsonUtility.ToJson(v));
    }
}