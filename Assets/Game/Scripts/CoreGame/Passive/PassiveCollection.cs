using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveCollection", menuName = "CoreGame/PassiveCollection")]
public class PassiveCollection : SerializedScriptableObject
{
    [ListDrawerSettingsAttribute(ListElementLabelName = "name", ShowIndexLabels =false,ShowPaging = false,DraggableItems = false)]
    
    public List<BasePassive> Passives;
}
