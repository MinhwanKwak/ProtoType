using System.Collections;
using System;
using System.Numerics;
using System.Collections.Generic;
using UniRx;

public interface IUserDataMode
{
	// Config.Language Language {get; set;}= Config.Language.en;
	IReactiveProperty<BigInteger> Money { get; set; }
	IReactiveProperty<int> Material { get; set; }
	IReactiveCollection<FacilityData> FacilityDatas { get; set; }
	DateTime LastLoginTime { get; set; }
	DateTime CurPlayDateTime { get; set; }

	//int EventIdx {get; set;}
	//IReactiveProperty<BigInteger> EventQuestValue { get; set; }
	//IReactiveProperty<int> EventQuestLevel { get; set; }
	//int ReceiveEventQuestLevel { get; set; }
}

public class UserDataMain : IUserDataMode
{
	public IReactiveProperty<BigInteger> Money { get; set; } = new ReactiveProperty<BigInteger>(0);
	public IReactiveProperty<int> Material { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveCollection<FacilityData> FacilityDatas  { get; set; } = new ReactiveCollection<FacilityData>();
	public DateTime LastLoginTime { get; set; } = default(DateTime);
	public DateTime CurPlayDateTime { get; set; } = new DateTime(1, 1, 1);
}

public class UserDataEvent : UserDataMain
{
}