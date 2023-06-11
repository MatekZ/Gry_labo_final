using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.IO;
public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	public static string ID;

	public static string  getid(string id)
	{
		ID = id;	
		return ID;
	}

    static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveItems(ItemContainerSaveData items, string path)
	{
		FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + ID + "/" + path + ".dat", items);
      
    }

	public static ItemContainerSaveData LoadItems(string path)
	{
       
        string filePath = baseSavePath + "/" + ID + "/" + path + ".dat";
		
       
        if (System.IO.File.Exists(filePath))
		{
			return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
		}
		return null;
	}


	public static void DeleteFile()
	{
        string filePath = baseSavePath + "/" + ID + "/" + "Inventory.dat";
		if(File.Exists(filePath))
		{
            File.Delete(filePath);
        }
        

        string filePath1 = baseSavePath + "/" + ID + "/" + "Equipment.dat";
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }


    }
}
