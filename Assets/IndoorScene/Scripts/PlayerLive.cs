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

    public GameObject levelCompleteUI;

    public Transform initialTransform;

    CharacterController controller;
    
    ThirdPersonController playerThird;

   
    bool isEmpty;
    bool isturn;
   
    // Start is called before the first frame update
    void Start()
    {
        playerThird = GetComponent<ThirdPersonController>();
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
        playerThird.CornerTurnAngle = 90f;
        deathTextAnim.SetBool("AnimateText", false);
        playerLife = maxLife;
        bar.SetMaxHealth(maxLife);
        controller.enabled = true;
    }

    public void LevelCompleted(){
        
        Audio_Manager.Instance.RequestMusic(MUSICTYPE.LevelWin,true);
        Audio_Manager.Instance.MusicLoop(false);
        levelCompleteUI.SetActive(true);
        StartCoroutine(PlayBackgroundMusic());
    }

    IEnumerator PlayBackgroundMusic(){
        yield return new WaitForSeconds(3.2f);
        Audio_Manager.Instance.RequestMusic(MUSICTYPE.AmbienceMenu,true);
        Audio_Manager.Instance.MusicLoop(true);
    }
}
