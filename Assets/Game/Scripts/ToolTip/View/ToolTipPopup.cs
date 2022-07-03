using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPopup : AbsPopupView
{
    public Transform itemViewContainer;
    public Button btnClose;
    public RectTransform parent;
    
    public ItemView itemView;
    public Vector3 ScreenSize;
    protected override void Awake()
    {
        base.Awake();
        GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_VIEW);
        itemView = Instantiate(prefab, itemViewContainer).GetComponent<ItemView>();
        itemView.transform.localPosition=Vector3.zero;
        btnClose.onClick.AddListener(Hide);
        parent.gameObject.SetActive(false);
    }


    public override bool EnableBack()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        ToolTipPopupParameter parameterPopup  = parameter as ToolTipPopupParameter;
        parent.gameObject.SetActive(false);
        itemView.Show(parameterPopup.rewardLogic);
        ScreenSize =  transform.position;
        SetupPositionFolowScreen(parameterPopup.position);

        ActionBufferManager.Instance.ActionDelayFrame(()=>
            {
                parent.transform.position = parameterPopup.position;
                parent.gameObject.SetActive(true);
            }
            ,1);
    }

    public void SetupPositionFolowScreen(Vector3 position)
    {
        if (position.x >= ScreenSize.x)
        {
            if (position.y >= ScreenSize.y)
            {
                parent.pivot = new Vector2(1,1);
            }
            else
            {
                parent.pivot = new Vector2(1,0);
            }
        }
        else
        {
            if (position.y >= ScreenSize.y)
            {
                parent.pivot = new Vector2(0,1);
                
            }
            else
            {
                parent.pivot = new Vector2(0,0);
            }
        }
    }
    
}
