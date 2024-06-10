using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    // Start is called before the first frame update
    public int coin;
    public Text coinText;
    void Update()
    {
        UpdateCoins();
    }
    public void UpdateCoins() {
        if (PlayerPrefs.HasKey("coins"))
        {
            coin = PlayerPrefs.GetInt("coins");
        }
        else
            coin = 0;
        int mili = (coin / 1000000);
        int thousan = (coin / 1000) % 1000;
        int hunder = coin % 1000;
        string formatCoin = string.Format("{0:0},{1:000},{2:000}", mili, thousan, hunder);
       // coinText.text = coin.ToString() ;
        coinText.text = formatCoin;
    }
    // Update is called once per frame
}
