using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("BackGround Music")]
    public AudioClip bgClip;
    AudioSource musicBackground;  

    [Header("Puzzle Button Music")]
    // Classic Game Music
    public AudioClip puzzleButtonClip;
    public AudioClip notificaFalseClip;
    public AudioClip notificaTrueClip;
    public AudioClip bgMusicClassGameClip;
    public AudioClip StartLervelClip;
    public AudioClip RandomMapClip;
    public AudioClip LoseGameClassicClip;
    
    // Extend Game Music
    public AudioClip bgSelectLevelClip;
    public AudioClip bgMusicExGameCLip;
    public AudioClip ExSelectClip;
    public AudioClip ExNotitrueCLip;
    public AudioClip ExNotiFalseClip;
    public AudioClip ExRandomClip;
    public AudioClip ExStartLevelClip;
    public AudioClip ExWinClip;

    public AudioSource puzzleButtonSound;
    public AudioSource notificaSound;

    [Header("Button Sound")]
    public AudioClip ButtonClip;
    public AudioSource ButtonSound;

    [Header("Mixer Groups")]
    public AudioMixerGroup backGroundMixerGround;
    public AudioMixerGroup buttonGround;
    public AudioMixerGroup gamePlay;

    void Awake()
    {
        musicBackground = gameObject.AddComponent<AudioSource>();     
        ButtonSound = gameObject.AddComponent<AudioSource>();
        puzzleButtonSound = gameObject.AddComponent<AudioSource>();
        notificaSound = gameObject.AddComponent<AudioSource>();

        musicBackground.outputAudioMixerGroup = backGroundMixerGround;       
        ButtonSound.outputAudioMixerGroup = buttonGround;
        puzzleButtonSound.outputAudioMixerGroup = gamePlay;
        notificaSound.outputAudioMixerGroup = gamePlay;

        // Start Audio game
        PlayAudioGame();
    }
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame   
    public void PlayAudioGame() {
        
        musicBackground.clip = bgClip; 
        musicBackground.loop = true;
        musicBackground.Play();

        ButtonSound.clip = ButtonClip;
        ButtonSound.loop = false;

        puzzleButtonSound.clip = puzzleButtonClip;
        puzzleButtonSound.loop = false;
        
        notificaSound.loop = false;

    }
    public void MuteSoundButton(bool flag) {
        ButtonSound.mute = flag;
    }
    public void MuteMusicBackGround(bool flag)
    {
        musicBackground.mute = flag;        
    }
    public void MutePuzzleButtonSound (bool flag)
    {
        puzzleButtonSound.mute = flag;
        notificaSound.mute = flag;
    }
    public void SetMusicForClassicGame() {        
        musicBackground.clip = bgMusicClassGameClip;
        musicBackground.loop = true;
        musicBackground.Play();

    }
    public void SetMusicSelectLevelGame()
    {
        musicBackground.clip = bgSelectLevelClip;
        musicBackground.loop = true;
        musicBackground.Play();

    }
    public void LoseClassicMusic() {
        musicBackground.clip = LoseGameClassicClip;
        musicBackground.loop = true;
        musicBackground.Play();
    }
    public void SetMusicExGame()
    {
        musicBackground.clip = bgMusicExGameCLip;
        musicBackground.loop = true;
        musicBackground.Play();
    }
}
