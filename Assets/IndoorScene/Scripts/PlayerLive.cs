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

    bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        deathTextAnim = deathText.GetComponent<Animator>();
        deathTextAnim.SetBool("AnimateText", false);
        playerLife = maxLife;
        bar.SetMaxHealth(maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEmpty){
            Debug.Log("Run this line");
            StartCoroutine(AnimateDeathText());
            Audio_Manager.Instance.RequestSound(SOUNDTYPE.death);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ghost")){
            Audio_Manager.Instance.RequestSound(SOUNDTYPE.loselife);
            onDamage();
        }
    }

    public void onDamage(){
        playerLife -= 1;
        bar.SetHealth(playerLife);
        if(playerLife <= 0)
            isEmpty = true;
    }

    IEnumerator AnimateDeathText(){
        deathTextAnim.SetBool("AnimateText", true);
        yield return new WaitForSeconds(1f);
        isEmpty = false;
    }
}
