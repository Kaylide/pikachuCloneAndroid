using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mapSelectPanel;
    //public GameObject CanvasSelect;
    public GameObject[] levelSelectionPanels;
    public static UIManager instance;
    public int stars;
    public Text starTxt;
    public Text[] quesStartTxt;
    public Text[] lockStartTxt;
    public Text[] unlockStartTxt;
    public MapSelect[] mapSelects;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            if (instance != this) {
                Destroy(gameObject);
            }         
        }
      //  DontDestroyOnLoad(gameObject);

    }
    public void Update()
    {
       // setCamera();
        UpdateStart();
        UpdateLockUI();
        UpdateUnLockUI();

    }
    private void UpdateLockUI() {
        for (int i = 0; i < mapSelects.Length; i++) {
            quesStartTxt[i].text = mapSelects[i].questNum.ToString();
            if (mapSelects[i].isUnlock == false) {
                lockStartTxt[i].text = stars.ToString() + "/" + mapSelects[i].endLevel * 3;
            }
           
        }
    }
    private void UpdateUnLockUI()
    {
        for (int i = 0; i < mapSelects.Length; i++)
        {         
            unlockStartTxt[i].text = stars.ToString() + "/" + mapSelects[i].endLevel * 3;
        }
    }
    private void UpdateStart()
    {
        stars = PlayerPrefs.GetInt("TotalStars");
        starTxt.text = stars.ToString();
    }
    public void MapButtonUnlock(int indexMap)
    {
        if (mapSelects[indexMap].isUnlock == true)
        {
            levelSelectionPanels[indexMap].gameObject.SetActive(true);
            mapSelectPanel.gameObject.SetActive(false);
        }
        else {
            Debug.Log("Can not open this map now");
        }
    }
    public void ButtonBackEvt()
    {
        mapSelectPanel.gameObject.SetActive(true);
        for (int i = 0; i < mapSelects.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }
    public void BackMapSelection() {
        mapSelectPanel.gameObject.SetActive(true);
        for (int i = 0; i < mapSelects.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }
}
