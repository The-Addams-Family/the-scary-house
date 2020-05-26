﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    [SerializeField] private float rotateRate;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpSpeed = 10.0f;

    [SerializeField] private float gravity = -9.82f;

    [SerializeField] private float cornerTurnAngle = 0f;

    [SerializeField] private float backAngle = 0f;
    [SerializeField] private float forwardAngle = 180f;

    private CharacterController _charController;

    private float verticalVelocity = 0f;

    private float verticalRotation = 90;

    private float hAxis;
    bool isWalking;
    float stepTimer;
    [SerializeField] float stepDuration = 0.3f;

    public float CornerTurnAngle {
        get { return cornerTurnAngle; }
         set { cornerTurnAngle = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
       _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        hAxis = Input.GetAxis("Horizontal"); // vänster/höger ska vara bak/fram 
     
        if (_charController.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                verticalVelocity = jumpSpeed;
            } else {
                verticalVelocity = 0f;
            }
        }

                verticalVelocity += gravity * Time.deltaTime;
        
        isWalking = Mathf.Abs(hAxis) != 0f;
         if(isWalking){
             stepTimer += Time.deltaTime;
             if(stepTimer > stepDuration){
                 stepTimer = 0f;
                 Audio_Manager.Instance.RequestSound(SOUNDTYPE.step);
             }
         }else{
             stepTimer =0f;
         }
        /*
         Move(hAxis);

         Turn(hAxis);
         */
    
        
    }

    private void FixedUpdate() {
        /*
        
         Move(hAxis);

         Turn(hAxis);
        */
    }

    private void LateUpdate() {
        if(isWalking){
            Move(hAxis);
         

            Turn(hAxis); 
        }
                
    }

    private void Move(float input) {
        
        float dt = Time.deltaTime;

       Quaternion rot = Quaternion.Euler(0f, cornerTurnAngle, 0f);
        Vector3 forwardDirection = rot * Vector3.forward * input * moveSpeed * dt + Vector3.up * verticalVelocity * dt;
        _charController.Move(forwardDirection);
        
        
    }

    private void Turn(float input) {
         // Detta ser att rotera karaktären 180 grader när man byter flyttriktningen
        if (input > 0.1f) {
            verticalRotation = backAngle + cornerTurnAngle;
        } else if (input < -0.1f) {
            verticalRotation = forwardAngle + cornerTurnAngle;
        }

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = Mathf.Lerp(rot.y, verticalRotation, 5f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rot);
    }
}