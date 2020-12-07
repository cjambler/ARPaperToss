using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class shopItems : MonoBehaviour
{
    private int currentCoinCount;

    // Start is called before the first frame update
    void Start()
    {
        currentCoinCount = PlayerPrefs.GetInt("Currency", 0);    
    }

    public void BuyItem(int itemPrice, string itemName) 
    {
        if (currentCoinCount >= itemPrice) 
        {
            Debug.Log("Item Bought");
            currentCoinCount -= itemPrice;
            PlayerPrefs.SetInt("Currency", currentCoinCount);
            currentCoinCount = PlayerPrefs.GetInt("Currency", 0);

            Analytics.CustomEvent("Shop Items Bought", new Dictionary<string, object> {
                { "Price", itemPrice },
                { "Item Name", itemName }
            });
        }

        else 
        {
            Debug.Log("Not Enough Coins");
        }
    }
}
