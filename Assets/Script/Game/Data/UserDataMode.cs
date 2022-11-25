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
	List<FacilityData> FacilityDatas { get; set; }

	//int EventIdx {get; set;}
	//IReactiveProperty<BigInteger> EventQuestValue { get; set; }
	//IReactiveProperty<int> EventQuestLevel { get; set; }
	//int ReceiveEventQuestLevel { get; set; }
}

public class UserDataMain : IUserDataMode
{
	public IReactiveProperty<BigInteger> Money { get; set; } = new ReactiveProperty<BigInteger>(0);
	public IReactiveProperty<int> Material { get; set; } = new ReactiveProperty<int>(0);
	public List<FacilityData> FacilityDatas  { get; set; } = new List<FacilityData>();
}

public class UserDataEvent : UserDataMain
{
}