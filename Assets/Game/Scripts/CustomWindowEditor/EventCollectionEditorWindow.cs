using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_EDITOR
public class EventCollectionEditorWindow : OdinEditorWindow
{
    //[MenuItem("My Game/My Window")]
    public static object thisTarget;
    private static void OpenWindow()
    {
        GetWindow<EventCollectionEditorWindow>().Show();
    }
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        Object target = EditorUtility.InstanceIDToObject(instanceID);
 
        if (target is SerializedScriptableObject)
        {
            var path = AssetDatabase.GetAssetPath(instanceID);
            thisTarget = target;
//            if (Path.GetExtension(path) != ".watermesh") return false;
            InspectObject(thisTarget);
            //OpenWindow();
            Selection.activeObject = target;
            return true;
        }
 
        return false;
    }
    
//    [PropertyOrder(-10)]
//    [HorizontalGroup]
//    [Button(ButtonSizes.Large)]
//    public void SomeButton1() { }
//
//    [HorizontalGroup]
//    [Button(ButtonSizes.Large)]
//    public void SomeButton2() { }
//
//    [HorizontalGroup]
//    [Button(ButtonSizes.Large)]
//    public void SomeButton3() { }
//
//    [HorizontalGroup]
//    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
//    public void SomeButton4() { }
//
//    [HorizontalGroup]
//    [Button(ButtonSizes.Large), GUIColor(1, 0.5f, 0)]
//    public void SomeButton5() { }
//    
    

}
#endif
