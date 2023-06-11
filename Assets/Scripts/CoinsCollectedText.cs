using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsCollectedText : MonoBehaviour, IDataPresistance
{
    [SerializeField] private int totalCoins = 0;

    private int coinsCollected = 0;

    private TextMeshProUGUI coinsCollectedText;



  
    private void Awake()
    {
        coinsCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // subscribe to events
        GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }

    private void OnDestroy()
    {
        // unsubscribe from events
        GameEventsManager.instance.onCoinCollected -= OnCoinCollected;
    }

    public void LoadData(GameData data)
    {
     foreach(KeyValuePair<string, bool> pair in data.coinsCollected)
        {
            if(pair.Value)
            {
                coinsCollected++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
       
    }

    private void OnCoinCollected()
    {
        coinsCollected++;
    }

    private void Update()
    {
        coinsCollectedText.text = coinsCollected + " / " + totalCoins;
    }

}