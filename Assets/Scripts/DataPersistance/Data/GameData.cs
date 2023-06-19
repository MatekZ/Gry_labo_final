using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]


public class GameData 
{

    public string LvlName;
    public long lastUpdated;
    public int health;
    public Vector3 PlayerPosition;
    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, bool> chest1;
    public int coins_lvl0;
    public int coins_lvl1;
    public int coins_lvl2;
    public int coins_lvl3;
  
    public GameData()
    {
      
             this.health = 100;
          // PlayerPosition =  new Vector3(-84.95f, -0.88f,  0f);
             PlayerPosition = new Vector3(146f, 60f, 0f);
             coinsCollected = new SerializableDictionary<string, bool>();
             chest1 = new SerializableDictionary<string, bool>();
             this.LvlName = "";
             coins_lvl0 = 0;
             coins_lvl1 = 0;
             coins_lvl2 = 0;
             coins_lvl3 = 0;


    }

    public int GetProcentageComplete()
    {
        int totalCollected = 0;

        foreach(bool collected  in coinsCollected.Values) 
        {
            if (collected)
            {
                totalCollected++;
            }
        }
        int procentageCompleted = -1;
        if(coinsCollected.Count != 0) 
        {
            procentageCompleted = (totalCollected *100 / 100);
        }
    return procentageCompleted;
    }

}
 