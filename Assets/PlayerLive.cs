using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : MonoBehaviour
{
    int maxLife = 3;
    int playerLife;
    

    public HealthBar bar;
    // Start is called before the first frame update
    void Start()
    {
        playerLife = maxLife;
        bar.SetMaxHealth(maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ghost")){
            onDamage();
        }
    }

    void onDamage(){
        playerLife -= 1;
        bar.SetHealth(playerLife);
    }
}
