using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] private GameObject ghostObject;
    [SerializeField] private float waitSeconds = 0.5f;
    [SerializeField] private float fadeSteps = 0.05f;


    private MeshRenderer _meshRenderer;
    private Material _material;

    private float _opacity; // synligheten för spöket

    private IEnumerator _fadeIn;
    private IEnumerator _fadeOut;

    private void Start() {
        _meshRenderer = ghostObject.GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        Color c = _material.color;
        c.a = 0;
        _material.color = c;

        _fadeIn = FadeIn();
        _fadeOut = FadeOut();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (_fadeOut != null)
                StopCoroutine(_fadeOut);
            
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            if (_fadeIn != null)
                StopCoroutine(_fadeIn);
            StartCoroutine(FadeOut());
        }        
    }

    IEnumerator FadeIn() {
        Color c = _material.color;
        for (float f = 0f; f <= 1.0f; f += fadeSteps) {
            
            c.a = f;
            _material.color = c;
            Debug.Log(f);
            yield return new WaitForSeconds(waitSeconds);
        }
       
    }

    IEnumerator FadeOut() {
         Color c = _material.color;
        for (float f = 1.0f; f > 0.0f; f -= 0.1f) {
           
            c.a = f;
            _material.color = c;
            Debug.Log(f);
            yield return new WaitForSeconds(waitSeconds);
        }

            c.a = 0.0f;
            _material.color = c;        
       
    }    
}
