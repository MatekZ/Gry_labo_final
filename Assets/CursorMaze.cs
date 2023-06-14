using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorMaze : MonoBehaviour
{
    private float timer = 0f;
    public float interval = 1111111111f;
    public GameObject miniGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            miniGame.SetActive(false);
            timer = 0f;
        }
    }
   

    private void OnMouseExit()
    {
        Debug.Log("exit");  
        miniGame.SetActive(false);

    }

    /*public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click");
    }*/
}
