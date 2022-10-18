using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AutoAddComponent : MonoBehaviour
{
    public abstract bool AddComponent(GameEntity entity);
//    [Button("COPPY DATA", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
//    public virtual void CoppyData()
//    {
//        
//    }
}
