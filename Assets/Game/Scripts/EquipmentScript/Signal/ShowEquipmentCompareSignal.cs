using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;

public class ShowEquipmentCompareSignal : Signal<ParameterEquipmentCompare>
{
}

public class ParameterEquipmentCompare : ParameterPopup
{
    public EquipmentData leftData;
    public EquipmentData rightData;
    public CompareEquipmentType compareType;
    public CompareEquipmentInfo compareInfo;
    
    public ParameterEquipmentCompare(){ }

    public ParameterEquipmentCompare(CompareEquipmentType compareType, CompareEquipmentInfo compareInfo ,EquipmentData data)
    {
        this.compareType = compareType;
        this.compareInfo = compareInfo;
        switch (compareType)
        {
            case CompareEquipmentType.Left:
                this.leftData = data;
                break;
            case CompareEquipmentType.Right:
                this.rightData = data;
                break;
        }
    }

    public void Clear()
    {
        leftData = null;
        rightData = null;
    }


}
public enum CompareEquipmentType
{
    Left,
    Right,
}
public enum CompareEquipmentInfo
{
    CharacterEquip,
    Craft,
}
