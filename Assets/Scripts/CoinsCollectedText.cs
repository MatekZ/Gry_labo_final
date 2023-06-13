using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        Scene scene = SceneManager.GetActiveScene();
        data.LvlName = scene.name;
        Debug.Log("" + scene.name);
        if (scene.name == "Poziom_0")
        {
            coinsCollected = data.coins_lvl0;
        }
        if (scene.name == "Poziom_1")
        {
            coinsCollected = data.coins_lvl1;
        }
        if (scene.name == "Poziom_2")
        {
            coinsCollected = data .coins_lvl2;
        }

    }

    public void SaveData(ref GameData data)
    {
        Scene scene = SceneManager.GetActiveScene();
        data.LvlName = scene.name;
        Debug.Log("" + scene.name);
        if (scene.name == "Poziom_0")
        {
            data.coins_lvl0 = coinsCollected;
        }
        if (scene.name == "Poziom_1")
        {
            data.coins_lvl1 = coinsCollected;
        }
        if (scene.name == "Poziom_2")
        {
            data.coins_lvl2 = coinsCollected;
        }

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
