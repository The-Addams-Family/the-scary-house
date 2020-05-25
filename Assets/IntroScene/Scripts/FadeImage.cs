using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeImage : MonoBehaviour
{
    CreditationAnim creditation;
    Animator _animator;
    public Image img;
    
    bool toPlay;
    // Start is called before the first frame update
    void Start()
    {
        
       _animator = GetComponent<Animator>(); 
       creditation = FindObjectOfType<CreditationAnim>();
       creditation.OnAnimateTextOver += SetPlayOver;
       toPlay = false;
    }

   private void Update() {
        if(toPlay){
            StartCoroutine(Fadeimage());
        }
    }
    IEnumerator Fadeimage(){    
        _animator.SetTrigger("FadeOut");
        
        yield return new WaitUntil(()=>img.color.a==1); 
        SceneManager.LoadScene(1);
        toPlay = false;
        
    }
    void SetPlayOver(){
        toPlay = true;
    }
}
