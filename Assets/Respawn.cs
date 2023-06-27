using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Respawn : MonoBehaviour
{
    

    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Save();
            Debug.Log("enter");
            player.respawnPointNum++; 
            this.gameObject.SetActive(false);

        }
    }

    public void Save()
    {
        DataPersistanceManager.Instance.SaveGame();
        //  ItemSaveManager.Instance.SaveEquipment(character);
        //  ItemSaveManager.Instance.SaveInventory(character);
    }
}
