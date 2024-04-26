using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bandaSonora;

    public AudioClip fxButton;

    public AudioClip fxCoin;

    public AudioClip fxDead;

    public AudioClip fxFire;
    
    AudioSource _audioSource;

    public GameObject musicObj;

    AudioSource audioMusic;

    public static AudioManager Instance;

    void Awake(){

        if(Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        audioMusic = musicObj.GetComponent<AudioSource>();
        audioMusic = this.GetComponent<AudioSource>();
        audioMusic.clip = bandaSonora;
        audioMusic.loop = true;
        audioMusic.volume = 0.2f;
        audioMusic.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//metodo para hacer sonar clips de audio
    public void SonarClipUnaVez(AudioClip ac ){
        _audioSource.PlayOneShot(ac);
    }
}
