using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private PlayerLive playerLive;
    private ThirdPersonController playerController;


    private void Start()
    {
        playerLive = player.gameObject.GetComponent<PlayerLive>();
        playerController = player.gameObject.GetComponent<ThirdPersonController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // reset also the direction for the player when he dies
            playerController.CornerTurnAngle = 90f;
            
            StartCoroutine(playerLive.AnimateDeathText());
        }
        
    }
}
