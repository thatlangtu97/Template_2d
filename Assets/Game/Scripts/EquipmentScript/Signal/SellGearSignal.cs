using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;

public class SellGearSignal : Signal<DataSellGear>
{
}

public class DataSellGear
{
    public List<EquipmentData> datas = new List<EquipmentData>();
    public int gearOfHero;
    public DataSellGear (){}

    public DataSellGear(EquipmentData equipment)
    {
        datas.Add(equipment);
        gearOfHero = equipment.idOfHero;
    }

    public DataSellGear(List<EquipmentData> equipments, int idOfHero)
    {
        datas = equipments;
        gearOfHero = idOfHero;
    }
}
