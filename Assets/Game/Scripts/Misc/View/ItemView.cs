using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Image imgIcon;
    public Image imgBorder;
    public Image imgBackGroundRarity;
    public Text txtValue;

    public void Show(AbsRewardLogic absRewardLogic)
    {
        imgIcon.sprite = absRewardLogic.Icon();
        imgBorder.color = absRewardLogic.ColorBorder();
        txtValue.text = absRewardLogic.ValueText();
        imgBackGroundRarity.sprite = absRewardLogic.BackGround();
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
