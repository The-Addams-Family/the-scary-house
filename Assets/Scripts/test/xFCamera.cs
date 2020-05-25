using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xFCamera : MonoBehaviour
{
    [SerializeField] private xTPController target;
   // [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed = 15f;
    [SerializeField] private FloatVariable zOffsetScriptableObject;

    [SerializeField] private float zOffsetInit;     // Startvärdet för zOffset

    [SerializeField] private bool isFollow = true;

    [SerializeField] private bool usingCornerSystem = true;

    [SerializeField] private float rotationDamping = 10f;

    private Vector3 _cameraOffset;
    
    void Start() {
        zOffsetScriptableObject.value = zOffsetInit;
    }


    // Update is called once per frame
    private void LateUpdate() {
        
        
        if (target) {

            // Ta ut karaktärens direction riktning och använd denna för att
            // rotera kamerans att följa med
            //Quaternion camTurnAngle = Quaternion.AngleAxis(target.CornerTurnAngle * cameraSpeed, Vector3.up);
            //_cameraOffset = Quaternion.AngleAxis(target.CornerTurnAngle, Vector3.up) * Vector3.forward;
            
            //Debug.Log(JsonUtility.ToJson(camTurnAngle));

            //_cameraOffset = camTurnAngle * (transform.position - target.transform.position);

            if (usingCornerSystem) {
                // Utför först rotationen med damping effekt (eftersläpning) sedan förflyttning
                Quaternion _rotationVector = Quaternion.Euler(0, target.CornerTurnAngle  - 90f, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, _rotationVector, Time.deltaTime * rotationDamping);
               
                Vector3 campos = target.transform.position + transform.forward * zOffsetScriptableObject.value; 
                transform.localPosition = Vector3.Lerp(transform.position, campos, cameraSpeed * Time.deltaTime);
            } else {

                // nedan beräknar positionen för kamerans förflyttning
                Vector3 newPos = transform.position; 
                
                newPos.x = target.transform.position.x; 

                // Beräknar avståndet mellan kameran och spelaren
                newPos.z = target.transform.position.z - zOffsetScriptableObject.value;
               
                if (!isFollow) {
                    transform.position = newPos;
                }
                else {
                    transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
                }
            }

        }
    }
}
