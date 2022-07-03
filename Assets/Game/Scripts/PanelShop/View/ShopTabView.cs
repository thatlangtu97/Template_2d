using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabView :AbsTabView<ShopTabType>
{
    
    protected override void OnMapValue()
    {
    }

    public override void onChange(bool isSelect)
    {
        base.onChange(isSelect);
        foreach (var text in textTab)
        {
            if (isSelect)
            {
                text.color = new Vector4(text.color.r, text.color.g, text.color.b, 1f);
            }
            else
            {
                text.color = new Vector4(text.color.r, text.color.g, text.color.b, .5f);
            }
        }

    }
}
