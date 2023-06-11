using System;
using UnityEngine;

public class ItemChest : MonoBehaviour, IDataPresistance

{


    [SerializeField] Item item;
	[SerializeField] int amount = 1;
	[SerializeField] Inventory inventory;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Color emptyColor;
	[SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
	[SerializeField] PlayerController player;


    private bool isInRange;
	private bool isEmpty;


    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnValidate()
	{
		if (inventory == null)
			inventory = FindObjectOfType<Inventory>();

		if (spriteRenderer == null)
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		spriteRenderer.sprite = item.Icon;
		spriteRenderer.enabled = false;
	}

	private void Update()
	{
		if (isInRange && !isEmpty && Input.GetKeyDown(itemPickupKeyCode))
		{
			Item itemCopy = item.GetCopy();
			if (inventory.AddItem(itemCopy))
			{
				amount--;
				if(item.ItemName == "Key1" || item.ItemName == "Key2" || item.ItemName == "Key3" || item.ItemName == "Key4")
				{
					player.key_amount++;
                }
                if (amount == 0)
				{
					isEmpty = true;
					spriteRenderer.color = emptyColor;
				}
			}
			else
			{
				itemCopy.Destroy();
			}
		}
	
	}

	private void OnTriggerEnter(Collider other)
	{
		CheckCollision(other.gameObject, true);
	}

	private void OnTriggerExit(Collider other)
	{
		CheckCollision(other.gameObject, false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
	}

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			isInRange = state;
			spriteRenderer.enabled = state;
		}
	}

	public bool GetEmpty()
	{
		return isEmpty;
	}


    public void LoadData(GameData data)
    {

        data.chest1.TryGetValue(id, out isEmpty);
        if (isEmpty)
        {
            spriteRenderer.gameObject.SetActive(false);
        }
    }



    public void SaveData(ref GameData data)
    {
        if (data.chest1.ContainsKey(id))
        {
            data.chest1.Remove(id);
        }

        data.chest1.Add(id, isEmpty);

    }
}
