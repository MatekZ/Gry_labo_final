using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inv;
  //  public Character character;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameisPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }


    public void Save()
    {
        DataPersistanceManager.Instance.SaveGame();
      //  ItemSaveManager.Instance.SaveEquipment(character);
      //  ItemSaveManager.Instance.SaveInventory(character);
    }

    public void Resume()
    {
        inv.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

    }

    void Pause()
    {
        inv.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;

    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
