using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinsMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] movingPumkins;

    private float[] speed = { 1.5f, 1f, 1.75f };

    void Update()
    {
        MovingPumkins();

        //transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 5), transform.position.y, transform.position.z);
        }

    void MovingPumkins()
    {
        int count = 0;

        foreach (var item in movingPumkins)
        {
            item.transform.position = new Vector3(Mathf.PingPong(Time.time * speed[count], 4), item.transform.position.y, item.transform.position.z);
            count++;
        }
    }
}
