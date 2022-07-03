using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;


public class AddRewardFromItemSignal:Signal<AddRewardParameter>
{
		
}
public class AddRewardParameter
{
	public List<AbsRewardLogic> ItemInfos = new List<AbsRewardLogic>();
	public Action finish = delegate {  };
	public bool showPopup;
	public Action<List<AbsRewardLogic>> rewardGenerated = delegate(List<AbsRewardLogic> list) {  };
	
	public AddRewardParameter(List<AbsRewardLogic> ItemInfos, Action finish, bool showPopup)
	{
		this.finish = finish;
		this.showPopup = showPopup;
		this.ItemInfos = ItemInfos;

	}
		
	public AddRewardParameter(AbsRewardLogic itemLogic, Action finish, bool showPopup)
	{
		this.finish = finish;
		this.showPopup = showPopup;
		ItemInfos.Add(itemLogic);
			
	}
	public AddRewardParameter ListenRewardAfterGenerate(Action<List<AbsRewardLogic>> rewardGenerated)
	{
		this.rewardGenerated += rewardGenerated;
		return this;
	}

//	public void RemoveItemEmpty()
//	{
//		List<AbsRewardLogic> remove = new List<AbsRewardLogic>();
//		foreach (var item in ItemInfos)
//		{
//			if (item.Quantity() <= 0) remove.Add(item);
//		}
//
//		foreach (var item in remove)
//		{
//			ItemInfos.Remove(item);
//		}
//	}
}