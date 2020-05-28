using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeTrigger : MonoBehaviour
{
 
    [SerializeField] PlayerLive player;

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Audio_Manager.Instance.RequestSound(SOUNDTYPE.loselife);
            player.OnDamage();
        }
    }
}