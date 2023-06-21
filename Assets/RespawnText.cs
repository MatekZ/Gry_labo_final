using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RespawnText : MonoBehaviour
{
    public Image savingImage;
    public TextMeshProUGUI savingText;
    //public GameObject savingPanel;
    public float fadeSpeed = 0.5f;
    public float waitTime = 1f;

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
            savingImage.gameObject.SetActive(true);
            StartCoroutine(SavingPanelCoroutine());

        }
    }
    private IEnumerator SavingPanelCoroutine()
    {
        savingImage.gameObject.SetActive(true);
        savingText.gameObject.SetActive(true);
        float currentAlpha = 1f;
        while (currentAlpha > 0f)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
            Color panelColor = savingImage.color;
            panelColor.a = currentAlpha;
            savingImage.color = panelColor;

            Color textColor = savingText.color;
            textColor.a = currentAlpha;
            savingText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
        savingImage.gameObject.SetActive(false);
        savingText.gameObject.SetActive(false);

    }
}
