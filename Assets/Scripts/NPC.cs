using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel, skipText;

    public TMP_Text dialogueText;
    public string[] dialogueSentences;
    private int i;
    public float dialogueSpeed;
    public bool playerClose;
    public int countToPanel;
    public GameObject nicoCraftPanel;
    public PlayerController player;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                TextReset();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(ShowText());
            }
        }

        if (dialogueText.text == dialogueSentences[i])
        {
            //NPCCanvas.SetActive(true);
            skipText.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.G) && playerClose)
            {

                NextSentence();
                if (countToPanel == 0)
                {
                    Debug.Log("ostatnie zdanie");
                    nicoCraftPanel.SetActive(true);
                    if (nicoCraftPanel.activeInHierarchy)
                    {
                        player.walkSpeed = 0;
                        player.jumpImpulse = 0;
                    }

                
                }
            }
        }
        //Debug.Log("count = "+ countToPanel);

    }

    public void Set()
    {
        player.walkSpeed = 5;
        player.jumpImpulse = 15;
        countToPanel = 2;
    }

    public void TextReset()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        i = 0;
        dialoguePanel.SetActive(false);
    }


    public void NextSentence()
    {
        countToPanel--;
        skipText.SetActive(false);
        if (i < dialogueSentences.Length - 1)
        {
            i++;
            dialogueText.text = "";
            StartCoroutine(ShowText());
        }
        else
        {
            TextReset();
        }
    }



    IEnumerator ShowText()
    {
        if (playerClose)
        {
            foreach (char character in dialogueSentences[i].ToCharArray())
            {
                dialogueText.text += character;
                yield return new WaitForSeconds(dialogueSpeed);
            }
        }
    }





    public void OnLastContinueClick(int q)
    {
        if (q == 1)
        {
            Debug.Log("q = " + q);
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            TextReset();
            playerClose = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopAllCoroutines();
            playerClose = false;
            TextReset();
            skipText.SetActive(false);
        }
    }

}
