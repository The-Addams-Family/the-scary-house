using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MUSICTYPE{
    none = -1,
    NightAmbience ,
    Thunder,
    Laugh,
    IndoorAmbience
}

public enum SOUNDTYPE{
    step = 0,
    loselife, 
    death,
    running
}
public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance {get; private set;}
    public AudioSource[] myAudioSource;
    [SerializeField] AudioClip[] clips = default;
        [SerializeField] private AudioSource myAudioSound;

    [System.Serializable]
    private class SoundGroup{
        [SerializeField] private string name;
        public AudioClip[] sounds = default;
        public float[] volume = default;
    }
    [SerializeField] private SoundGroup[] soundGroups;

    [SerializeField] float musicFadeDuration = 1f;
    [SerializeField] AnimationCurve curveMusic = default;
    
    Coroutine musicFade;
    int musicIndex;
    int previousMusicIndex;
    MUSICTYPE currentMusicType = MUSICTYPE.none;
    private void Awake() {
        if(Instance == null){  // first call
            Instance = this;
            myAudioSource = new AudioSource[2];
            for(int i = 0; i < myAudioSource.Length; i++){
                myAudioSource[i] = transform.Find("My_AudioSource_"+i.ToString()).GetComponent<AudioSource>();
            }
            myAudioSound = transform.Find("My_AudioSound").GetComponent<AudioSource>();
            
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void RequestSound(SOUNDTYPE sound){
        myAudioSound.PlayOneShot(
            soundGroups[(int)sound].sounds[Random.Range(0, soundGroups[(int)sound].sounds.Length)],
        Random.Range(soundGroups[(int)sound].volume[0], soundGroups[(int)sound].volume[1]));
    }

    public void SoundLoop(bool state){
        if (state){
            myAudioSound.loop = true;
        }else{
            myAudioSound.loop = false;
        }
    }

    

    public void RequestMusic(MUSICTYPE music, bool withFade){
        if(currentMusicType != music){
            currentMusicType = music;
            previousMusicIndex = musicIndex;
            musicIndex = Mathf.Abs(musicIndex - 1);
            if(currentMusicType == MUSICTYPE.none){
                myAudioSource[musicIndex].clip = null;
                if(!withFade){
                    for(int i = 0; i < myAudioSource.Length; i++){
                        myAudioSource[i].enabled = false;
                        myAudioSource[i].volume = 0f;
                        myAudioSource[i].Stop();
                    }
                    
                }else
                {
                    StartMusicFade();
        
                } 
            }else{
                
                myAudioSource[musicIndex].clip = clips[(int)music];
                if(!withFade){
                    myAudioSource[musicIndex].enabled = true;
                    myAudioSource[musicIndex].volume = 1f;
                    myAudioSource[musicIndex].Play();
                    myAudioSource[previousMusicIndex].enabled = false;
                    myAudioSource[previousMusicIndex].volume = 0f;
                    myAudioSource[previousMusicIndex].Stop();
                }else
                {
                    StartMusicFade();
        
                }  
            }
              
        }

    }
    void StartMusicFade(){
        StopMusicFade();
        musicFade = StartCoroutine(MusicFadeRoutine());
    }

    void StopMusicFade(){
        if(musicFade != null){
            StopCoroutine(musicFade);
        }
        musicFade = null;
    }

    IEnumerator MusicFadeRoutine(){
        if(currentMusicType != MUSICTYPE.none){
            myAudioSource[musicIndex].enabled = true;
            myAudioSource[musicIndex].Play();
        }
        
        float fadePercent = 0f;
        float[] startVolume = new float[2]{myAudioSource[previousMusicIndex].volume,
        myAudioSource[musicIndex].volume};
        float[] endVolume = new float[2] {0f, currentMusicType != MUSICTYPE.none ? 1f : 0f};
        while(fadePercent <= 1f){
            fadePercent += Time.fixedDeltaTime / musicFadeDuration;
            myAudioSource[previousMusicIndex].volume = Mathf.Lerp(startVolume[0],endVolume[0],
            curveMusic.Evaluate(fadePercent));
            myAudioSource[musicIndex].volume = Mathf.Lerp(startVolume[1],endVolume[1],
            curveMusic.Evaluate(fadePercent));
            yield return new WaitForFixedUpdate();
        }
        myAudioSource[previousMusicIndex].enabled = false;
        StopMusicFade();
    }

    public void MusicLoop(bool state){
        if (state){
            myAudioSource[musicIndex].loop = true;
        }else{
            myAudioSource[musicIndex].loop = false;
        }
    }
}
