using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveTowardsHouseAI : MonoBehaviour
{

    public Transform[] path;
    public float speed = 8;
    public Animator doorAnim;
    public Animator imageFade;
    public Animator playerEnter;


    public Image img;
   
   private void Start() {
       img.enabled = false;
       playerEnter.enabled = false;
       StartCoroutine(FollowPath());
   }

    IEnumerator MoveToPath(Vector3 destination,float speed){
        while(transform.position != destination){
            transform.position = Vector3.MoveTowards(transform.position, destination,speed *Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FollowPath(){
        foreach(Transform way in path){
            if(way.gameObject.CompareTag("GoRight")){
                speed = 4;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,45, transform.rotation.eulerAngles.z);
            }
            if(way.gameObject.CompareTag("GoToHouse")){
                speed = 2;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,45, transform.rotation.eulerAngles.z);
            }
            yield return StartCoroutine(MoveToPath(way.position,speed));
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("GoToHouse")){
            StartCoroutine(OpenDoor());
        }

    }

    IEnumerator OpenDoor(){
        yield return new WaitForSeconds(1.0f);
        doorAnim.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(3.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y+2f,transform.position.z);
        playerEnter.enabled = true;
        playerEnter.SetTrigger("Enter");
        yield return new WaitForSeconds(2.0f);
        img.enabled = true;
        imageFade.SetTrigger("FadeToGame");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }

}
