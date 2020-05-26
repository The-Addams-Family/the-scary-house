using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private PlayerLive playerLive;
    


    private void Start()
    {
        playerLive = player.gameObject.GetComponent<PlayerLive>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(playerLive.AnimateDeathText());
        }
        
    }
}
