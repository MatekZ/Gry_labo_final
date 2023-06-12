using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour
{
    public float changeTimer;
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            changeTimer = 1;
            SceneManager.LoadScene(sceneName);
        }

        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            SceneManager.LoadScene(sceneName);
        }        
    }
}
