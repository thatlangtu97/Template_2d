
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class OpenSceneToolbar : MonoBehaviour
{
    [MenuItem("Open Scene/UI Dev &1")]
    public static void OpenSplashScreen()
    {
        OpenScene("UIDev");
    }
    [MenuItem("Open Scene/Entry &6")]
    public static void OpenEntry()
    {
        OpenScene("EntryScene");
    }
    [MenuItem("Open Scene/FlashScene &2")]
    public static void OpenFlashScene()
    {
        OpenScene("FlashScene");
    }
    [MenuItem("Open Scene/HomeScene &3")]
    public static void OpenHomeSceneOld()
    {
        OpenScene("HomeScene");
    }
    [MenuItem("Open Scene/Main &4")]
    public static void Main()
    {
        OpenScene("Main");
    }
    [MenuItem("Open Scene/TestBt &5")]
    public static void OpenTestBt()
    {
        OpenScene("TestBt");
    }
    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Game/Scenes/" + sceneName + ".unity");
        }
    }
    [MenuItem("Setup StateMachine/Remove All Pool &1")]
    public static void AutoRemovePool()
    {
         List<GameObject> prefabList = new List<GameObject>();
         prefabList  = new List<GameObject>(Resources.LoadAll<GameObject>("PrefabCharacter"));
         Debug.Log(prefabList.Count);
         foreach (var VARIABLE in prefabList)
         {
             PoolItem temp = VARIABLE.GetComponent<PoolItemStateMachine>();
             if (temp != null)
             { 
                 
                 Debug.Log($"destroy {VARIABLE.gameObject.name}");
             }

         }
    }
    [MenuItem("Setup StateMachine/Add All Pool &2")]
    public static void AutoAddPoolStateMachine()
    {
        List<GameObject> prefabList = new List<GameObject>();
        prefabList  = new List<GameObject>(Resources.LoadAll<GameObject>("PrefabCharacter"));
        Debug.Log(prefabList.Count);
        foreach (var VARIABLE in prefabList)
        {
            PoolItem temp = VARIABLE.GetComponent<PoolItemStateMachine>();
            if (temp == null)
            {
                
                PoolItemStateMachine tempAdd = VARIABLE.AddComponent<PoolItemStateMachine>();
                tempAdd.stateMachine = VARIABLE.GetComponent<StateMachineController>();
                Debug.Log($"Add {VARIABLE.gameObject.name}");
            }


        }
    }
}

#endif