using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    public GameEntity entity;
    public Collider2D collider2d;
    public ComponentManager component;
    public void Setup(GameEntity e)
    {
        entity = e;
        Collider2D col = e.stateMachineContainer.value.GetComponent<Collider2D>();
        if (collider2d != null)
        {
            Destroy(collider2d);
            Debug.Log("Destroy component before copy");
        }
        collider2d = CopyComponent(col, gameObject);
        gameObject.layer = col.gameObject.layer;

    }

    [Button("SETUP PARRY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void Test()
    {
        Setup(component.entity);
    }
//    T CopyComponent<T>(T original, GameObject destination) where T : Component
//    {
//        System.Type type = original.GetType();
//        Component copy;
//        if(destination.GetComponent<T>() == null)
//            copy = destination.AddComponent(type);
//        else
//        {
//            copy = destination.GetComponent<T>();
//        }
//        System.Reflection.FieldInfo[] fields = type.GetFields();
//        foreach (System.Reflection.FieldInfo field in fields)
//        {
//            field.SetValue(copy, field.GetValue(original));
//        }
//        return copy as T;
//    }
    
    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }
}
