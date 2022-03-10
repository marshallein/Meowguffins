using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountScript : MonoBehaviour
{
    Text coinText;
    public static int coinCount = 0;
    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        coinCount = 0;
    }
    void Start()
    {       
        coinText = GetComponent<Text>();
    }
    private void OnEnable()
    {
        EventManager.CoinCollected += EventManager_CoinCollected;
    }
    private void OnDisable()
    {
        EventManager.CoinCollected -= EventManager_CoinCollected;
    }
     
    private void EventManager_CoinCollected()
    {
        coinCount++;
        coinText.text = coinCount.ToString(); //TODO:
    }

    
}
