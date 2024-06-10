using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    private Coins coins;
    public int prices;
    public string itemName;
    public Text quanlityTxt;
    void Awake()
    {
        coins = GameObject.FindGameObjectWithTag("Coins").GetComponent<Coins>();
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("" + itemName))
        {
            int quality = PlayerPrefs.GetInt("" + itemName);
            quanlityTxt.text = "" + quality;
        }
    }
    // Start is called before the first frame update
    public void BuyItems() {
        if (coins.coin > prices) {
            if (PlayerPrefs.HasKey("" + itemName))
            {
                int quanlity = PlayerPrefs.GetInt("" + itemName);
                coins.coin -= prices;
                PlayerPrefs.SetInt("coins", coins.coin);
                PlayerPrefs.SetInt("" + itemName, quanlity + 1);
               // coins.UpdateCoins();
                Toast.Instance.Show("Buy item sucessfully ", 2f, Toast.ToastColor.Green);
            }
            else {
                coins.coin -= prices;
                PlayerPrefs.SetInt("coins", coins.coin);
                PlayerPrefs.SetInt("" + itemName, 1);
               // coins.UpdateCoins();
                Toast.Instance.Show("Buy item sucessfully ", 2f, Toast.ToastColor.Green);
            }
             
        }
    }
   
}
