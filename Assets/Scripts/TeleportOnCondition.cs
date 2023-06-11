using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnCondition : MonoBehaviour
{
    public bool playerClose, conditionMet;
    [SerializeField] PlayerController player;
    [SerializeField] Transform destination;
    [SerializeField] GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
        if (playerClose)
        {
            if (conditionMet)
            {
                dialoguePanel.SetActive(false);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TeleportTo();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialoguePanel.SetActive(true);
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

    private void CheckCondition()
    {
        if (player.key_amount == 4)
        {
            conditionMet = true;
        }
        else
        {
            conditionMet = false;
        }
    }

    private void TeleportTo()
    {
        player.transform.position = destination.position;
    }
}
