using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectLevelController : MonoBehaviour
{
    public GameObject panelLevel1;
    public List<GameObject> btnList;
    public List<GameObject> imgListLevel;
    public List<GameObject> listStarImg;
    public List<GameObject> listLock;
    public List<GameObject> listStage;
    public List<Sprite>  listStarNumber;
    public List<Sprite>  listNumberLevel;
    public int level;
    private int startNumber;

    // Start is called before the first frame update
    void Start()
    {       
        GetObject("btn", btnList);
        GetObject("Stage", listStage);
        GetObject("LevelNumber", imgListLevel);
        GetObject("Lock", listLock);
        GetObject("Star", listStarImg);
        if (PlayerPrefs.HasKey("ExGameLevel"))
        {
            level = PlayerPrefs.GetInt("ExGameLevel");
        }
        else
            level = 0;
        SetUpPanelLevel();
        AddListener();
    }
    private void GetObject(string tag , List<GameObject> list ) {
        GameObject[] gob = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gob.Length; i++) {
            list.Add(gob[i]);
        }
    }
    private void SetUpPanelLevel() {
        if (level > 0)
        {
            int indexImgLevel;
            for (int i = 0; i < level; i++)
            {
                if (i > 10)
                    indexImgLevel = i % 10;
                else if (i > 20)
                    indexImgLevel = i % 20;
                else
                    indexImgLevel = i;
                if (i == level - 1)
                {
                    
                    imgListLevel[indexImgLevel].GetComponent<Image>().sprite = listNumberLevel[i];
                    listStarImg[i].SetActive(false);
                    btnList[i].GetComponent<Button>().interactable = true;
                    listLock[i].SetActive(false);
                }
                else
                {
                    startNumber = PlayerPrefs.GetInt("starLevel" +(i+1));
                    listStarImg[i].GetComponent<Image>().sprite = listStarNumber[startNumber];
                    imgListLevel[indexImgLevel].GetComponent<Image>().sprite = listNumberLevel[i];
                    btnList[i].GetComponent<Button>().interactable = true;
                    listLock[i].SetActive(false);
                }
            }
            for (int j = level ; j < btnList.Count; j++)
            {
                listStarImg[j].SetActive(false);
                imgListLevel[j].SetActive(false);
                listLock[j].SetActive(true);
            }
        }
        else {
            imgListLevel[0].GetComponent<Image>().sprite = listNumberLevel[0];
            btnList[0].GetComponent<Button>().interactable = true;
            listStarImg[0].SetActive(false);
            listLock[0].SetActive(false);
            for (int j = 1; j < btnList.Count; j++)
            {
                listStarImg[j].SetActive(false);
                imgListLevel[j].SetActive(false);
                listLock[j].SetActive(true);
            }
        }       
     //   panelLevel2.SetActive(false);
     //   panelLevel3.SetActive(false);

    }
    void AddListener()
    {
        foreach (GameObject btn in btnList)
        {
            btn.transform.GetComponent<Button>().onClick.AddListener(() => EvtClick());
        }
    }
    void EvtClick()
    {
        int levelPlay = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        PlayerPrefs.SetInt("levelPlay",levelPlay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}



