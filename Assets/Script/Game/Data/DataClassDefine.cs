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

public class EventItemData : ItemData {
	public override object Clone()
	{
		var clone = new EventItemData(){
			ItemID = ItemID,
			Count = Count
		};
		clone.Create();
		return clone;
	}
}


public class HeroData : IReadOnlyData
{
	public int Idx  = 0;
	public int Lv = 0;
	public int Exp = 0;

	public IReactiveProperty<int> LvProperty = new ReactiveProperty<int>(0);

	public HeroData(int heroidx , int lv , int exp)
    {
		Idx = heroidx;
		Lv = lv;
		Exp = exp;
		Create();
	}


	public virtual void Create()
	{
		LvProperty.Value = Lv;
		LvProperty.Subscribe(x => { Lv = x; });
	}
	public virtual object Clone()
	{
		var clone = new HeroData(Idx, Lv, Exp);
		clone.Create();
		return clone;
	}
}

public class HeroSkillData : IReadOnlyData
{
	public int SkillIdx = 0;
	public DateTime SkillCoolTime;
	public DateTime SkillMaintainTime;



	public IReactiveProperty<int> SkillCoolTimeProperty = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> SkillRetentionProperty = new ReactiveProperty<int>(0);

	public HeroSkillData(int skillidx, long skillcooltime , long skillmaintaintime)
	{
		SkillIdx = skillidx;
		SkillCoolTime = new DateTime(skillcooltime);
		SkillMaintainTime = new DateTime(skillmaintaintime);
	}


	public virtual void Create()
	{
	}
	public virtual object Clone()
	{
		var clone = new HeroSkillData(SkillIdx , SkillCoolTime.Ticks , SkillMaintainTime.Ticks);
		clone.Create();
		return clone;
	}
}


public class EventData : IClientData
{
	
	public IReactiveProperty<BigInteger> QuestValue = new ReactiveProperty<BigInteger>();
	public IReactiveProperty<int> QuestLevel = new ReactiveProperty<int>();
	public int ReceiveLevel { get; set; } = 0;
	public int PremiumReceiveLevel { get; set; } = 0;
	public RouletteData RouletteData;
	public EventStatusData EventStatusData = new EventStatusData(0 ,0, false , 0 , 0);
	public IReactiveCollection<EventSpecialProductData> EventSpecialProductList = new ReactiveCollection<EventSpecialProductData>(); 

	public void SetEventIdx(int idx)
    {
		QuestValue.Value = 0;
		QuestLevel.Value = 0;
		ReceiveLevel = 0;
		PremiumReceiveLevel = 0;
		EventSpecialProductList.Clear();

		//RouletteData.Init(-1);
	}

	public void ReceiveEventReward()
    {
		ReceiveLevel = QuestLevel.Value;

		//GameRoot.Instance.UserData.Save();
    }

	public void ReceivePremiumEventReward()
    {
		PremiumReceiveLevel = QuestLevel.Value;

		//GameRoot.Instance.UserData.Save();
	}
}

/*[Serializable]
public class EventRouletteData : IReadOnlyData
{
	public int RouletteIdx { get; set; } = 0;
	public int Level { get; set; } = 1;

	public int Manager { get; set; } = -1;
	public int Manager2 { get; set; } = -1;
	public int Manager3 { get; set; } = -1;

	public int RouletteCnt { get; set; } = 0;
	public int JackPotCheckCnt { get; set; } = 0;

	public IReactiveProperty<int> LevelProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> ManagerProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> Manager2Property = new ReactiveProperty<int>();
	public IReactiveProperty<int> Manager3Property = new ReactiveProperty<int>();
	public IReactiveProperty<int> RouletteCntProperty = new ReactiveProperty<int>();

	public void SetIdx(int idx) { RouletteIdx = idx; }
	public void Init(int idx)
    {
		SetIdx(idx);

		Level = 1;
		Manager = -1;
		Manager2 = -1;
		Manager3 = -1;
		RouletteCnt = 0;
		JackPotCheckCnt = 0;

		LevelProperty.Value = 1;

		RouletteCntProperty.Value = 0;
		ManagerProperty.Value = -1;
		Manager2Property.Value = -1;
		Manager3Property.Value = -1;

	}
	public void IncreaseLevel()
	{
		// var FacilityInfo = Tables.Instance.GetTable<FacilityInfo>().GetData(RouletteIdx);

		// var data = Tables.Instance.GetTable<RouletteUpgrade>().GetData(LevelProperty.Value);
		// var curUpgradeData = new StageUpgradeAlreadyData()
		// {
		// 	level = 1,
		// 	nextLevel = 2,
		// 	ProductionSpeed = (float)data.speed,
		// 	UpgradePrice = data.cost,
		// 	ObjectValue = data.value
		// };
		//curUpgradeData = GameRoot.Instance.BuffManager.GetRouletteBuffData(RouletteIdx, curUpgradeData);

		// var price = curUpgradeData.UpgradePrice;
		// var haveMoney = GameRoot.Instance.UserData.CurMode.Money.Value;
		// if (price <= haveMoney)
		// {
			//GameRoot.Instance.UserData.ConsumMoney(price);
		// 	LevelProperty.Value = Level + 1;

		// 	RouletteCntProperty.Value = 0;
		// }
		// else
		// {
			// GameRoot.Instance.UISystem.OpenUI<ToastMessagePopup>(ui => {
			// 	ui.SetData(Tables.Instance.GetTable<Localize>().GetString("str_toast_title_nomoney"),
			// 		Tables.Instance.GetTable<Localize>().GetString("str_toast_desc_nomoney"));
			// });
		// }

		// GameRoot.Instance.UserData.Save(SaveDataType.EventRouletteData);
	}
	public void EquipManager(int idx, int manager)
	{
		if (idx.Equals(1)) ManagerProperty.Value = manager;
		else if (idx.Equals(2)) Manager2Property.Value = manager;
		else Manager3Property.Value = manager;

		//GameRoot.Instance.NotificationSystem.CheckNotification(GameNotificationSystem.NotificationCategory.CardDeployPossible);
		//GameRoot.Instance.NotificationSystem.CheckNotification(GameNotificationSystem.NotificationCategory.EquipCardLevelupPossible);

	}
	public void UnEquipManager(int idx)
	{
		if (idx.Equals(1)) ManagerProperty.Value = -1;
		else if (idx.Equals(2)) Manager2Property.Value = -1;
		else Manager3Property.Value = -1;

		//GameRoot.Instance.NotificationSystem.CheckNotification(GameNotificationSystem.NotificationCategory.CardDeployPossible);
		//GameRoot.Instance.NotificationSystem.CheckNotification(GameNotificationSystem.NotificationCategory.EquipCardLevelupPossible);

	}
	public void Create()
	{
		LevelProperty.Value = Level;
		LevelProperty.Subscribe(x => { Level = x; });

		ManagerProperty.Value = Manager;
		ManagerProperty.Subscribe(x => { Manager = x; });
		Manager2Property.Value = Manager2;
		Manager2Property.Subscribe(x => { Manager2 = x; });
		Manager3Property.Value = Manager3;
		Manager3Property.Subscribe(x => { Manager3 = x; });
		RouletteCntProperty.Value = RouletteCnt;
		RouletteCntProperty.Subscribe(x => { RouletteCnt = x; });

	}

	public object Clone()
	{
		return new EventRouletteData();
	}
}*/

public class RouletteData : IReadOnlyData
{
	public FacilityData facilityData;
	public int RunCount { get; protected set; } = 0;
	public ReactiveProperty<int> RunCountProperty = new ReactiveProperty<int>();
	public ReactiveProperty<BigInteger> RouletteCoin = new ReactiveProperty<BigInteger>();

	public RouletteData(FacilityData data, int runCnt, BigInteger rouletteCoin)
    {
		facilityData = data;
		RunCount = runCnt;
		RouletteCoin.Value = rouletteCoin;

		Create();
    }
	public void Create()
	{
		RunCountProperty.Value = RunCount;
		RunCountProperty.Subscribe(x => { RunCount = x; });
	}
	public void IncreaseRunCount()
    {
		RunCountProperty.Value++;
	}
	public void ResetRunCount()
    {
		RunCountProperty.Value = 0;
    }
	public object Clone()
	{
		return new RouletteData(facilityData, RunCount, RouletteCoin.Value);
	}
}

public class FacilityData : IReadOnlyData
{
	public int FacilityIndex { get; protected set; } = 0;
	public bool IsOpend { get; protected set; } = false;
	public int SlotCount { get; protected set; } = 1;
	public int Level { get; protected set; } = 1;
	public int Manager1 { get; protected set; } = -1;
	public int Manager2 { get; protected set; } = -1;
	public int Manager3 { get; protected set; } = -1;

	public int WaitingGuest { get; set; } = 0;

	public IReactiveProperty<int> LevelProperty = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> Manager1Property = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> Manager2Property = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> Manager3Property = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> SlotCountProperty = new ReactiveProperty<int>();

	public IReactiveProperty<bool> AutoProperty = new ReactiveProperty<bool>();

	public IReactiveProperty<bool> OpenProperty = new ReactiveProperty<bool>();

	public IReactiveProperty<int> CostumeProperty = new ReactiveProperty<int>();
	public FacilityData(int idx, bool opend, int slotCnt, int lv, int manager1, int manager2, int manager3,int waitingguest)
	{
		FacilityIndex = idx;
		IsOpend = opend;
		SlotCount = slotCnt;
		Level = lv;
		Manager1 = manager1;
		Manager2 = manager2;
		Manager3 = manager3;
		WaitingGuest = waitingguest;
		Create();
	}

	public void SetIdx(int idx) { FacilityIndex = idx; }
	public void SetOpend(bool value)
	{
		OpenProperty.Value = value;
	}
	public void AddSlotCount()
	{
		++SlotCountProperty.Value;
	}
	public void IncreaseLevel(int lv)
	{
		LevelProperty.Value = lv;
	}
	public void SetCostume(int idx)
    {
		CostumeProperty.Value = idx;
    }

	public int GetManagerIdx(int idx)
	{
		if (idx.Equals(0)) 
			return Manager1;
		else if (idx.Equals(1))
			return Manager2;
		else 
			return Manager3;
	}

	public void EquipManager(int idx, int manager)
	{
		if (idx.Equals(0)) 
			Manager1Property.Value = manager;
		else if (idx.Equals(1)) 
			Manager2Property.Value = manager;
		else 
			Manager3Property.Value = manager;
	}
	public void UnEquipManager(int idx)
	{
		if (idx.Equals(0)) 
			Manager1Property.Value = -1;
		else if (idx.Equals(1)) 
			Manager2Property.Value = -1;
		else 
			Manager3Property.Value = -1;
	}


	public void Create()
	{
		LevelProperty.Value = Level;
		LevelProperty.Subscribe(x => { Level = x; });

		Manager1Property.Value = Manager1;
		Manager1Property.Subscribe(x => { Manager1 = x; });
		Manager2Property.Value = Manager2;
		Manager2Property.Subscribe(x => { Manager2 = x; });
		Manager3Property.Value = Manager3;
		Manager3Property.Subscribe(x => { Manager3 = x; });

		SlotCountProperty.Value = SlotCount;
		SlotCountProperty.Subscribe(x => { SlotCount = x; });

		OpenProperty.Value = IsOpend;
		OpenProperty.Subscribe(x => { IsOpend = x; });

	}

	public virtual object Clone()
	{
		var clone = new FacilityData(FacilityIndex, IsOpend, SlotCount, Level, Manager1, Manager2, Manager3, WaitingGuest);
		return clone;
	}
}

[Serializable]
public class BookData : IReadOnlyData
{
	public int StageIdx { get; set; } = 0;
	public bool isComplete { get; set; } = false;
	public bool isReceive { get; set; } = false;

	public void Create(){}
	public void Create(int stageIdx)
    {
		StageIdx = stageIdx;
		isComplete = false;
		isReceive = false;
    }

	public object Clone()
	{
		return new BookData();
	}
}

public class DropBoxData : IReadOnlyData
{
	public int MonsterKillCount = 0;
	public IReactiveProperty<int> BoxCount = new ReactiveProperty<int>(0);

	public void Create() { }

	public object Clone()
    {
		return new DropBoxData
		{
			MonsterKillCount = MonsterKillCount,
			BoxCount = BoxCount
		};
    }
}

public class ManagerData : IReadOnlyData
{
	
	public int ManagerIdx { get; protected set; } = 0;
	public int ManagerCount { get; protected set; } = 0;
	public int ManagerLv { get; protected set; } = 0;

	public DateTime CreateItemTime { get; protected set; }

	
	public IReactiveProperty<int> ManagerLvProperty = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> ManagerCntProperty = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>();

	public ManagerData(int idx, int cnt, int lv, long createTime)
	{
		ManagerIdx = idx;
		ManagerCount = cnt;
		ManagerLv = lv;
		CreateItemTime = new System.DateTime(createTime);
		Create();
	}

	
	public void EarnManagerCard(int cnt)
	{
		ManagerCntProperty.Value += cnt;
	}
	public virtual void Create()
	{		
		ManagerLvProperty.Value = ManagerLv;
		ManagerLvProperty.Subscribe(x => { ManagerLv = x; });

		ManagerCntProperty.Value = ManagerCount;
		ManagerCntProperty.Subscribe(x => ManagerCount = x);
	}

	public virtual object Clone()
	{
		var clone = new ManagerData(ManagerIdx, ManagerCount, ManagerLv, CreateItemTime.Ticks);
		return clone;
	}
}



//public class EventHireCardData : IReadOnlyData
//{
//	public DateTime creatTime { get; set; }
//	public DateTime freeCardTime { get; set; }
//	public int refreshCnt { get; set; }
//	public int cardIdx { get; set; }
//	public bool isLock { get; set; }
//	public bool isAdWatch { get; set; }

//	public IReactiveProperty<int> FCRemainTimeProperty = new ReactiveProperty<int>();

//	public virtual void Create()
//    {
//	}

//	public	EventHireCardData(long _createTime, bool _lock, bool _isAdWatch , int _cardidx)
//	{
		
//		creatTime = new DateTime(_createTime);
//		isLock = _lock;
//		cardIdx = _cardidx;
//		isAdWatch = _isAdWatch;
//		Create();
//	}

//	public virtual object Clone()
//	{
//		var clone = new EventHireCardData(creatTime.Ticks,  isLock, isAdWatch ,  cardIdx);
//		return clone;
//	}
//}

public class HireCardData : IReadOnlyData
{
	public DateTime InitHireCreateTime { get; set; }
	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>();
	public int AdCount { get; set; } = 3;
	public int GemBuyCount { get; set; } = 1;
 	public List<int> hireManagerIdxList = new List<int>(3);
	public List<int> hireManagerGemPrice = new List<int>(3);



	public virtual void Create()
	{
		hireManagerIdxList.Clear();
		hireManagerGemPrice.Clear();
		InitHireCreateTime = default(DateTime);
		GemBuyCount = 1;
		if (hireManagerIdxList.Count <= 0)
		{
			for (int i = 0; i < 3; ++i)
			{
				hireManagerIdxList.Add(0);
				hireManagerGemPrice.Add(0);
			}
		}
	}

	public virtual object Clone()
	{
		var clone = new HireCardData();
		clone.Create();
		return clone;
	}
}


public class PackageData : IReadOnlyData
{
	
	public int PackageIdx { get; set; } = 0;
	public int PackagedRewardGroup { get; set; } = 0;
	public DateTime StartTime { get; set; }
	public int BuyCount { get; set; } = 0;
	public int OpenFirstCount { get; set; } = 0;
	public int OpenSecondCount { get; set; } = 0;


	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>();
	
	public IReactiveProperty<int> BuyCntProperty = new ReactiveProperty<int>();
	
	public PackageData(int idx, int packagereward , long datetime , int buycount , int openfirstcount , int opensecondcount)
	{
		PackageIdx = idx;
		PackagedRewardGroup = packagereward;
		StartTime = new DateTime(datetime);
		BuyCount = buycount;
		OpenFirstCount = openfirstcount;
		OpenSecondCount = opensecondcount;

		Create();
	}
	public virtual void Create()
	{
		BuyCntProperty.Value = BuyCount;
		BuyCntProperty.Subscribe(x => {BuyCount = x;});
	}

	public virtual object Clone()
	{
		var clone = new PackageData(PackageIdx, PackagedRewardGroup, StartTime.Ticks, BuyCount, OpenFirstCount , OpenSecondCount);
		return clone;
	}
}


public class EventMainRewardData : IReadOnlyData
{
	public int MissionOrder { get; set; } = 0;
	public int MissionIdx { get; set; } = 0;
	public int RewardType { get; set; } = 0;
	public int RewardIdx { get; set; } = 0;
	public int RewardCount { get; set; } = 0;

	public EventMainRewardData(int missionorder , int missionidx,int type, int idx, int count)
	{
		MissionOrder = missionorder;
		MissionIdx = missionidx;
		RewardType = type;
		RewardIdx = idx;
		RewardCount = count;
	}
	public virtual void Create()
	{
	}

	public virtual object Clone()
	{
		var clone = new EventMainRewardData(MissionOrder,MissionIdx,RewardType,  RewardIdx , RewardCount);
		return clone;
	}
}


public class EventPassRewardData : IReadOnlyData
{
	public int MissionOrder { get; set; } = 0;
	public int MissionIdx { get; set; } = 0;
	public int RewardType { get; set; } = 0;
	public int RewardIdx { get; set; } = 0;
	public int RewardCount { get; set; } = 0;


	public EventPassRewardData(int order , int missionidx,int rewardtype, int rewardidx, int rewardcount)
	{
		MissionOrder = order;
		MissionIdx = missionidx;
		RewardType = rewardtype;
		RewardIdx = rewardidx;
		RewardCount = rewardcount;
	}
	public virtual void Create()
	{
	}

	public virtual object Clone()
	{
		var clone = new EventPassRewardData(MissionOrder , MissionIdx ,RewardType , RewardIdx , RewardCount	);
		return clone;
	}
}


//public class AdflySkinChangeData : IReadOnlyData
//{
//	public int EventIdx { get; set; } = 0;
//	public int SkinIdx { get; set; } = 0;
//	public int Level { get; set; } = 0;
//	public DateTime RewardBuffCoolTime { get; set; }
//	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>(0);



//	public AdflySkinChangeData(int eventidx , int skinidx , int level , long buffcooltime)
//    {
//		EventIdx = eventidx;
//		SkinIdx = skinidx;
//		Level = level;
//		RewardBuffCoolTime = new DateTime(buffcooltime);
//    }

//	public virtual void Create()
//    {

//    }

//	public virtual object Clone()
//    {
//		var clone = new AdflySkinChangeData(EventIdx, SkinIdx, Level, RewardBuffCoolTime.Ticks);
//		return clone;
//    }

//}



public class EquipAdflySkinChangeData : IReadOnlyData
{
	public int EventIdx { get; set; } = 0;
	public int SkinIdx { get; set; } = 0;

	public IReactiveProperty<int> EventIdxProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> SkinIdxProperty = new ReactiveProperty<int>();



	public EquipAdflySkinChangeData(int eventidx, int skinidx)
	{
		EventIdx = eventidx;
		SkinIdx = skinidx;
	}

	public virtual void Create()
	{
		EventIdxProperty.Value = EventIdx;
		SkinIdxProperty.Value = SkinIdx;

		EventIdxProperty.SkipLatestValueOnSubscribe().Subscribe(x =>
		{
			EventIdx = x;
		});

		SkinIdxProperty.SkipLatestValueOnSubscribe().Subscribe(x =>
		{
			SkinIdx = x;
		});
	}

	public virtual object Clone()
	{
		var clone = new EquipAdflySkinChangeData(EventIdx, SkinIdx);
		clone.Create();
		return clone;
	}
}




public class FacilitySkinChangeData : IReadOnlyData
{
	public int EventIdx { get; set; } = 0;
	public int SkinIdx { get; set; } = 0;


	public FacilitySkinChangeData(int eventidx, int skinidx)
	{
		EventIdx = eventidx;
		SkinIdx = skinidx;
	}

	public virtual void Create()
	{

	}

	public virtual object Clone()
	{
		var clone = new FacilitySkinChangeData(EventIdx, SkinIdx);
		return clone;
	}

}


public class SkinChangeData: IReadOnlyData
{
	public IReactiveCollection<FacilitySkinChangeData> FacilityEventSkinList = new ReactiveCollection<FacilitySkinChangeData>();
	public IReactiveCollection<FacilitySkinChangeData> EquipFacilityEventSkiList = new ReactiveCollection<FacilitySkinChangeData>();
	public EquipAdflySkinChangeData EquipAdFlySkinChange = new EquipAdflySkinChangeData( 0 , 0);


	public void SetEquipAdflySkin(int eventidx , int skinidx)
    {
		EquipAdFlySkinChange.EventIdx = eventidx;
		EquipAdFlySkinChange.SkinIdx = skinidx;
		EquipAdFlySkinChange.EventIdxProperty.Value = eventidx;
		EquipAdFlySkinChange.SkinIdxProperty.Value = skinidx;
    }

	public virtual void Create()
    {
		FacilityEventSkinList.Clear();
		EquipFacilityEventSkiList.Clear();
	}

	public virtual object Clone()
    {
		var clone = new SkinChangeData();
		clone.Create();
		return clone;
    }
}

public class EnergyData : IReadOnlyData
{
	public int EnergyIdx { get; set; } = 0;
	public int EnergyOrder { get; set; } = 0;
	public DateTime EnergyEventCoolTime { get; set; }
	public DateTime EnergyEventLimitTime { get; set; }
	public IReactiveCollection<int> EnergyClearOrderList = new ReactiveCollection<int>();
	public List<int> EnergyRewardCountList = new List<int>();
	public int BeforeEnergyIdx { get; set; } = 0;
	public bool ViewDoublePass2Popup { get; set; } = false;

	public List<EnergyClearDatas> ClearDataList = new List<EnergyClearDatas>();


	public virtual void Create()
	{
		EnergyIdx = 0;
		EnergyOrder = 0;
		EnergyEventCoolTime = default(DateTime);
		EnergyEventLimitTime = default(DateTime);
		ClearDataList.Clear();
		EnergyClearOrderList.Clear();
		EnergyRewardCountList.Clear();
		BeforeEnergyIdx = 0;
		ViewDoublePass2Popup = false;
	}

	public virtual object Clone()
	{
		var clone = new EnergyData();
		clone.Create();
		return clone;
	}
}

public class EnergyClearDatas
{
	public int EnergyIdx = 0;
	public DateTime EnergyClearTime;
	public int EnergyOrder = 0;
	public bool DoublePass = false;

	public EnergyClearDatas(int energyidx , long datetime, int energyorder, bool doublepass)
    {
		EnergyIdx = energyidx;
		EnergyClearTime = new DateTime(datetime);
		EnergyOrder = energyorder;
		DoublePass = doublepass;
    }


}


public class EventPackageData : IReadOnlyData
{
	public int PackageIdx { get; set; } = 0;
	public int PackagedRewardGroup { get; set; } = 0;
	public DateTime StartTime { get; set; }
	public int BuyCount { get; set; } = 0;


	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> BuyCntProperty = new ReactiveProperty<int>();
	public  void Create()
	{
		BuyCntProperty.Value = BuyCount;
		BuyCntProperty.Subscribe(x => {BuyCount = x; });
	}
	 
	public EventPackageData(int idx, int packagereward, long datetime, int buycount)
	{
		PackageIdx = idx;
		PackagedRewardGroup = packagereward;
		StartTime = new DateTime(datetime);
		BuyCount = buycount;
		Create();
	}


	public  object Clone()
	{
		var clone = new EventPackageData(PackageIdx, PackagedRewardGroup, StartTime.Ticks, BuyCount);
		return clone;
	}
}

[Serializable]
public class DailyMissionData : IReadOnlyData
{ 
	public DateTime ResetTime { get; set; }
	public List<QuestData> MissionDatas = new List<QuestData>();

	public void Create()
    {
		ResetTime = default(DateTime);
		MissionDatas.Clear();

	}

	public virtual object Clone()
    {
		var clone = new DailyMissionData()
		{
			ResetTime = ResetTime,
			MissionDatas = MissionDatas
		};
		clone.Create();

		return clone;
    }
}


[Serializable]
public class MainPassData : IReadOnlyData
{
	public List<QuestData> MainDatas = new List<QuestData>();

	public void Create()
	{
		MainDatas.Clear();

	}


	public virtual object Clone()
	{
		var clone = new MainPassData()
		{
			MainDatas = MainDatas
		};
		clone.Create();

		return clone;
	}
}

[Serializable]
public class AchievementData : IReadOnlyData
{
	public int Index { get; set; } = 0;
	public string Value { get; set; } = "0";
	public int ReceiveLevel { get; set; } = 0;

	public IReactiveProperty<BigInteger> ValueProperty = new ReactiveProperty<BigInteger>();
	public IReactiveProperty<int> ReceiveProperty = new ReactiveProperty<int>();

	public void Create()
    {
		ValueProperty.Value = BigInteger.Parse(Value);
		ReceiveProperty.Value = ReceiveLevel;
		ValueProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			Value = x.ToString();
		});
		ReceiveProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			ReceiveLevel = x;
		});
	}

	public void ClearAchievement()
    {
		ReceiveProperty.Value += 1;
    }

	public virtual object Clone()
	{
		var clone = new AchievementData()
		{
			Index = Index,
			Value = Value,
			ReceiveLevel = ReceiveLevel
		};
		clone.Create();
		return clone;
	}
}

public class AttendanceData : IReadOnlyData
{
	public int AttendanceIdx = -1;
	public int AttendanceOrder = -1;
	public DateTime LastAttendanceTime = default(DateTime);

	public virtual void Create()
    {

    }

	public virtual object Clone()
    {
		var clone = new AttendanceData()
		{
			AttendanceIdx = AttendanceIdx,
			AttendanceOrder = AttendanceOrder,
			LastAttendanceTime = LastAttendanceTime
		};
		clone.Create();
		return clone;
	}
}

[Serializable]
public class StageClearData : IReadOnlyData
{
	public List<int> ItemIdxs = new List<int>();
	public int Stageidx { get; set; } = 0;

	public DateTime CreateClearTime { get; protected set; }

	public IReactiveProperty<int> RemainTimeProperty = new ReactiveProperty<int>(0);

	public StageClearData(List<int> _itemidxs , int _stageidx , long _createTime)
    {
		for(int i = 0; i < _itemidxs.Count; ++i)
        {
			ItemIdxs.Add(_itemidxs[i]);
        }
		Stageidx = _stageidx;
		CreateClearTime = new DateTime(_createTime);

	}

	public virtual void Create()
    {
    }

	public virtual object Clone()
	{
		var clone = new StageClearData(ItemIdxs, Stageidx, CreateClearTime.Ticks);
		return clone;
	}
}


[Serializable]
public class AdFlySkinData : IReadOnlyData
{
	public List<int> AdFlySkinIdxs = new List<int>();
	public int Level { get; set; } = 1;
	public int CurEquipAdflyIdx { get; set; } = 0;

	public IReactiveProperty<int> LvProperty = new ReactiveProperty<int>(0);

	CompositeDisposable disposables = new CompositeDisposable();
	public virtual void Create()
	{
		disposables.Clear();

		LvProperty.Value = Level;
		

		LvProperty.Subscribe(x => {
			Level = x;
		}).AddTo(disposables);
	}	
	

	public virtual object Clone()
	{
		var clone = new AdFlySkinData();
		return clone;
	}
}



[Serializable]
public class DragonHuntData : IReadOnlyData
{
	
	public int Hp { get; set; } = 0;
	public int CurGrade { get; set; } = 0;
	public IReactiveCollection<int> DragonHuntRewardIndexsList = new ReactiveCollection<int>();

	public virtual void Create()
	{
		//disposables.Clear();

		//HpProperty.Value = Hp;


		//HpProperty.Subscribe(x => {
		//	Hp = x;
		//	Save();
		//}).AddTo(disposables);
	}




	public virtual object Clone()
	{
		var clone = new DragonHuntData();
		return clone;
	}
}


[Serializable]
public class ProductData : IReadOnlyData
{
	public List<FacilityMoneyData> FacilityMoneyList = new List<FacilityMoneyData>();
	public int FacilityOrder { get; set; } = 0;
	public float PosX { get; set; } = 0f;
	public float PosY { get; set; } = 0f;




	public void Create()
	{

	}

	public ProductData(List<FacilityMoneyData> facilitymoneylist , int facilityorder , float posx , float posy)
	{
		FacilityMoneyList = facilitymoneylist;
		FacilityOrder = facilityorder;
		PosX = posx;
		PosY = posy;
	}

	public virtual object Clone()
	{

		var clone = new ProductData(FacilityMoneyList , FacilityOrder, PosX , PosY);
		return clone;
	}
}



[Serializable]
public class FacilityMoneyData
{
	public int SlotOrder { get; set; } = 0;
	public int FacilityIdx { get; set; } = 0;
}



[Serializable]
public class RestQuestData : IReadOnlyData
{
	public List<RestQuestRewardGroup> QuestRewardGroup = new List<RestQuestRewardGroup>();
	public int StageIdx { get; set; } = 0;


	public void Create()
	{

	}

	public RestQuestData(List<RestQuestRewardGroup> questrewardgroup , int stageidx)
    {
		QuestRewardGroup = questrewardgroup;
		StageIdx = stageidx;
    }

	public virtual object Clone()
	{

		var clone = new RestQuestData(QuestRewardGroup, StageIdx);
		return clone;
	}
}


public class RestQuestRewardGroup
{
	public int RewardGroup = 0;
	public int RewardIdx = 0;
	public int RewardCount = 0;


	public RestQuestRewardGroup(int rewardgroup, int rewardidx, int rewardcount)
	{
		RewardGroup = rewardgroup;
		RewardIdx = rewardidx;
		RewardCount = rewardcount;
	}
}





[Serializable]
public class QuestData : IReadOnlyData
{	
	public int QuestOrder { get; set; } = 0;
	public string QuestValue { get; set; } = "0";
	public bool ReceviedReward { get; set; } = false;
	
	public IReactiveProperty<BigInteger> QvProperty = new ReactiveProperty<BigInteger>();
	
	public IReactiveProperty<bool> ReceviedProperty = new ReactiveProperty<bool>();



	public void Create()
	{

		QvProperty.Value = BigInteger.Parse(QuestValue);
		ReceviedProperty.Value = ReceviedReward;

		QvProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			QuestValue = x.ToString();
			//Save();
		});
		ReceviedProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			ReceviedReward = x;
			//Save();
		});

	}

	public virtual object Clone()
	{
		var clone = new QuestData(){
			QuestOrder = QuestOrder,
			QuestValue = QuestValue,
			ReceviedReward = ReceviedReward
		};
		clone.Create();
		return clone;
	}
}


public class EventQuestData : QuestData
{


	public override object Clone()
	{
		var clone = new EventQuestData(){
			QuestOrder = QuestOrder,
			QuestValue = QuestValue,
			ReceviedReward = ReceviedReward
		};
		clone.Create();
		return clone;
	}
}


public class StageData : IClientData
{	
	public Dictionary<int, FacilityData> FacilityState { get; set; } = new Dictionary<int, FacilityData>();
	public int StageIdx {get; private set;} = 1;
	public int NextOpenFloor {get; private set;} = -1;
	public int MaxOpenFloor {get; private set;} = -1;
	public bool IsMaxFloor { get { return NextOpenFloor >= MaxOpenFloor; }}

	public FacilityData GetFacility(int facilityIdx)
	{
		if(FacilityState.ContainsKey(facilityIdx))
			return FacilityState[facilityIdx];
		
		return null;
	}	

	//public void Init(int idx)
	//{
	//	StageIdx = idx;
	//	var tb = Tables.Instance.GetTable<FacilityStage>().DataList.FindAll(x => x.stage == StageIdx);
	//	if(tb != null)
	//	{
	//		MaxOpenFloor = tb.Count - 1;
	//		for(var i = 0; i < tb.Count; ++i)
	//		{
	//			if(i == 0)
	//				continue;

	//			var findIdx = tb[i].facility_idx;
	//			if(FacilityState.ContainsKey(findIdx))
	//			{
	//				if(!FacilityState[findIdx].IsOpend)
	//				{
	//					NextOpenFloor = tb[i].facility_floor;
	//					return;
	//				}
	//			}				
	//		}
	//		NextOpenFloor = MaxOpenFloor;
	//	}		
	//}
	
	public void UpgradeStageIdx()
	{
		++StageIdx;
		FacilityState.Clear();
	}

	public void SetStageIdx(int idx)
	{
		StageIdx = idx;
		FacilityState.Clear();
	}
}

//public class HireCardDatas : IClientData
//{
//	public List<int> HireCardDataList = new List<int>();
//	public ReactiveProperty<int> RemainTimePropety = new ReactiveProperty<int>();
//	public DateTime creatTime { get; set; }
//}



public class EventHireCardDatas : IClientData
{
	public ReactiveProperty<int> ADRemainTimeProperty = new ReactiveProperty<int>();

}

public class MonsterBuffData : IClientData
{
	public IReactiveProperty<int> BuffLevel = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> BuffPoint = new ReactiveProperty<int>(0);
	public System.DateTime LastReduceTime = default(System.DateTime);
}

public class TicketData : IClientData
{
	public int TicketIdx;
	public int TicketCount = 0;
	public DateTime TicketResetTime = default(System.DateTime);
	public int TicketAdsCount = 0;

	public IReactiveProperty<int> TicketCountProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> ResetTimeProperty = new ReactiveProperty<int>();

	public void Create()
    {
		TicketCountProperty.Value = TicketCount;
		TicketCountProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			TicketCount = x;
		});
    }
}


public class DragonCaveData : IClientData
{
	public int DragonIdx;
	public int DragonState;
	public int HeartCount = 0;
	public int DragonLv;

	public DateTime GrowTime = default(System.DateTime);
	public IReactiveProperty<int> GrowTimeProperty = new ReactiveProperty<int>(0);

	public IReactiveProperty<int> HeartPropertyCount = new ReactiveProperty<int>(0);

	public bool IsUpgrade = false;


	public virtual void Create()
    {
		HeartPropertyCount.Value = HeartCount;
		HeartPropertyCount.Subscribe(x => { HeartCount = x;});
	}
}



public class ExpeditionData : IClientData
{
	public List<Conquestdata> ConQuestDataList = new List<Conquestdata>();
	public float MonsterCount = 0;
	public int TotalMonsterCount = 0;
	public int offlineattacktime = 0;
	public DateTime AttackTime;
	public DateTime EventEndTime;
	public int EventPartsIdx = 0;
	public int EventType = -1;
	public int EventCount = 0;
	public bool EventClear = false;
	public IReactiveProperty<int> AttackTimeProperty = new ReactiveProperty<int>();
	public ExpeditionMiddleRewardData MiddleRewardData = new ExpeditionMiddleRewardData();


	public void Create()
	{

	}

}

public class ExpeditionMiddleRewardData
{
	public IReactiveProperty<bool> IsMiddleReward = new ReactiveProperty<bool>(false);
	public float MonsterAttackDamage = 0f;
	public float HeroTotalAttack = 0f;
	public int MiddleAttackTime;

}


public class Conquestdata
{
	public int ChapterIdx = 0;
	public int PointOrder = 0;
	public int GroupOrder = 0;
	public bool IsReward = false;


   public Conquestdata(int chapteridx , int pointorder, int grouporder , bool isrewarad)
    {
		ChapterIdx = chapteridx;
		PointOrder = pointorder;
		GroupOrder = grouporder;
		IsReward = isrewarad;
    }
}


public class ShopData : IClientData
{
	public List<PackageData> packageDatas = new List<PackageData>();
	public int AdCount = 0;
	public ReactiveProperty<int> Adcountproperty = new ReactiveProperty<int>();
	public DateTime AdCreateTime;
	public ReactiveProperty<int> ADRemainTimeProperty = new ReactiveProperty<int>();

	public void Create()
    {
		Adcountproperty.Value = AdCount;

		Adcountproperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			AdCount = x;
		
		});

	}
}

public class GrowthCabinetData : IClientData
{
	public int WardobePoint = 0;
	public int ManagerPoint = 0;

	public DateTime ManagerAdCreateTime;
	public DateTime WardobeAdCreateTime;

	public ReactiveProperty<int> WardobeRemainTimeProperty = new ReactiveProperty<int>();
	public ReactiveProperty<int> ManagerRemainTimeProperty = new ReactiveProperty<int>();

	public void Create()
    {
		

    }
	public void EarnWardobePoint(int cnt)
	{
		WardobePoint += cnt; 
	}

	public void EarnManagerPoint(int cnt)
    {
		ManagerPoint += cnt;
    }

}

public class EventStatusData : IClientData
{
	public int CurEventIdx = 0;
	public int BeforeEventIdx = 0;
	public bool IsNewEvent = false;
	public DateTime EventCoolTime;
	public DateTime EventStartTime;

	public ReactiveProperty<int> EventCoolTimeProperty = new ReactiveProperty<int>();
	public ReactiveProperty<int> EventStartTimeProperty = new ReactiveProperty<int>();

	public EventStatusData(int cureventidx ,int beforeeventidx , bool isnewevent , long eventcooltime , long eventstartime)
    {
		CurEventIdx = cureventidx;
		BeforeEventIdx = beforeeventidx;
		IsNewEvent = isnewevent;
		EventCoolTime = new System.DateTime(eventcooltime);
		EventStartTime = new System.DateTime(eventstartime);
    }

	
	public void Create()
    {

    }
}

public class EventSpecialProductData : IClientData
{
	public int SpecialCatIdx = 0;
	public float BuffValue = 0f;
	public int BuffLv = 0;


	public EventSpecialProductData(int specialcatidx ,float buffvalue ,  int bufflv)
	{
		SpecialCatIdx = specialcatidx;
		BuffValue = buffvalue;
		BuffLv = bufflv;
	}


	public void Create()
	{

	}
}

public class EventShopData : IClientData
{
	public List<EventPackageData> packageDatas = new List<EventPackageData>();
}

[Serializable]
public class OptionData : IReadOnlyData {
	public bool BGM { get; set; } = true;
	public bool Effect { get; set; } = true;
	public bool Notifi { get; set; } = true;
	public bool SlowGraphic {get; set;} = true;

	public void Create() { }
	public object Clone()
	{
		return new OptionData();
	}
}

public class CompetitionData : IReadOnlyData
{
	public IReactiveCollection<CompetitionClearData> CompetitionClearList = new ReactiveCollection<CompetitionClearData>();
	public IReactiveCollection<CompetitionClearData> OpenSpecialCompetitionList = new ReactiveCollection<CompetitionClearData>();


	public void Create()
    {
	}

    public object Clone()
    {
		var clone = new CompetitionData();
		clone.Create();
		return clone;
    }
}

public class MagicPackageData : IReadOnlyData
{
	public int PackageData = 0;
	public int ItemData = 0;

	public MagicPackageData(int packageidx, int itemidx)
	{
		PackageData = packageidx;
		ItemData = itemidx;
	}

	public void Create()
	{
	}

	public object Clone()
	{
		var clone = new MagicPackageData(PackageData, ItemData);
		return clone;
	}
}

public class CompetitionClearData : IReadOnlyData
{
	public int CompetitionIdx = 0;
	public int CompetitionScore = 0;

	public IReactiveProperty<int> ScoreProperty = new ReactiveProperty<int>();

	public CompetitionClearData(int idx, int score)
	{
		CompetitionIdx = idx;
		CompetitionScore = score;
		Create();
	}

	public void Create()
    {
		ScoreProperty.Value = CompetitionScore;
		ScoreProperty.SkipLatestValueOnSubscribe().Subscribe(x => {
			CompetitionScore = x;
		});

	}

	public object Clone()
    {
		var clone = new CompetitionClearData(CompetitionIdx, CompetitionScore);
		return clone;
    }
}


public class PentHouseData : IReadOnlyData {

	public List<int> EquipFurniture = new List<int>();
	public List<int> EquipWardrobe = new List<int>();

	public IReactiveCollection<int> OwnedFurnitureList = new ReactiveCollection<int>();
//	public IReactiveCollection<WardrobeData> OwnedWardrobeList = new ReactiveCollection<WardrobeData>();

	public IReactiveCollection<int> NewItemList = new ReactiveCollection<int>();

	public IReactiveCollection<int> CompleteSetList = new ReactiveCollection<int>();
	public IReactiveCollection<int> NoneCompleteSetList = new ReactiveCollection<int>();
	public List<int> SetNewItemList = new List<int>();

	public int SpecialCount = 0;

	public void Create() {

	}
	public object Clone()
    {
		var clone = new PentHouseData()
		{
		};
		clone.Create();

		return clone;
	}
}

//public class WardrobeData
//{
//	public int WardrobeIdx { get; protected set; } = 0;
//	public int WardrobeCount { get; protected set; } = 0;
//	public int WardrobeLv { get; protected set; } = 0;

//	public IReactiveProperty<int> WardrobeLvProperty = new ReactiveProperty<int>();
//	public IReactiveProperty<int> WardrobeCntProperty = new ReactiveProperty<int>();

//	public WardrobeData(int idx, int cnt, int lv)
//	{
//		WardrobeIdx = idx;
//		WardrobeCount = cnt;
//		WardrobeLv = lv;
//		Create();
//	}

//	public void WardrobeLevelUp()
//	{
//		var basicTd = Tables.Instance.GetTable<PentHouse>().GetData(WardrobeIdx);
//		if (basicTd != null)
//		{
//			if (basicTd.max_level > WardrobeLvProperty.Value)
//				++WardrobeLvProperty.Value;
//		}
//	}

//	public void AddWardobeCard(int count)
//    {
//		WardrobeCount += count;

//	}

//	public void EarnWardrobeCard(int cnt)
//	{
//		WardrobeCntProperty.Value += cnt;
//	}


//	public virtual object Clone()
//	{
//		var clone = new WardrobeData(WardrobeIdx, WardrobeCount, WardrobeLv);
//		return clone;
//	}
//}

public class TicketShopData : IReadOnlyData
{
	public IReactiveCollection<int> WardrobeList = new ReactiveCollection<int>();
	public IReactiveCollection<int> PurchaseList = new ReactiveCollection<int>();

	public DateTime WardrobeLastResetTime { get; set; } = default(DateTime);

	public IReactiveProperty<int> ResetWardrobeTimeProperty = new ReactiveProperty<int>();

	public TicketShopData() { }
	public void Create() { }
	public object Clone()
	{
		var clone = new TicketShopData();
		{
		};
		clone.Create();

		return clone;
	}
}