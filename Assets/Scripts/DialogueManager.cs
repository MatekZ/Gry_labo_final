using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] dialogSentences;
    public int i = 0;
    public float dialogueSpeed;
    public TextMeshProUGUI dialogueTextBox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueTextBox.alpha > 0 && string.IsNullOrWhiteSpace(dialogueText.text))
        {
            NextSentence();


        }

    }

    private void NextSentence()
    {
        if (i <= dialogSentences.Length - 1)
        {
            dialogueText.text = "";
            StartCoroutine(ShowText());
        }
        else
        {
            dialogueText.text = "";
            i = 0;
        }
    }



    IEnumerator ShowText()
    {
        foreach (char character in dialogSentences[i].ToCharArray())
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        i++;

    }
}
