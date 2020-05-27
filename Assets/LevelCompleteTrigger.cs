using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    public PlayerLive playerLife;

    private void OnTriggerEnter(Collider other) {
        playerLife.LevelCompleted();
    }
}
