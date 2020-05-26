using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLive : MonoBehaviour
{
    int maxLife = 3;
    int playerLife;
    public Text deathText;
    public HealthBar bar;
    Animator deathTextAnim;

    public Transform initialTransform;

    CharacterController controller;
    bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        deathTextAnim = deathText.GetComponent<Animator>();
        deathTextAnim.SetBool("AnimateText", false);
        playerLife = maxLife;
        bar.SetMaxHealth(maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEmpty){
            StartCoroutine(AnimateDeathText());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ghost")){
            Audio_Manager.Instance.RequestSound(SOUNDTYPE.loselife);
            OnDamage();
        }
    }

    public void OnDamage(){
        playerLife -= 1;
        bar.SetHealth(playerLife);
        if(playerLife <= 0)
            isEmpty = true;
    }

    public IEnumerator AnimateDeathText(){
        deathTextAnim.SetBool("AnimateText", true);
        Audio_Manager.Instance.RequestSound(SOUNDTYPE.death);
        yield return new WaitForSeconds(2.2f);
        OnPlayerDeath();
        isEmpty = false;
        
    }

    void OnPlayerDeath(){
        controller.enabled = false;
        transform.position = initialTransform.position;
        deathTextAnim.SetBool("AnimateText", false);
        playerLife = maxLife;
        bar.SetMaxHealth(maxLife);
        controller.enabled = true;
    }
}
