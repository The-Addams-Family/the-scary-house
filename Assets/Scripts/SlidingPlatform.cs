using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlidingPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
    
        if(other.CompareTag("Player")){
          
          other.gameObject.transform.parent = transform.parent.transform;
        }
    }
}
