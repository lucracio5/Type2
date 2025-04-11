using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Variable_Tracker;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class SaveSystem
{   
    public static SaveData _saveData = new SaveData();
    public static GameObject Gamemanager;
    public void Start()
    {
        Gamemanager = GameObject.Find("Game Manager");
    }

    [System.Serializable]
    public struct SaveData
    {
        public VariableSaveData variableSave;
    }
    public static string SavefileName()
    {
        string Savefile = Application.persistentDataPath + "/save" +".save";
        return Savefile;
    }
    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SavefileName(), JsonUtility.ToJson(_saveData, true));
    }
    public static void HandleSaveData()
    {
        Gamemanager.GetComponent<Variable_Tracker>().Save(ref _saveData.variableSave);
    }
    public static void Load()
    {
        string saveContent = File.ReadAllText(SavefileName());

        _saveData = _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        //JsonUtility.FromJson<VariableSaveData>(saveContent);
        HandleLoadData();
    }
    public static void HandleLoadData()
    {
        Gamemanager.GetComponent<Variable_Tracker>().LoadData(_saveData.variableSave);
    }
    private void Update()
    {
        if (Gamemanager.GetComponent<Variable_Tracker>().save)
        {
            SaveSystem.Save();
        }
        if (Gamemanager.GetComponent<Variable_Tracker>().load)
        {
            SaveSystem.Load();
        }
    }
  
}
