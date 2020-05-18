using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreditationAnim : MonoBehaviour
{
    public event Action  OnAnimateTextOver;
    IIntroSequence introText;
    public Text credText;
    public Text credTextNames;
    Animator _animator;
    
    
    bool _timeToPlay;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        credText.enabled = false;
        credTextNames.enabled = false;
        
        introText = FindObjectOfType<IIntroSequence>();
        introText.onAnimateFinished += SetPlayOver;
        
    }

    private void Update() {
        if(_timeToPlay){
            StartCoroutine(AnimateText());
        }
    }


    IEnumerator AnimateText(){

        credText.enabled = true;
        credTextNames.enabled = true;
        _animator.SetTrigger("playAnim");
        yield return new WaitForSeconds(8);
        _animator.SetTrigger("playFadeOut");
        yield return new WaitForSeconds(3);
        if(OnAnimateTextOver != null){
            
            OnAnimateTextOver();
        }
      
    }

    void SetPlayOver(){
        _timeToPlay = true;
    }
}
