using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtItems : MonoBehaviour
{
    private float time = 10;
    private GameController gameController;
    int count = 0;
    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EvtItemtimePlus() {
        int quanlity = PlayerPrefs.GetInt("PlusTime10");
        if (quanlity > 0 && count < 3)
        {
            quanlity--;
            gameController.time += 10;
            PlayerPrefs.SetInt("PlusTime10", quanlity);
            count++;
        }
        else {
            Toast.Instance.Show("Used 3 item, you can not continue", 2f, Toast.ToastColor.Red);
        }
    }
    public void EvtItemLazer()
    {
        int quanlity = PlayerPrefs.GetInt("Lazer");
        if (quanlity > 0 && count < 3)
        {
            quanlity--;
            gameController.lazerTime = 5;
            gameController.itemLaze = true;
            PlayerPrefs.SetInt("Lazer", quanlity);
            count++;
            Toast.Instance.Show("You have 5s for Magic Lazer", 2f, Toast.ToastColor.Green);
        }
        else
        {
            Toast.Instance.Show("Used 3 item, you can not continue", 2f, Toast.ToastColor.Red);
        }
    }
}
