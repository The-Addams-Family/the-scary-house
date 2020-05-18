using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float rotation_x;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float X_mouse = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float Y_mouse = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotation_x -= Y_mouse;
        rotation_x = Mathf.Clamp(rotation_x, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotation_x,0f,0f);
        playerBody.Rotate(Vector3.up * X_mouse);
    }
}
