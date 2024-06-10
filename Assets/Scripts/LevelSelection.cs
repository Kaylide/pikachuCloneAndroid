using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelSelection : MonoBehaviour
{
    public bool isUnlocked = false;
    public Image lockImage;
    public Image[] starImage;
    public Sprite[] starSprite;
  //  public GameObject SelectLevelCanvas;
    public void Update()
    {
        UnlockLevel();
        UpdateLevelButton();
        
    }
    public void UpdateLevelButton() {
        if (isUnlocked || gameObject.name =="1")
        {
            lockImage.gameObject.SetActive(false);
            for (int i = 0; i < starImage.Length; i++)
            {
                starImage[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < PlayerPrefs.GetInt("starLevel" + gameObject.name); j++)
            {
                starImage[j].sprite = starSprite[j];
            }
        }
        else {
            lockImage.gameObject.SetActive(true);
            for (int i = 0; i < starImage.Length; i++)
            {
                starImage[i].gameObject.SetActive(false);
            }
            
        }
        
    }
    private void UnlockLevel() {
        int CurrentLevel = int.Parse(gameObject.name);
        if (CurrentLevel != 1)
        {
            int previousLevel = CurrentLevel - 1;
            if (PlayerPrefs.GetInt("starLevel" + previousLevel) > 0)
            {
                isUnlocked = true;
            }
        } 
    }
    public void GetLevelSelection()
    {
        int level = Int32.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        PlayerPrefs.SetInt("ExGameLevel", level);
       // SelectLevelCanvas.SetActive(false);
    }
}
