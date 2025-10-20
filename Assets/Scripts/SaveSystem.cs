using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using static Variable_Tracker;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class SaveSystem
{
    public static SaveData _saveData = new SaveData();
    public static GameObject Gamemanager;
    public static bool has_loaded;
    public static void EnsureGameManager()
    {
        if (Gamemanager == null)
        {
            Gamemanager = GameObject.Find("Game Manager");
        }

    }


    [System.Serializable]
    public struct SaveData
    {
        public VariableSaveData variableSave;
    }
    public static string SavefileName()
    {
        string Savefile = Application.persistentDataPath + "/savefile8" +".save";
        return Savefile;
    }
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SavefileName(), JsonUtility.ToJson(_saveData, true));
    }
    public static void HandleSaveData()
    {
        EnsureGameManager();
        Gamemanager.GetComponent<Variable_Tracker>().Save(ref _saveData.variableSave);
    }
    public static void Load()
    {
        if (!File.Exists(SavefileName()))
        {
            Debug.LogWarning("Save file not found, creating new save data.");
            _saveData = new SaveData();
            has_loaded = false;
            EnsureGameManager();
            Gamemanager.GetComponent<Variable_Tracker>().DefaultValues(_saveData.variableSave);
            return;
        }
        else
        {
            has_loaded = true;
        }

        string saveContent = File.ReadAllText(SavefileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    public static void HandleLoadData()
    {
        EnsureGameManager();
        Gamemanager.GetComponent<Variable_Tracker>().LoadData(_saveData.variableSave);
    }

    
   // [MenuItem("Tools/Clear Persistent Data")]
    public static void ClearData()
    {
        var path = Application.persistentDataPath;
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Debug.Log("Persistent data cleared.");
        }
        else
        {
            Debug.Log("No persistent data found.");
        }
    }
    
   
  
}
