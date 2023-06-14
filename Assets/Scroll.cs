using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public GameObject miniGame, startDialogue, skipText, start, end, maze;
    public bool playerClose;
    public TMP_Text dialogueText;
    public string[] dialogueSentences;
    public int countToPanel;
    private int i;
    public float dialogueSpeed;
    public bool firstInteraction = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerClose && firstInteraction == true)
        {
            if (startDialogue.activeInHierarchy)
            {
                TextReset();
            }
            else
            {
                startDialogue.SetActive(true);
                StartCoroutine(ShowText());
                firstInteraction = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerClose && firstInteraction == false)
        {
            miniGame.SetActive(true);
            start.SetActive(true);
            end.SetActive(false);
            maze.SetActive(false);

        }
        if (dialogueText.text == dialogueSentences[i])
        {
            //NPCCanvas.SetActive(true);
            skipText.SetActive(true);


            if (Input.GetKeyDown(KeyCode.G) && playerClose)
            {

                NextSentence();
                if (countToPanel <= 0)
                {
                    miniGame.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerClose = true;
        }
    }

    public void TextReset()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        i = 0;
        startDialogue.SetActive(false);
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

}
