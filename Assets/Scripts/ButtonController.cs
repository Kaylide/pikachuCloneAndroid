using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject Page1On, Page2On, Page3On, Page1Off, Page2Off, Page3Off, CanvasLevel1, CanvasLevel2, CanvasLevel3;
    private int i = 1;
    public GameObject pauseCanvas;
    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();    
        // PlayerPrefs.DeleteAll();
    }
    public void NextScene() {
        audioManager.ButtonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void HomeScene()
    {
        audioManager.ButtonSound.Play();
        SceneManager.LoadScene(1);
    }
    public void BackScene()
    {
        audioManager.ButtonSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void SetMusicBgSelectLevelGame() {
        audioManager.ButtonSound.Play();
        audioManager.SetMusicSelectLevelGame();
        SceneManager.LoadScene(3);
    }
    public void SetMusicDefault() {
        audioManager.PlayAudioGame();
    }
    public void PlayClassicGame()
    {
        PlayerPrefs.DeleteKey("LevelCurrent");
        PlayerPrefs.DeleteKey("CurrentScore");
    }
    public void Paused()
    {
        audioManager.ButtonSound.Play();
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }
    public void Restart()
    {
        audioManager.ButtonSound.Play();       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {
        audioManager.ButtonSound.Play();
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
    public void NextLevelExd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.ButtonSound.Play();
        Time.timeScale = 1;
        PlayerPrefs.SetInt("ExGameLevel",PlayerPrefs.GetInt("ExGameLevel")+1);
    }
    public void MuteMusic(bool flag) {
        audioManager.MuteMusicBackGround(flag);
    }
    public void MuteSound(bool flag) {      
        audioManager.MuteSoundButton(flag);
    }
    public void MutePuzzelSound(bool flag)
    {
        audioManager.MutePuzzleButtonSound(flag);
    }
    private void SlidePage(bool page1,bool page2, bool page3) {
       switch (page1) { 
           case true:
                Page1On.SetActive(true);
                Page1Off.SetActive(false);
                CanvasLevel1.SetActive(true);
                break;
           case false:
                Page1On.SetActive(false);
                Page1Off.SetActive(true);
                CanvasLevel1.SetActive(false);
                break;
       }
       switch (page2)
       {
           case true:
               Page2On.SetActive(true);
               Page2Off.SetActive(false);
               CanvasLevel2.SetActive(true);
               break;
           case false:
               Page2On.SetActive(false);
               Page2Off.SetActive(true);
               CanvasLevel2.SetActive(false);
               break;
       }
       switch (page3)
       {
           case true:
               Page3On.SetActive(true);
               Page3Off.SetActive(false);
               CanvasLevel3.SetActive(true);
               break;
           case false:
               Page3On.SetActive(false);
               Page3Off.SetActive(true);
               CanvasLevel3.SetActive(false);
               break;
       }
   }
   public void NextPage() {
        audioManager.ButtonSound.Play();
        switch (i) { 
           case 1:
               SlidePage(false, true, false);
               i++;
               break;
           case 2:
               SlidePage(false, false, true);
               i++;
               break;
       }
   }
   public void PrewPage()
   {
        audioManager.ButtonSound.Play();
        switch (i)
       {
           case 2:
               SlidePage(true, false, false);
               i--;
               break;
           case 3:
               SlidePage(false, true, false);
               i--;
               break;
       }
   }
    public void ButtonSound() {
        audioManager.ButtonSound.Play();
    }
}
