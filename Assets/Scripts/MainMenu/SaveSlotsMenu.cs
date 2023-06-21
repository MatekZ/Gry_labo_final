using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private StartGameButton mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    private SaveSlot[] saveSLots;


    private bool isLoadingGame = false;


    private void Awake()
    {
        saveSLots = this.GetComponentsInChildren<SaveSlot>();
    }


    public void OnSaveSlotsClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        DataPersistanceManager.Instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        ItemSaveIO.getid(saveSlot.GetProfileId());

        if (!isLoadingGame)
        {

            DataPersistanceManager.Instance.NewGame();
            ItemSaveIO.DeleteFile();
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            SceneManager.LoadSceneAsync(DataPersistanceManager.Instance.GetScene());
        }

    

      
    }


    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }
   

    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);

        this.isLoadingGame = isLoadingGame;


        Dictionary<string,GameData> profilesGameData = DataPersistanceManager.Instance.GetAllProfilesGameData();

        GameObject firstSelected = backButton.gameObject;


        foreach (SaveSlot saveSlot in saveSLots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData); 
            saveSlot.SetData(profileData);

            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if(firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        StartCoroutine(this.SetFirstSelected(firstSelected));
    }


    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);    
    }

    public void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSLots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;    
    }

}
