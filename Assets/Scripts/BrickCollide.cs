using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollide : MonoBehaviour
{
    Rigidbody rb;
    PlayerLive player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLive>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals("Player")){
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name.Equals("Player")){
            Audio_Manager.Instance.RequestSound(SOUNDTYPE.loselife);
            player.onDamage();    
        }
        StartCoroutine(DestroyBrick());
    }

    IEnumerator DestroyBrick(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
