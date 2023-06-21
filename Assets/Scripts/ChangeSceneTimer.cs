using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour, IDataPresistance
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


    public void LoadData(GameData data)
    {
        data.PlayerPosition = new Vector3(-84.95f, -0.88f, 0f);
        //data.PlayerPosition = new Vector3(146f, 60f, 0f);
    }

    public void SaveData(ref GameData data)
    {

    }

}
