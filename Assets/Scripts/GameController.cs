using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    //public Text TimeText;
    public string typeGame;
    public Slider slider;
    public Algorithm line;
    public int Row, Col, level;
    public List<GameObject> btnList = new List<GameObject>();
    public List<GameObject> objlist = new List<GameObject>();
    public List<Sprite> spriteList;
    public GameObject[][] MatricBtn;
    private Vector3Int FirstPos = Vector3Int.zero;
    private Vector3Int SecondPos = Vector3Int.zero;
    public float time;
    public Sprite[] spriteLevel;
    public Image levelImage;
    private AudioManager audioManager;
    public bool itemLaze = false;
    public float lazerTime = 5;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioManager>();
        line = GameObject.FindGameObjectWithTag("Stage").GetComponent<Algorithm>();
    }
    void Start()
    {       
        slider.maxValue = time;
        slider.minValue = 0;       
        line.setCol(Col);
        line.setRow(Row);
        line.CreateMatrix(Row, Col);
        line.GetRamdomSprite(spriteList,level);
        line.RandomMap();
        line.gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        line.gridLayout.constraintCount = Row;
        GetButton();
        AddListener();
        CreateMatricBtn();
        CheckLogicGame();
        UpdateLevel();
    }
    void Update() {      
        if (time > 0) {
            time -= 1 * Time.deltaTime;   
            slider.value = time;        
        }
        if (itemLaze) {
            if (lazerTime > 0)
            {
                lazerTime -= 1 * Time.deltaTime;
            }
            else {
                itemLaze = false;
                Toast.Instance.Show("The Magic Lazer time has run out", 3f, Toast.ToastColor.Blue);
            }         
        }                
    }
    public void CheckLogicGame() {
        Vector3Int p1 = new Vector3Int();
        Vector3Int p2 = new Vector3Int();
        int countCouple = 0;
        do
        {          
            for (int i = 0; i < btnList.Count; i++)
            {
                for (int j = i + 1; j < btnList.Count; j++)
                {                                      
                        int index = btnList[i].name.LastIndexOf(",");
                        p1.x = int.Parse(btnList[i].name.Substring(0, index));
                        p1.y = int.Parse(btnList[i].name.Substring(index + 1));
                        p2.x = int.Parse(btnList[j].name.Substring(0, index));
                        p2.y = int.Parse(btnList[j].name.Substring(index + 1));
                        if (line.checkdLine(p1, p2))
                        {                           
                            if (btnList.Count > 6)
                            {
                                countCouple++;
                                if (countCouple > 1)
                                {
                                    Debug.Log("Can play continute");
                                    Debug.Log("" + p1 + "-->" + p2);
                                    return;
                                }
                            }
                            else {
                                Debug.Log("Can play continute");
                                Debug.Log("" + p1 + "-->" + p2);
                                return;
                            }                           
                        }                    
                }
            }
            RandomMap();
            Debug.Log("RandomMap !!!");
        }
        while (line.checkdLine(p1, p2));
       
        

    }
    private void RandomMap() {
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < btnList.Count; i++) {
            sprites.Add(btnList[i].GetComponent<Button>().image.sprite);
        }
        line.ShuffleListSprite(sprites);
        int index = 0;
        for (int i = 1; i < Row - 1; i++) {
            for (int j = 1; j < Col - 1; j++)
            {
                if (line.Matrix[i][j] > 0 && btnList.Contains(MatricBtn[i][j])) {
                    MatricBtn[i][j].GetComponent<Button>().image.sprite = sprites[index];
                    line.Matrix[i][j] = int.Parse(sprites[index].name);
                    index++;
                }
            }
        }
        if (typeGame.Equals("ClassicGame")) {
            audioManager.notificaSound.clip = audioManager.RandomMapClip;
            audioManager.notificaSound.Play();
        }
        else if (typeGame.Equals("ExtendGame")) {
            audioManager.notificaSound.clip = audioManager.ExRandomClip;
            audioManager.notificaSound.Play();
        }       
    } 
    void GetButton()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("btn");
        for (int i = 0; i < objects.Length; i++) {
            btnList.Add(objects[i]);
        }
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("Wall");
        for (int j = 0; j < objects2.Length; j++)
        {
            objlist.Add(objects2[j]);
        }
    }
    void showMatrix()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                Debug.Log("" + MatricBtn[i][j].transform.position);
            }       
        }
    }
    void CreateMatricBtn()
    {
        int p = 0;
        int p1 = 0;
        MatricBtn = new GameObject[Row][];
        for (int i = 0; i < Row; i++)
        {
            MatricBtn[i] = new GameObject[Col];
            for (int j = 0; j < Col ; j++) {
                if (i == 0 || j == 0 || i == Row - 1 || j == Col - 1)
                {
                    MatricBtn[i][j] = objlist[p];
                    p++;
                }
                else {
                    MatricBtn[i][j] = btnList[p1];
                    p1++;
                }
                               
            }
        }
        Debug.Log("ok");
    }
   
    void AddListener() 
    {
        foreach (GameObject btn in btnList) {
            btn.transform.GetComponent<Button>().onClick.AddListener( () => EvtClick() );
        }
    }
    void EvtClick() 
    {
        if (typeGame.Equals("ClassicGame"))
        {
            audioManager.puzzleButtonSound.Play();
        }
        else if (typeGame.Equals("ExtendGame")) {
            audioManager.puzzleButtonSound.clip = audioManager.ExSelectClip;
            audioManager.puzzleButtonSound.Play();
        }
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int index = name.LastIndexOf(",");
        int x = int.Parse(name.Substring(0,index));
        int y = int.Parse(name.Substring(index+1));       
        if (FirstPos == Vector3Int.zero)
        {
            FirstPos = new Vector3Int(x, y, 0);          
            MatricBtn[FirstPos.x][FirstPos.y].transform.GetComponent<Image>().color = new Color(0, 2, 47, 100);
            Debug.Log("Pos = " + x + "; " + y);           
        }
        else {
            SecondPos = new Vector3Int(x, y, 0);
            Debug.Log("" + FirstPos.x + ", " + FirstPos.y + "-----> " + SecondPos.x + ", " + SecondPos.y);
            if (itemLaze)
            {
                if (!FirstPos.Equals(SecondPos) && line.Matrix[FirstPos.x][FirstPos.y] == line.Matrix[SecondPos.x][SecondPos.y])
                {
                    line.ConnectTowPoint(FirstPos, SecondPos);
                    StartCoroutine(line.ClearLine(FirstPos));
                    Execute(FirstPos, SecondPos);
                    audioManager.notificaSound.clip = audioManager.ExNotitrueCLip;
                    audioManager.notificaSound.Play();
                }
                else {
                    audioManager.notificaSound.clip = audioManager.ExNotiFalseClip;
                    audioManager.notificaSound.Play();
                    MatricBtn[FirstPos.x][FirstPos.y].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                
            }
            else {
                if (!line.checkTwoPoint(FirstPos, SecondPos))
                {
                    if (typeGame.Equals("ClassicGame"))
                    {
                        audioManager.notificaSound.clip = audioManager.notificaFalseClip;
                        audioManager.notificaSound.Play();
                    }
                    else if (typeGame.Equals("ExtendGame"))
                    {
                        audioManager.notificaSound.clip = audioManager.ExNotiFalseClip;
                        audioManager.notificaSound.Play();
                    }

                    Debug.Log("False !! ");
                    MatricBtn[FirstPos.x][FirstPos.y].transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Execute(FirstPos, SecondPos);
                    if (typeGame.Equals("ClassicGame"))
                    {
                        audioManager.notificaSound.clip = audioManager.notificaTrueClip;
                        audioManager.notificaSound.Play();
                    }
                    else if (typeGame.Equals("ExtendGame"))
                    {
                        audioManager.notificaSound.clip = audioManager.ExNotitrueCLip;
                        audioManager.notificaSound.Play();
                    }
                }
            }        
            FirstPos = Vector3Int.zero;
            SecondPos = Vector3Int.zero;
            Debug.Log("Done");
        }
    }
    void Execute( Vector3Int v1, Vector3Int v2) {
        MatricBtn[v1.x][v1.y].transform.GetComponent<Button>().interactable = false;
        MatricBtn[v2.x][v2.y].transform.GetComponent<Button>().interactable = false;
        MatricBtn[v1.x][v1.y].transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        MatricBtn[v2.x][v2.y].transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        line.Matrix[v1.x][v1.y] = -1 ;
        line.Matrix[v2.x][v2.y] = -1 ;      
        Debug.Log("Delect");

    }
    public void UpdateLevel()
    {
        levelImage.sprite = spriteLevel[level];      
    }
}
