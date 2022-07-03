using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTabController : AbsTabController<ShopTabType,ShopTabView>
{
    public ShopTabController(List<GameObject> listGameObjects, List<TabInitInfo<ShopTabType>> tabInitInfos) : base(listGameObjects,tabInitInfos)
    {
    }
}
public enum ShopTabType
{
    Gold,
    Gem,
    Gacha,
}
