using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class IIntroSequence : MonoBehaviour
{
    Animation textAnim;
    static bool playOver;
    public event Action onAnimateFinished;
    void Start()
    { 
        textAnim = GetComponent<Animation>();
        StartCoroutine(AnimatingSequence());  
    }

    IEnumerator AnimatingSequence(){
        yield return new WaitForSeconds(6.0f);
        textAnim.Play();
        yield return new WaitForSeconds(4.0f);
        if(onAnimateFinished != null)
            onAnimateFinished();
    }

   
    
}
