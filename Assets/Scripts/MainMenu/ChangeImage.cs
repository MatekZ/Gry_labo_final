using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeImage : MonoBehaviour
{

    public Sprite newButtonImage;
    public Button button;
    public TMP_Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void ChangeButtonImage()
    {
        button.image.sprite = newButtonImage;

    }

    
}
