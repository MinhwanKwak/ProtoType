using System;
using System.Numerics;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;

public interface IReadOnlyData : ICloneable { 
	void Create();
}
public interface IClientData { }


public class ItemData : IReadOnlyData
{
	public int ItemID { get; set; } = 0;
	public int Count { get; set; } = 0;

	public IReactiveProperty<int> CountProperty = new ReactiveProperty<int>();
	public virtual void Create() {
		CountProperty.Value = Count;
		CountProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			Count = x;
			//GameRoot.Instance.UserData.Save();
		});

	}
	public virtual object Clone()
	{
		var clone = new ItemData(){
			ItemID = ItemID,
			Count = Count
		};
		clone.Create();
		return clone;
	}
}




