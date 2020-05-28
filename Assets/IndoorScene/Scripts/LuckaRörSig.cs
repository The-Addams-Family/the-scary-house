using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckaRörSig : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.PingPong(Time.time, 4), transform.localPosition.z);
    }
}
