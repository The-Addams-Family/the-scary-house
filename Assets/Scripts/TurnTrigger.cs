using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    [SerializeField] private ThirdPersonController character;
    [SerializeField] private float exitAngle = 0f;
    [SerializeField] private float enterAngle = 0f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            character.CornerTurnAngle = enterAngle;
        }
    }

    private void OnTriggerExit(Collider other) {
        
    }
}
