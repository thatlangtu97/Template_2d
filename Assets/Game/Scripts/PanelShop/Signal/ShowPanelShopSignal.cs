using strange.extensions.signal.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelShopSignal : Signal<ParameterPanelShop>
{
}

public class ParameterPanelShop : ParameterPopup
{
    public ShopTabType shopTabType;
    public ParameterPanelShop(){}

    public ParameterPanelShop(ShopTabType shopTabType)
    {
        this.shopTabType = shopTabType;
    }
}
