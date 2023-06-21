using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : Menu
{

    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]

    [SerializeField] private Button newGame;
    [SerializeField] private Button continueGame;
    [SerializeField] private Button options;
    [SerializeField] private Button quit;
    [SerializeField] private Button loadGameButton;



    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
     
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }


    private void Start()
    {
        if (!DataPersistanceManager.Instance.HasGameData())
        {
            continueGame.interactable = false;
            loadGameButton.interactable = false;
        }
    }


    public void OnContiniueGameClicked()
    {
        DisableButtons();
        SceneManager.LoadSceneAsync(DataPersistanceManager.Instance.GetScene());
    }

    public void OptionsClicked()
    {
        DisableButtons();
    }

    public void ExitClicked() 
    {
    EnableButtons();
    }

    private void DisableButtons()
    {
        newGame.interactable = false;
        continueGame.interactable = false;
        quit.interactable = false;
        options.interactable = false;
        loadGameButton.interactable = false;
    }

    private void EnableButtons()
    {
        newGame.interactable = true;
        continueGame.interactable = true;
        quit.interactable = true;
        options.interactable = true;
        loadGameButton.interactable = true;

    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

}
