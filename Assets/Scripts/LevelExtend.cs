using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExtend : MonoBehaviour
{
    private UIManager uIManager;
    public void Awake()
    {
     //   uIManager = GameObject.FindGameObjectWithTag("UImanager").GetComponent<UIManager>();
    }
    public void BackMapSelection()
    {
        
        uIManager.mapSelectPanel.gameObject.SetActive(true);
        for (int i = 0; i < uIManager.mapSelects.Length; i++)
        {
            uIManager.levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }
}
