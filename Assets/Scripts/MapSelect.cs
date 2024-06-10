using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public bool isUnlock = false;
    public GameObject lockGo;
    public GameObject unLockGo;

    public int mapIndex; //the index of this map
    public int questNum;// How many stars can unlock this map
    public int startLevel;
    public int endLevel;

    private void Update()
    {
        UpdateMapStatus();
        UnlockMap();
    }

    private void UpdateMapStatus()
    {
        if (isUnlock)
        {
            unLockGo.gameObject.SetActive(true);
            lockGo.gameObject.SetActive(false);
        }
        else {
            unLockGo.gameObject.SetActive(false);
            lockGo.gameObject.SetActive(true);
        }
    }
    private void UnlockMap()
    {
        if (UIManager.instance.stars >= questNum)
        {
            isUnlock = true;
        }
        else {
            isUnlock = false;
        }
    }
}
