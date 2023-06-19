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
                   
                     
                
                }

                if (levelName == "Poziom_0")
                {
                    PlayerPosition = new Vector3(-86f, -0.88f, 0f);
                   

                }

                if (levelName == "Poziom_1")
                {
                    // PlayerPosition = new Vector3(-6f, 0.6f, 0f);
                    PlayerPosition = new Vector3(428f, -210.6f, 0f);
                
                }
                if (levelName == "DarkCastle")
                {
                    // PlayerPosition = new Vector3(-6f, 0.6f, 0f);
                    PlayerPosition = new Vector3(-4f, 0f, 0f);
                  

                }

                DataPersistanceManager.Instance.ChangePosition(PlayerPosition);
               





                SceneManager.LoadScene(levelName);
       

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
      //   SceneManager.LoadScene(data.LvlName);
    }


    public void SaveData(ref GameData data)
    {
         Scene scene = SceneManager.GetActiveScene();
         data.LvlName = scene.name;
        DataPersistanceManager.Instance.SaveEquipment(character);
        DataPersistanceManager.Instance.SaveInventory(character);   
        
    }


  
}
