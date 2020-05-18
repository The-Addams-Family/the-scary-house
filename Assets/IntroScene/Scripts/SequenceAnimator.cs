using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour
{
    List<Animator> _animators;
    void Start()
    {
        _animators = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText(){
        while(true){
            foreach(Animator anim in _animators){
                anim.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
