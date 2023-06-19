using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Coinscollectedlvl : MonoBehaviour, IDataPresistance
{
    public TextMeshProUGUI coins;
    public string name1;
    public void LoadData(GameData data)
    {
   

        if (name1 == "Poziom_0")
        {
            coins.text = data.coins_lvl0.ToString() + "/" + "20";
        }
        if (name1 == "Poziom_1")
        {
            coins.text = data.coins_lvl1.ToString();
        }
        if (name1 == "Poziom_2")
        {
            coins.text = data.coins_lvl2.ToString();
        }

        if (name1 == "DarkCastle")
        {
            coins.text = data.coins_lvl3.ToString();
        }

    }

    public void SaveData(ref GameData data)
    {

    }



    }
