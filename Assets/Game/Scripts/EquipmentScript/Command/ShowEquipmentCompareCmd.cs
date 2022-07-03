using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEquipmentCompareCmd : AbsShowPopupCmd
{
    [Inject] public ParameterEquipmentCompare ParameterEquipmentCompare { get; set; }

    public override void Execute()
    {
        EquipmentCompareView popup = GetInstance<EquipmentCompareView>();
        popup.ShowPopup(ParameterEquipmentCompare);
    }

    public override UILayer GetUiLayer()
    {
        return UILayer.UI2;
    }

    public override string GetResourcePath()
    {
        return GameResourcePath.POPUP_EQUIPMENT_COMPARE;
    }
}
