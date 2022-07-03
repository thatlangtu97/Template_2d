using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ScriptableObjectData
{
    private static readonly string FOLDER = "ScriptableObjectData/";
    
    public static T Load<T>(string path) where T : ScriptableObject
    {
        return Resources.Load<T>(path);
    }
}
