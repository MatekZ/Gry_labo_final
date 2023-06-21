using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour, IDataPresistance
{
    public string levelName;
    public bool playerClose;
    public Vector3 PlayerPosition;
    public Character character;
    public bool lvl0;
    public bool lvl1;
    public bool lvl2;
    public bool lvl3;



    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       
        if (playerClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

               
                //  DataPersistanceManager.Instance.LoadGame();

                if (levelName == "HUB")
                {
                    PlayerPosition = new Vector3(4f, -4f, 0f);

                    DataPersistanceManager.Instance.ChangePosition(PlayerPosition);


                    SceneManager.LoadScene(levelName);

                }

                if (levelName == "Poziom_0")
                {
                    PlayerPosition = new Vector3(-86f, -0.88f, 0f);
                    DataPersistanceManager.Instance.ChangePosition(PlayerPosition);

                    SceneManager.LoadScene(levelName);

                }

                if (levelName == "Poziom_1" && lvl1 == true)
                {
                    PlayerPosition = new Vector3(-6f, 0.6f, 0f);
                   // PlayerPosition = new Vector3(428f, -210.6f, 0f);
                    DataPersistanceManager.Instance.ChangePosition(PlayerPosition);


                    SceneManager.LoadScene(levelName);
                }
                if (levelName == "DarkCastle" && lvl2==true)
                {
                     PlayerPosition = new Vector3(-6f, 0.6f, 0f);
                   // PlayerPosition = new Vector3(371f, 2f, 0f);
                    DataPersistanceManager.Instance.ChangePosition(PlayerPosition);


                    SceneManager.LoadScene(levelName);

                }

                if (levelName == "UndeadCutscene" && lvl3 == true)
                {
                    // PlayerPosition = new Vector3(-6f, 0.6f, 0f);
                    PlayerPosition = new Vector3(-4f, 0f, 0f);
                    DataPersistanceManager.Instance.ChangePosition(PlayerPosition);


                    SceneManager.LoadScene(levelName);

                }


            }
        }
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           playerClose = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = false;   
        }
    }

    public void LoadData(GameData data)
    {
        DataPersistanceManager.Instance.LoadInventory(character);
        DataPersistanceManager.Instance.LoadEquipment(character);

        lvl0 = data.lvl0;
        lvl1 = data.lvl1;
        lvl2 = data.lvl2;


      

    }


    public void SaveData(ref GameData data)
    {
         Scene scene = SceneManager.GetActiveScene();
         data.LvlName = scene.name;




        if (data.LvlName == "HUB")
        {

           


        }

        if (data.LvlName == "Poziom_0")
        {
         
            data.lvl1 = true;
            

        }

        if  (data.LvlName == "Poziom_1")
        {
           data.lvl2 = true;

        }
        if (data.LvlName == "DarkCastle")
        {
           
            data.lvl3 = true;   

        }

        DataPersistanceManager.Instance.SaveEquipment(character);
        DataPersistanceManager.Instance.SaveInventory(character);   
        
    }


  
}
