 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class DataPersistanceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

  
    private GameData gameData;
    private List<IDataPresistance> dataPresistanceObjects;    
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";

    private int i = 0;
  public static DataPersistanceManager Instance {  get; private set; }


    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
      
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
;
    }
    

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        this.dataPresistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
      
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

   
    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;

        LoadGame();
    }


    public void ChangePosition(Vector3 PLayer)
    {
        this.gameData.PlayerPosition = PLayer;
    }


    public void NewGame()
    {
        this.gameData = new GameData();
        i = 1;
    }


    public void LoadGame()
    {
        

        this.gameData = dataHandler.Load(selectedProfileId);
     


        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaluts");
            return;
        }

        foreach(IDataPresistance dataPresistanceObj in dataPresistanceObjects)
        {
            dataPresistanceObj.LoadData(gameData);
        }
      
    }


    public string GetScene()
    {
        return this.gameData.LvlName;
    }


    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found");
            return;
        }

        foreach (IDataPresistance dataPresistanceObj in dataPresistanceObjects)
        {
           
                dataPresistanceObj.SaveData(ref gameData);
            
           
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();


        dataHandler.Save(gameData,selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPresistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPresistance> dataPresistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPresistance>();

        return new List<IDataPresistance>(dataPresistanceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public  Dictionary<string,GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    [SerializeField] ItemDatabase itemDatabase;

	private const string InventoryFileName = "Inventory";
	private const string EquipmentFileName = "Equipment";

	public void LoadInventory(Character character)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
		if (savedSlots == null) return;
		character.Inventory.Clear();

        if (i == 1)
        {
            i = 0;
            return;
        }

		for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
		{
			ItemSlot itemSlot = character.Inventory.ItemSlots[i];
			ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

			if (savedSlot == null)
			{
				itemSlot.Item = null;
				itemSlot.Amount = 0;
			}
			else
			{
				itemSlot.Item = itemDatabase.GetItemCopy(savedSlot.ItemID);
				itemSlot.Amount = savedSlot.Amount;
			}
		}
	}

	public void LoadEquipment(Character character)
	{
		ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(EquipmentFileName);
		if (savedSlots == null) return;

		foreach (ItemSlotSaveData savedSlot in savedSlots.SavedSlots)
		{
			if (savedSlot == null) {
				continue;
			}

			Item item = itemDatabase.GetItemCopy(savedSlot.ItemID);
			character.Inventory.AddItem(item);
			character.Equip((EquippableItem)item);
		}
	}

 

	public void SaveInventory(Character character)
	{
		SaveItems(character.Inventory.ItemSlots, InventoryFileName);
	}

	public void SaveEquipment(Character character)
	{
		SaveItems(character.EquipmentPanel.EquipmentSlots, EquipmentFileName);
	}

	private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
	{
		var saveData = new ItemContainerSaveData(itemSlots.Count);

		for (int i = 0; i < saveData.SavedSlots.Length; i++)
		{
			ItemSlot itemSlot = itemSlots[i];

			if (itemSlot.Item == null) {
				saveData.SavedSlots[i] = null;
			} else {
				saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.Item.ID, itemSlot.Amount);
			}
		}

		ItemSaveIO.SaveItems(saveData, fileName);
	}   

}
