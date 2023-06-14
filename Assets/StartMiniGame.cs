using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMiniGame : MonoBehaviour
{
    public GameObject route, miniGame, end; 
   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("clikc");

        route.SetActive(true);
        end.SetActive(true);
    }
   

}
