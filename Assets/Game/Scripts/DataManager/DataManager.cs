﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{


    public const string DATA_PATH = "Game/Data/";
    private static DataManager instance;
    private List<IObjectDataManager> listObjectDataManager;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject dataManager = new GameObject();
                dataManager.name = "DataManager";
                instance = dataManager.AddComponent<DataManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        listObjectDataManager = new List<IObjectDataManager>();
    }
    public static void DeleteData(string fileName)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public static T LoadData<T>(string fileName) where T : DataObject
    {
        T obj = default(T);
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, FileMode.Open);
            obj = formatter.Deserialize(fileStream) as T;
            fileStream.Close();
        }
        return obj;
    }
    public static void SaveData(DataObject obj, string fileName)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (obj != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath, File.Exists(filePath) ? FileMode.Open : FileMode.Create);
            binaryFormatter.Serialize(fileStream, obj);
            fileStream.Close();
        }
    }
}
public interface IObjectDataManager
{
    void LoadData();

    void SaveData();

    void DeleteData();
}
[System.Serializable]
public class DataObject
{

}