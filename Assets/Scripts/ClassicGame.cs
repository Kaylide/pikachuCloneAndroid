using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicGame : MonoBehaviour
{
    public int level;
    public GameController controller;
    public GameObject nextLevelCanvas;
    public GameObject LoseCanvas;
    public Image levelCanvas;
    public int highscore;
    public Text Hightext;
    public int score;
    public Text Scoretext;
    public int temp;
    public int scorePlus;
    private AudioManager audioManager;
    void Awake()
    {
        controller = gameObject.GetComponent<GameController>();      
        audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
        controller.typeGame = "ClassicGame";
        setMusicClassicGame();
    }
    void Start()
    {
        controller.line.SourceSprites = Resources.LoadAll<Sprite>("Sprites/GameImg");        
        if (PlayerPrefs.HasKey("LevelCurrent"))
        {
            level = PlayerPrefs.GetInt("LevelCurrent");
        }
        else
            level = 1;
        if (PlayerPrefs.HasKey("HightScore")) {
            highscore = PlayerPrefs.GetInt("HightScore");
        }
        else
            highscore = 0;
        if (PlayerPrefs.HasKey("CurrentScore"))
        {
            score = PlayerPrefs.GetInt("CurrentScore");
        }
        else
            score = 0;      
        Hightext.text = "" + highscore;
        Scoretext.text = "" + score;
        scorePlus = (int)(150 + 150 * ((float)level / 10f));
        levelCanvas.sprite = controller.spriteLevel[level];
        StartCoroutine(StartDelay());
        setGameClassic();
        controller.line.Height = (int)(150f / ((controller.Row - 2) * 0.5));
        controller.line.Width = controller.line.Height;
        temp = (controller.Row - 2) * (controller.Col - 2);
    }
    void Update()
    {
        IncreaseDifficulty();
        controller.line.UpdateValueButton();
        PlusScore();
        UpdateHightScore();
        checkFinishGame();
    }
    void setGameClassic() {
        switch (level) {
            case 1:
                controller.Row = 8;
                controller.Col = 12;
                controller.level = level;
                controller.time = 300f + 300f * (level / 5);
                break;
            case 2:
                controller.Row = 8;
                controller.Col = 14;
                controller.level = level;
                controller.time = 300f + 300f * (level / 5);
                break;
            case 3:
                controller.Row = 9;
                controller.Col = 14;
                controller.level = level;
                controller.time = 300f + 300f * (level / 5);
                break;
            case 4:
                controller.Row = 9;
                controller.Col = 16;
                controller.level = level;
                controller.time = 300f + 300f * (level / 5);
                break;
            case 5:
                controller.Row = 9;
                controller.Col = 18; 
                controller.level = level;
                controller.time = 300f + 300f * (level / 5);
                break;           
        }
        if (level > 5) {
            controller.Row = 9;
            controller.Col = 18;
            controller.level = level;
            controller.time = 300f + 300f * (level / 5);
        }
    }
    private void checkFinishGame() {
        if (controller.time > 0)
        {
            if (controller.btnList.Count == 0)
            {
                Time.timeScale = 0;
                PlayerPrefs.SetInt("LevelCurrent", level + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            if (highscore >= score) {
                PlayerPrefs.SetInt("HightScore", highscore);
            }           
            LoseCanvas.SetActive(true);
            audioManager.LoseClassicMusic();
        }
    }
    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float timeDelay = Time.realtimeSinceStartup + 4.5f;
        audioManager.notificaSound.clip = audioManager.StartLervelClip;
        audioManager.notificaSound.Play();
        while (Time.realtimeSinceStartup < timeDelay)
            yield return 0;
        nextLevelCanvas.SetActive(false);
        Time.timeScale = 1;


    }
    private void PlusScore() {
        if ((temp - controller.btnList.Count) == 2) {
            temp = controller.btnList.Count;
            score += scorePlus;
            Scoretext.text = "" + score;
            Debug.Log("Plus score !");
            controller.CheckLogicGame();

        }
    }
    private void UpdateHightScore() {
        if (score > highscore) {
            highscore = score;
            Hightext.text = "" + highscore;
            PlayerPrefs.SetInt("HightScore", highscore);
            controller.CheckLogicGame();
        }
        PlayerPrefs.SetInt("CurrentScore", score);
    }
    private void IncreaseDifficulty() {
        switch (level)
        {           
            case 3:
                controller.line.GoRightButton();               
                break;
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
    }
    public void setMusicClassicGame()
    {
        audioManager.SetMusicForClassicGame();
    }

}
