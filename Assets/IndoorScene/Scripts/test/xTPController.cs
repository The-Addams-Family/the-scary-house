using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
// Håller reda på vilken riktning man har, det är antingen vänster eller höger
public enum PlayerDirection
{
    Right,
    Left
}
*/
public class xTPController : MonoBehaviour
{

    [SerializeField] private float rotateRate;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpSpeed = 10.0f;

    [SerializeField] private float gravity = -9.82f;

    [SerializeField] private float backAngle = 0f;
    [SerializeField] private float forwardAngle = 180f;

    private CharacterController _charController;

    private float _verticalVelocity = 0f;

    private float _verticalRotation = 90;

    private float _hAxis;

    private float _cornerTurnAngle = 90f;

    private PlayerDirection _direction;

    /// <summary>
    /// Angle to orientate Vector3.forard around the Y axis to point it towards the direction of walking.
    /// </summary>
    public float WalkAngle
    {
        get { return Direction == PlayerDirection.Right ? _cornerTurnAngle : _cornerTurnAngle - 180; }
    }


    public float CornerTurnAngle {
        get { return _cornerTurnAngle; }
         set { _cornerTurnAngle = value; }
    }

    public PlayerDirection Direction {
        get { return _direction; }
    }

    // Start is called before the first frame update
    void Start()
    {
       _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        _hAxis = Input.GetAxis("Horizontal"); // vänster/höger ska vara bak/fram 
     
        if (_charController.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _verticalVelocity = jumpSpeed;
            } else {
                _verticalVelocity = 0f;
            }
        }

                _verticalVelocity += gravity * Time.deltaTime;
        
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

         Move(_hAxis);

         Turn(_hAxis);        
    }

    private void Move(float input) {
        
        float dt = Time.deltaTime;

       Quaternion rot = Quaternion.Euler(0f, _cornerTurnAngle, 0f);
        Vector3 forwardDirection = rot * Vector3.forward * input * moveSpeed * dt + Vector3.up * _verticalVelocity * dt;
        _charController.Move(forwardDirection);
        
        
    }

    private void Turn(float input) {
         // Detta ser att rotera karaktären 180 grader när man byter flyttriktningen
        if (input > 0.1f) {
            _verticalRotation = backAngle + _cornerTurnAngle;
            _direction = PlayerDirection.Left;
        } else if (input < -0.1f) {
            _verticalRotation = forwardAngle + _cornerTurnAngle;
             _direction = PlayerDirection.Right;
        }

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = Mathf.Lerp(rot.y, _verticalRotation, 5f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rot);
    }
}
