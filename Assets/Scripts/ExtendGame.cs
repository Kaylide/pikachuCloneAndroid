using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExtendGame : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameController controller;
    private AudioManager audioManager;
    public int level;
    public int starNumber,totalStart;
    public Image starImgWin;
    public Text coinTxtWin;
    public Coins coin;
    public int temp;
    public List<Sprite> listStar;
    public float time;
    private bool flag = true;
    void Awake()
    {
        controller = gameObject.GetComponent<GameController>();
        audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
        controller.typeGame = "ExtendGame";
        audioManager.notificaSound.clip = audioManager.ExStartLevelClip;
        audioManager.notificaSound.Play();
        audioManager.SetMusicExGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("Coins").GetComponent<Coins>();
        controller.line.SourceSprites = Resources.LoadAll<Sprite>("Sprites/ExtendGameImg");
        level = PlayerPrefs.GetInt("ExGameLevel");
        totalStart = PlayerPrefs.GetInt("TotalStars");
        time = 15f + 15f * (level / 2);
        setGameExtend();
        controller.line.Height = (int)(80f / ((controller.Row - 2) * 0.3));
        controller.line.Width = controller.line.Height;
        Time.timeScale = 1;
        temp = (controller.Row - 2) * (controller.Col - 2);
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseDifficulty();
        controller.line.UpdateValueButton();
        PlusTime();
        if (checkFinishGame()) {
            if (flag) {
                plusCoins();
                flag = false;
            }
        }
            
    }
    void setGameExtend()
    {
        switch (level)
        {
            case 1:
                controller.Row = 6;
                controller.Col = 7;
                controller.level = level;
                controller.time = time;
                break;
            case 2:
                controller.Row = 6;
                controller.Col = 8;
                controller.level = level;
                controller.time = time;
                break;
            case 3:
                controller.Row = 7;
                controller.Col = 8;
                controller.level = level;
                controller.time = time;
                break;
            case 4:
                controller.Row = 8;
                controller.Col = 9;
                controller.level = level;
                controller.time = time;
                break;
            case 5:
                controller.Row = 8;
                controller.Col = 10;
                controller.level = level;
                controller.time = time;
                break;
            case 6:
                controller.Row = 8;
                controller.Col = 14;
                controller.level = level;
                controller.time = time;
                break;
        }
        if (level  > 6)
        {
            controller.Row = 8;
            controller.Col = 15;
            controller.level = level;
            controller.time = time;
        }
    }
    private bool checkFinishGame()
    {
        if (controller.time > 0)
        {
            if (controller.btnList.Count == 0)
            {            
                audioManager.notificaSound.PlayOneShot(audioManager.ExWinClip);
                Time.timeScale = 0;
                winCanvas.SetActive(true);
                return true;
            }
        }
        else
        {
            starNumber = 0;
            Time.timeScale = 0;
            loseCanvas.SetActive(true);       
            PlayerPrefs.SetInt("coins", coin.coin);

        }
        return false;
    }
    private void plusCoins() {
            float timereport = (controller.time / time) * 100;
            if (timereport < 30)
            {
                starNumber = 1;
                starImgWin.sprite = listStar[starNumber];
                coin.coin += 200;
                coinTxtWin.text = "" + 200;
            }
            if (timereport >= 30)
            {
                starNumber = 2;
                starImgWin.sprite = listStar[starNumber];
                coin.coin += 300;
                coinTxtWin.text = "" + 500;
            }
            if (timereport >= 50)
            {
                starNumber = 3;
                starImgWin.sprite = listStar[starNumber];
                coin.coin += 500;
                coinTxtWin.text = "" + 500;
            }
            if (starNumber > PlayerPrefs.GetInt("starLevel" + level))
            {
                int count = starNumber - PlayerPrefs.GetInt("starLevel" + level);
                PlayerPrefs.SetInt("TotalStars", totalStart + count);
                PlayerPrefs.SetInt("starLevel" + level, starNumber);

            }
            PlayerPrefs.SetInt("coins", coin.coin);   
    }
    private void PlusTime() {
        if ((temp - controller.btnList.Count) == 2)
        {
            temp = controller.btnList.Count;
            if (controller.time < controller.slider.maxValue)
                controller.time  += 1;      
            controller.CheckLogicGame();
        }
    }
    private void IncreaseDifficulty()
    {
        switch (level)
        {         
            case 4:
                controller.line.GoLeftButton();
                break;
            case 5:
                controller.line.GoRightButton();
                controller.line.GoBottomtButton();
                break;
            case 6:
                controller.line.GoLeftButton();
                controller.line.GoBottomtButton();
                break;
            case 7:
                controller.line.GoLeftButton();
                controller.line.GoTopButton();
                break;
            case 8:
                controller.line.GoRightButton();
                controller.line.GoTopButton();
                break;
        }
        if (level > 8) {
            controller.line.GoRightButton();
            controller.line.GoTopButton();
        }
    }
}