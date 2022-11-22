using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using BanpoFri;

public enum DataState
{
	None,
	Main,
	Event
}
public partial class UserDataSystem
{
	public bool Bgm = true;
	public bool Effect = true;
	public bool SlowGraphic = false;
	public Config.Language Language = Config.Language.ko;
	public IReactiveProperty<int> Cash { get; private set; } = new ReactiveProperty<int>(0);
	public IReactiveCollection<string> BuyInappIds {get; private set;} = new ReactiveCollection<string>();
	public List<AchievementData> AchievementData { get; set; } = new List<AchievementData>();
	public IReactiveCollection<string> Tutorial {get; private set;} = new ReactiveCollection<string>();
	public IReactiveCollection<string> GameNotifications { get; private set; } = new ReactiveCollection<string>();
	public DailyMissionData DayworkData { get; set; } = new DailyMissionData();
	public MainPassData MainPassData { get; set; } = new MainPassData();
	public Dictionary<string, int> RecordCount {get; private set;} = new Dictionary<string, int>();
	public IUserDataMode CurMode {get; private set;}
	private UserDataMain mainData = new UserDataMain();
	private UserDataEvent eventData = new UserDataEvent();
	public DataState DataState {get; private set;} = DataState.None;
	public bool IsMainState { get { return DataState == DataState.Main; } }
	public IReactiveCollection<int> OneLink { get; private set; } = new ReactiveCollection<int>();

	public IReactiveProperty<BigInteger> HUDMoney = new ReactiveProperty<BigInteger>(0);
	public IReactiveProperty<int> HUDMaterial = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> HUDCash = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> HUDTicket = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> HUDSilverDump = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> HUDGoldDump = new ReactiveProperty<int>(0);

	//void ConnectReadOnlyDatas()
	//{
	//	ChangeDataMode(DataState.Main);

	//	RecordCount.Clear();
	//	Tutorial.Clear();
	//	GameNotifications.Clear();
	//	AchievementData.Clear();
	//	BuyInappIds.Clear();

	//	for (int i = 0; i < flatBufferUserData.RecordcountLength; ++i)
	//	{
	//		var data = flatBufferUserData.Recordcount(i);
	//		RecordCount.Add(data.Value.Idx, data.Value.Count);
	//	}
	//	mainData.Money.Value = BigInteger.Parse(flatBufferUserData.Money);
	//	mainData.StoreMoney.Value = BigInteger.Parse(flatBufferUserData.Storemoney);
	//	mainData.LastLoginTime = new System.DateTime(flatBufferUserData.Lastlogintime);
	//	mainData.BoostEndTime = new System.DateTime(flatBufferUserData.Boostendtime);
	//	Cash.Value = flatBufferUserData.Cash;
	//	mainData.Medal.Value = flatBufferUserData.Medal;
	//	mainData.Material.Value = flatBufferUserData.Material;
	//	mainData.EnergyGem.Value = flatBufferUserData.Energylightning;
	//	mainData.EnergyExp.Value = flatBufferUserData.Energyfly;
	//	mainData.SilverDump.Value = flatBufferUserData.Silverdump;
	//	mainData.GoldDump.Value = flatBufferUserData.Golddump;
	//	Language = (Config.Language)System.Enum.Parse(typeof(Config.Language), flatBufferUserData.Optiondata.Value.Language);
	//	Bgm = flatBufferUserData.Optiondata.Value.Bgm;
	//	Effect = flatBufferUserData.Optiondata.Value.Effect;
	//	SlowGraphic = flatBufferUserData.Optiondata.Value.Slowgraphic;
	//	mainData.ShopData.AdCount = flatBufferUserData.Adcount;
	//	mainData.ShopData.AdCreateTime = new System.DateTime(flatBufferUserData.Adcreatetime);

	//	mainData.GrowthCabinetData.ManagerAdCreateTime = new System.DateTime(flatBufferUserData.Growthcabinetdatas.Value.Wardobeadtime);
	//	mainData.GrowthCabinetData.WardobeAdCreateTime = new System.DateTime(flatBufferUserData.Growthcabinetdatas.Value.Manageradtime);
	//	mainData.GrowthCabinetData.ManagerPoint = flatBufferUserData.Growthcabinetdatas.Value.Managergrowthpercent;
	//	mainData.GrowthCabinetData.WardobePoint = flatBufferUserData.Growthcabinetdatas.Value.Wardobegrowthpercent;


	//	HUDMoney.Value = mainData.Money.Value;
	//	HUDMaterial.Value = mainData.Material.Value;
	//	HUDCash.Value = Cash.Value;
	//	HUDTicket.Value = mainData.Medal.Value;
	//	HUDGoldDump.Value = mainData.GoldDump.Value;
	//	HUDSilverDump.Value = mainData.SilverDump.Value;

	//	for (int i = 0; i < flatBufferUserData.ItemdatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Itemdatas(i);

	//		var itemdata = new ItemData()
	//		{
	//			ItemID = data.Value.Index,
	//			Count = data.Value.Count,
	//		};
	//		itemdata.Create();
	//		mainData.ItemData.Add(itemdata);

 //       }

	//	for(int i = 0; i < flatBufferUserData.HerodatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Herodatas(i);

	//		var herodata = new HeroData(data.Value.Heroidx, data.Value.Lv, data.Value.Exp);
	//		herodata.Create();
	//		mainData.HeroData.Add(herodata);
 //       }


	//	for(int i = 0; i < flatBufferUserData.HeroskilldatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Heroskilldatas(i);

	//		var heroskilldata = new HeroSkillData(data.Value.Skillidx, data.Value.Skillcooltime, data.Value.Skillmaintaintime);
	//		heroskilldata.Create();
	//		mainData.HeroSkillData.Add(heroskilldata);
 //       }


	//	mainData.HierCardDatas.Create();

	//	if (flatBufferUserData.Hirecarddata != null)
	//	{
	//		mainData.HierCardDatas.InitHireCreateTime = new System.DateTime(flatBufferUserData.Hirecarddata.Value.Inithiretime);
	//		mainData.HierCardDatas.GemBuyCount = flatBufferUserData.Hirecarddata.Value.Gembuycount;

	//		mainData.HierCardDatas.hireManagerIdxList[0] = flatBufferUserData.Hirecarddata.Value.Hiremanageridx.Value.Idx1;
	//		mainData.HierCardDatas.hireManagerIdxList[1] = flatBufferUserData.Hirecarddata.Value.Hiremanageridx.Value.Idx2;
	//		mainData.HierCardDatas.hireManagerIdxList[2] = flatBufferUserData.Hirecarddata.Value.Hiremanageridx.Value.Idx3;
	//		mainData.HierCardDatas.hireManagerGemPrice[0] = flatBufferUserData.Hirecarddata.Value.Hiremanagergemprice.Value.Idx1;
	//		mainData.HierCardDatas.hireManagerGemPrice[1] = flatBufferUserData.Hirecarddata.Value.Hiremanagergemprice.Value.Idx2;
	//		mainData.HierCardDatas.hireManagerGemPrice[2] = flatBufferUserData.Hirecarddata.Value.Hiremanagergemprice.Value.Idx3;
	//	}

	
	//	for (int i = 0; i  < flatBufferUserData.PackagedatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Packagedatas(i);

	//		var packageData = new PackageData(data.Value.Index, data.Value.Rewardgroup, data.Value.Starttime , data.Value.Buycount , data.Value.Openfirstcount , data.Value.Opensecondcout);

	//		mainData.ShopData.packageDatas.Add(packageData);
	//	}


	//	for(int i = 0; i <  flatBufferUserData.MagicpackagedataLength; ++i)
 //       {
	//		var data = flatBufferUserData.Magicpackagedata(i);

	//		var magicdata = new MagicPackageData(data.Value.Packageidx, data.Value.Itemidx);

	//		mainData.MagicPackageData.Add(magicdata);
 //       }


	//	GameRoot.Instance.UserData.CurMode.CurrentStageHeroIdxs.Clear();
	//	for(int i = 0; i  < flatBufferUserData.CurrentstageheroidxLength; ++i)
 //       {
	//		var data = flatBufferUserData.Currentstageheroidx(i);
	//		GameRoot.Instance.UserData.CurMode.CurrentStageHeroIdxs.Add(data);
	//	}

 //       for (int i = 0; i < flatBufferUserData.QuestdatasLength; ++i)
 //       {
 //           var data = flatBufferUserData.Questdatas(i);
	//		var questData = new QuestData()
	//		{
	//			QuestOrder = data.Value.Order,
	//			QuestValue = data.Value.Value,
	//			ReceviedReward = data.Value.Recevied
				
	//		   };
 //           questData.Create();
 //          mainData.QuestData.Add(questData);
 //       }

	//	for (int i = 0; i < flatBufferUserData.RestquestdataLength; ++i)
	//	{
	//		var data = flatBufferUserData.Restquestdata(i);

	//		List<RestQuestRewardGroup> RewstQuestRewardList = new List<RestQuestRewardGroup>();
	//		for(int j= 0;  j < data.Value.ResetquestrewardgroupLength; ++j)
 //           {
	//			var rewardgroup = data.Value.Resetquestrewardgroup(j);
	//			var restquestreward = new RestQuestRewardGroup(rewardgroup.Value.Rewardgroup, rewardgroup.Value.Rewardidx, rewardgroup.Value.Rewardcount);
	//			RewstQuestRewardList.Add(restquestreward);
	//		}

	//		var restquestdata = new RestQuestData(RewstQuestRewardList, data.Value.Stageidx);


	//		mainData.RestQuestData.Add(restquestdata);
	//	}

	//	for(int i = 0; i  < flatBufferUserData.ProductdatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Productdatas(i);

	//		List<FacilityMoneyData> facilitymoneylist = new List<FacilityMoneyData>();

	//		for(int j = 0; j < data.Value.FacilitymoneydatasLength; ++j)
 //           {
	//			var facilitymoneygroup = data.Value.Facilitymoneydatas(j);
	//			var facilitymoneydatas = new FacilityMoneyData { FacilityIdx = facilitymoneygroup.Value.Facilityidx, SlotOrder = facilitymoneygroup.Value.Slotorder };
	//			facilitymoneylist.Add(facilitymoneydatas);

	//		}

	//		var productdata = new ProductData(facilitymoneylist, data.Value.Facilityorder, data.Value.Posx , data.Value.Posy);

	//		mainData.ProductData.Add(productdata); 
 //       }


	//	for (int i = 0; i < flatBufferUserData.AchievementdatasLength; ++i)
 //       {
	//		var data = flatBufferUserData.Achievementdatas(i);
	//		var achieve = new AchievementData()
	//		{
	//			Index = data.Value.Index,
	//			Value = data.Value.Value,
	//			ReceiveLevel = data.Value.Receivedlevel
	//		};
	//		achieve.Create();
	//		AchievementData.Add(achieve);
 //       }

	//	DayworkData.Create();
	//	if(flatBufferUserData.Dayworkdata != null)
 //       {
	//		DayworkData.ResetTime = new System.DateTime(flatBufferUserData.Dayworkdata.Value.Resettime);
	//		for(int i = 0; i < flatBufferUserData.Dayworkdata.Value.QuestdataLength; ++i)
 //           {
	//			var data = flatBufferUserData.Dayworkdata.Value.Questdata(i);
	//			var questData = new QuestData()
 //               {	
	//				QuestOrder = data.Value.Order,
	//				QuestValue = data.Value.Value,
	//				ReceviedReward = data.Value.Recevied

	//			};
	//			questData.Create();
	//			DayworkData.MissionDatas.Add(questData);
	//		}
 //       }

	//	MainPassData.Create();
	//	if(flatBufferUserData.Mainpassdata != null)
 //       {
	//		for (int i = 0; i < flatBufferUserData.Mainpassdata.Value.QuestdataLength; ++i)
	//		{
	//			var data = flatBufferUserData.Mainpassdata.Value.Questdata(i);
	//			var questData = new QuestData()
	//			{
	//				QuestOrder = data.Value.Order,
	//				QuestValue = data.Value.Value,
	//				ReceviedReward = data.Value.Recevied

	//			};
	//			questData.Create();
	//			MainPassData.MainDatas.Add(questData);
	//		}
	//	}

	//	mainData.EnergyData.Create();
	//	if(flatBufferUserData.Energydata != null)
 //       {
	//		mainData.EnergyData.EnergyIdx = flatBufferUserData.Energydata.Value.Energyidx;
	//		mainData.EnergyData.EnergyOrder = flatBufferUserData.Energydata.Value.Energycurrentorder;
	//		mainData.EnergyData.EnergyEventCoolTime = new System.DateTime(flatBufferUserData.Energydata.Value.Energyeventcooltime);
	//		mainData.EnergyData.EnergyEventLimitTime = new System.DateTime(flatBufferUserData.Energydata.Value.Limitenergyeventcreatetime);
	//		mainData.EnergyData.BeforeEnergyIdx = flatBufferUserData.Energydata.Value.Beforeenergyidx;
	//		mainData.EnergyData.ViewDoublePass2Popup = flatBufferUserData.Energydata.Value.Viewdoublepasspopup;
	//		for (int i = 0; i <  flatBufferUserData.Energydata.Value.EnergycleardatasLength; ++i)
 //           {
	//			var data = flatBufferUserData.Energydata.Value.Energycleardatas(i);
	//			var energydata = new EnergyClearDatas(data.Value.Energyidx, data.Value.Energycleartime, data.Value.Energyorder, data.Value.Doublepass);
	//			mainData.EnergyData.ClearDataList.Add(energydata);
 //           }

	//		for (int i = 0; i < flatBufferUserData.Energydata.Value.EnergyclearorderlistLength; i++)
	//		{
	//			var data = flatBufferUserData.Energydata.Value.Energyclearorderlist(i);
	//			mainData.EnergyData.EnergyClearOrderList.Add(data);
	//		}

	//		for(int i = 0; i < flatBufferUserData.Energydata.Value.EnergyrewardcountlistLength; ++i)
 //           {
	//			var data = flatBufferUserData.Energydata.Value.Energyrewardcountlist(i);
	//			mainData.EnergyData.EnergyRewardCountList.Add(data);
 //           }
	//	}


	//	if(flatBufferUserData.Expeditiondatas != null)
 //       {
	//		mainData.ExpeditionData.AttackTime = new System.DateTime(flatBufferUserData.Expeditiondatas.Value.Attacktime);
	//		mainData.ExpeditionData.MonsterCount = flatBufferUserData.Expeditiondatas.Value.Monstercount;
	//		mainData.ExpeditionData.TotalMonsterCount = flatBufferUserData.Expeditiondatas.Value.Totalmonstercount;
	//		mainData.ExpeditionData.offlineattacktime = flatBufferUserData.Expeditiondatas.Value.Offlineattacktime;
	//		mainData.ExpeditionData.EventEndTime = new System.DateTime(flatBufferUserData.Expeditiondatas.Value.Eventendtime);
	//		mainData.ExpeditionData.EventPartsIdx = flatBufferUserData.Expeditiondatas.Value.Eventpartsidx;
	//		mainData.ExpeditionData.EventType = flatBufferUserData.Expeditiondatas.Value.Eventtype;
	//		mainData.ExpeditionData.EventClear = flatBufferUserData.Expeditiondatas.Value.Eventclear;

	//		if (flatBufferUserData.Expeditiondatas.Value.Expeditionmiddlereward != null)
	//		{
	//			mainData.ExpeditionData.MiddleRewardData.IsMiddleReward.Value = flatBufferUserData.Expeditiondatas.Value.Expeditionmiddlereward.Value.Ismiddlereward;
	//			mainData.ExpeditionData.MiddleRewardData.MonsterAttackDamage = flatBufferUserData.Expeditiondatas.Value.Expeditionmiddlereward.Value.Monsterattackdamage;
	//			mainData.ExpeditionData.MiddleRewardData.HeroTotalAttack = flatBufferUserData.Expeditiondatas.Value.Expeditionmiddlereward.Value.Herototalattack;
	//			mainData.ExpeditionData.MiddleRewardData.MiddleAttackTime =flatBufferUserData.Expeditiondatas.Value.Expeditionmiddlereward.Value.Middleattacktime;
	//		}

	//		for (int i = 0; i < flatBufferUserData.Expeditiondatas.Value.CompleteconquerdataLength; ++i)
 //           {
	//			var data = flatBufferUserData.Expeditiondatas.Value.Completeconquerdata(i);
	//			var conquestdata = new Conquestdata(data.Value.Chapteridx, data.Value.Pointorder, data.Value.Grouporder, data.Value.Isreward);
	//			mainData.ExpeditionData.ConQuestDataList.Add(conquestdata);
 //           }
 //       }

	//	if(flatBufferUserData.Monsterbuffdata != null)
 //       {
	//		mainData.MonsterBuffData.BuffLevel.Value = flatBufferUserData.Monsterbuffdata.Value.Bufflevel;
	//		mainData.MonsterBuffData.BuffPoint.Value = flatBufferUserData.Monsterbuffdata.Value.Buffpoint;
	//		mainData.MonsterBuffData.LastReduceTime = new System.DateTime(flatBufferUserData.Monsterbuffdata.Value.Lastreducetime);
 //       }

	//	if(flatBufferUserData.Attendancedata != null)
 //       {
	//		mainData.AttendanceData.AttendanceIdx = flatBufferUserData.Attendancedata.Value.Attendanceidx;
	//		mainData.AttendanceData.AttendanceOrder = flatBufferUserData.Attendancedata.Value.Attendanceorder;
	//		mainData.AttendanceData.LastAttendanceTime = new System.DateTime(flatBufferUserData.Attendancedata.Value.Lastattendancetime);
 //       }

	//	if(flatBufferUserData.Dropboxdata != null)
 //       {
	//		mainData.DropBoxData.MonsterKillCount = flatBufferUserData.Dropboxdata.Value.Monsterkillcount;
	//		mainData.DropBoxData.BoxCount.Value = flatBufferUserData.Dropboxdata.Value.Boxcount;
 //       }

	//	for(int i = 0; i < flatBufferUserData.TicketdatasLength; i++)
 //       {
	//		var data = flatBufferUserData.Ticketdatas(i);

	//		var ticketData = new TicketData()
	//		{
	//			TicketIdx = data.Value.Ticketidx,
	//			TicketCount = data.Value.Ticketcount,
	//			TicketResetTime = new System.DateTime(data.Value.Ticketresettime),
	//			TicketAdsCount = data.Value.Adscount
	//		};
	//		ticketData.Create();

	//		mainData.TicketData.Add(ticketData);

 //       }


	//	for(int i = 0; i  < flatBufferUserData.DragoncavedataLength; i++)
 //       {
	//		var data = flatBufferUserData.Dragoncavedata(i);

	//		var dragoncavedata = new DragonCaveData()
	//		{
	//			DragonIdx = data.Value.Dragonidx,
	//			DragonState = data.Value.Dragonstate,
	//			GrowTime = new System.DateTime(data.Value.Dragongrowtime),
	//			IsUpgrade = data.Value.Isupgrade,
	//			HeartCount = data.Value.Heartcount,
	//			DragonLv = data.Value.Dragonlevel,
		
	//		};
	//		dragoncavedata.Create();
	//		mainData.DragonCaveData.Add(dragoncavedata);
 //       }

 //       if (!string.IsNullOrEmpty(flatBufferUserData.Buyinappids))
	//	{
	//		var splitArr = flatBufferUserData.Buyinappids.Split(';');
	//		foreach(var productIdx in splitArr)
	//		{
	//			BuyInappIds.Add(productIdx);
	//		}
	//	}

	//	if(!string.IsNullOrEmpty(flatBufferUserData.Tutorial))
	//	{
	//		var splitArr = flatBufferUserData.Tutorial.Split(';');
	//		foreach(var productIdx in splitArr)
	//		{
	//			Tutorial.Add(productIdx);
	//		}
	//	}

	//	for (int i = 0; i < flatBufferUserData.OnelinkdataLength; i++)
	//	{
	//		OneLink.Add(flatBufferUserData.Onelinkdata(i));
	//	}


	//	if (!string.IsNullOrEmpty(flatBufferUserData.Gamenotifications))
	//	{
	//		var splitArr = flatBufferUserData.Gamenotifications.Split(';');
	//		foreach (var productIdx in splitArr)
	//		{
	//			GameNotifications.Add(productIdx);
	//		}
	//	}

	//	var curStage = flatBufferUserData.Stageidx;
	//	var findFSDatas = Tables.Instance.GetTable<FacilityStage>().DataList.FindAll(x => x.stage == curStage);
	//	for (int i = 0; i < flatBufferUserData.FacilitydatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Facilitydatas(i);
	//		var find = findFSDatas.Find(x => x.facility_idx == data.Value.Index);
	//		if(find != null)
	//		{
	//			var newData = new FacilityData(
	//			data.Value.Index,
	//			data.Value.Isopend,
	//			data.Value.Slotcount,
	//			data.Value.Level,
	//			data.Value.Manager.Value.Idx1,
	//			data.Value.Manager.Value.Idx2,
	//			data.Value.Manager.Value.Idx3,
	//			data.Value.Guestcount);
				
	//			mainData.StageData.FacilityState.Add(data.Value.Index, newData);
	//			findFSDatas.Remove(find);
	//		}
	//	}

	//	foreach(var remind in findFSDatas)
	//	{
	//		var newData = new FacilityData(remind.facility_idx, false, 1, 1, -1, -1, -1, 0);
	//		mainData.StageData.FacilityState.Add(remind.facility_idx, newData);
	//	}
	//	mainData.StageData.Init(flatBufferUserData.Stageidx);

	//	for(int i = 0; i < flatBufferUserData.ManagerdatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Managerdatas(i);
	//		var newData = new ManagerData(data.Value.Index, data.Value.Count, data.Value.Level, data.Value.Createtime);
	//		mainData.ManagerData.Add(newData);
	//	}

		

	//	////skinchangedata
	//	//if(flatBufferUserData.Eventskinchangedata != null)
 // //      {

	//	//	mainData.SkinChangeData.Create();
	//	//	for (int i = 0; i < flatBufferUserData.Eventskinchangedata.Value.AdflyskinchangedatasLength; i++)
 // //          {
	//	//		var data = flatBufferUserData.Eventskinchangedata.Value.Adflyskinchangedatas(i);
	//	//		var adflyskindata = new AdflySkinChangeData(data.Value.Eventidx, data.Value.Skinidx, data.Value.Level, data.Value.Buffcooltime);
	//	//		mainData.SkinChangeData.AdFlySkinChangeList.Add(adflyskindata);

	//	//	}

	//	//	for (int i = 0; i < flatBufferUserData.Eventskinchangedata.Value.FacilityhaveskindatasLength; i++)
 // //          {
	//	//		var data = flatBufferUserData.Eventskinchangedata.Value.Facilityhaveskindatas(i);
	//	//		var facilityskindata = new FacilitySkinChangeData(data.Value.Eventidx, data.Value.Skinidx);
	//	//		mainData.SkinChangeData.FacilityEventSkinList.Add(facilityskindata);

 // //          }

	//	//	for(int i = 0; i < flatBufferUserData.Eventskinchangedata.Value.EquipfacilitydatalistLength; i++)
 // //          {
	//	//		var data = flatBufferUserData.Eventskinchangedata.Value.Equipfacilitydatalist(i);
	//	//		var facilityskindata = new FacilitySkinChangeData(data.Value.Eventidx, data.Value.Skinidx);
	//	//		mainData.SkinChangeData.EquipFacilityEventSkiList.Add(facilityskindata);
 // //          }

	//	//	mainData.SkinChangeData.EquipAdFlySkinChange = new EquipAdflySkinChangeData(flatBufferUserData.Eventskinchangedata.Value.Equipadflyskindata.Value.Eventidx, flatBufferUserData.Eventskinchangedata.Value.Equipadflyskindata.Value.Skinidx);
	//	//	mainData.SkinChangeData.EquipAdFlySkinChange.Create();
 // //      }
  



	//	//stagecleardata	
 //       for (int i = 0; i < flatBufferUserData.StagecleardataLength; ++i)
	//	{
	//		var data = flatBufferUserData.Stagecleardata(i);

	//		List<int> ItemList = new List<int>();
	//		for(int j = 0; j < data.Value.PackageitemlistLength; ++j)
 //           {
	//			var item = data.Value.Packageitemlist(j);
	//			ItemList.Add(item);

	//		}
	//		var ClearData = new StageClearData(ItemList, data.Value.Stageidx, data.Value.Packagestarttime);
	//		mainData.StageClearData.Add(ClearData);
	//	}

	//	//ticketshop
	//	if(flatBufferUserData.Ticketshopdata != null)
	//	{
	//		mainData.TicketShopData = new TicketShopData();
	//		for (int i = 0; i < flatBufferUserData.Ticketshopdata.Value.WardrobelistLength; i++)
	//		{
	//			mainData.TicketShopData.WardrobeList.Add(flatBufferUserData.Ticketshopdata.Value.Wardrobelist(i));
	//		}

	//		for (int i = 0; i < flatBufferUserData.Ticketshopdata.Value.WardrobepurchaselistLength; i++)
	//		{
	//			mainData.TicketShopData.PurchaseList.Add(flatBufferUserData.Ticketshopdata.Value.Wardrobepurchaselist(i));
	//		}
	//		mainData.TicketShopData.WardrobeLastResetTime = new System.DateTime(flatBufferUserData.Ticketshopdata.Value.Wardrobelastresettime);
	//	}


	//	mainData.DragonHuntData.DragonHuntRewardIndexsList.Clear();
	//	//dragonhunt
	//	if(flatBufferUserData.Dragonhuntdata != null)
 //       {
	//		mainData.DragonHuntData.Hp = flatBufferUserData.Dragonhuntdata.Value.Dragonhp;
	//		mainData.DragonHuntData.CurGrade = flatBufferUserData.Dragonhuntdata.Value.Dragongrade;

	//		for(int i = 0; i < flatBufferUserData.Dragonhuntdata.Value.DragonhuntrewardidxsLength; i++)
 //           {
	//			mainData.DragonHuntData.DragonHuntRewardIndexsList.Add(flatBufferUserData.Dragonhuntdata.Value.Dragonhuntrewardidxs(i));
 //           }
	//	}


	//	mainData.AdFlySkinData.Create();
	//	if (flatBufferUserData.Adflyskindata != null)
 //       {
	//		mainData.AdFlySkinData = new AdFlySkinData();
	//		for(int i = 0; i  < flatBufferUserData.Adflyskindata.Value.SkinidxLength; i++)
 //           {
	//			mainData.AdFlySkinData.AdFlySkinIdxs.Add(flatBufferUserData.Adflyskindata.Value.Skinidx(i));
 //           }

	//		mainData.AdFlySkinData.Level = flatBufferUserData.Adflyskindata.Value.Level;
	//		mainData.AdFlySkinData.CurEquipAdflyIdx = flatBufferUserData.Adflyskindata.Value.Curequipskinidx;
 //       }




 //       //-----------------------event-----------------------

 //       eventData.Money.Value = BigInteger.Parse(flatBufferUserData.Eventdata.Value.Money);
	//	eventData.StoreMoney.Value = BigInteger.Parse(flatBufferUserData.Eventdata.Value.Storemoney);
	//	eventData.Material.Value = flatBufferUserData.Eventdata.Value.Material;
	//	eventData.Medal.Value = flatBufferUserData.Eventdata.Value.Medal;



	//	eventData.LastLoginTime = new System.DateTime(flatBufferUserData.Eventdata.Value.Lastlogintime);
	//	eventData.BoostEndTime = new System.DateTime(flatBufferUserData.Eventdata.Value.Boostendtime);

	
	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.QuestdatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Questdatas(i);
	//		var questData = new QuestData()
	//		{
	//			QuestOrder = data.Value.Order,
	//			QuestValue = data.Value.Value,
	//			ReceviedReward = data.Value.Recevied

	//		};
	//		questData.Create();
	//		eventData.QuestData.Add(questData);
	//	}

	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.FacilitydatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Facilitydatas(i);
	//		var newData = new FacilityData(
	//			data.Value.Index,
	//			data.Value.Isopend,
	//			data.Value.Slotcount,
	//			data.Value.Level,
	//			data.Value.Manager.Value.Idx1,
	//			data.Value.Manager.Value.Idx2,
	//			data.Value.Manager.Value.Idx3,
	//			data.Value.Guestcount);

	//		eventData.StageData.FacilityState.Add(data.Value.Index, newData);
	//	}



	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.HeroskilldatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Heroskilldatas(i);

	//		var heroskilldata = new HeroSkillData(data.Value.Skillidx, data.Value.Skillcooltime, data.Value.Skillmaintaintime);
	//		heroskilldata.Create();
	//		eventData.HeroSkillData.Add(heroskilldata);
	//	}



	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.ProductdatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Productdatas(i);

	//		List<FacilityMoneyData> facilitymoneylist = new List<FacilityMoneyData>();

	//		for (int j = 0; j < data.Value.FacilitymoneydatasLength; ++j)
	//		{
	//			var facilitymoneygroup = data.Value.Facilitymoneydatas(j);
	//			var facilitymoneydatas = new FacilityMoneyData { FacilityIdx = facilitymoneygroup.Value.Facilityidx, SlotOrder = facilitymoneygroup.Value.Slotorder };
	//			facilitymoneylist.Add(facilitymoneydatas);

	//		}

	//		var productdata = new ProductData(facilitymoneylist, data.Value.Facilityorder, data.Value.Posx, data.Value.Posy);

	//		eventData.ProductData.Add(productdata);
	//	}




	//	if (flatBufferUserData.Eventstatusdata.HasValue)
	//		eventData.EventData.EventStatusData = new EventStatusData(flatBufferUserData.Eventstatusdata.Value.Cureventidx,flatBufferUserData.Eventstatusdata.Value.Beforeeventidx ,flatBufferUserData.Eventstatusdata.Value.Isnewevent, flatBufferUserData.Eventstatusdata.Value.Eventcooltime, flatBufferUserData.Eventstatusdata.Value.Eventstarttime);
	//	else
	//		eventData.EventData.EventStatusData = new EventStatusData(0,0,false, 0, 0);

	//	if (eventData.EventData.EventStatusData.CurEventIdx > 0)
	//	{
	//		//Have To Change EventIdx To EventStageIdx
	//		var td = Tables.Instance.GetTable<SeasonalEvent>().GetData(eventData.EventData.EventStatusData.CurEventIdx);
	//		if (td != null)
	//		eventData.StageData.Init(td.stage);
	//	}

	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.ManagerdatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Managerdatas(i);
	//		var newData = new ManagerData(data.Value.Index, data.Value.Count, data.Value.Level, data.Value.Createtime);
	//		eventData.ManagerData.Add(newData);
	//	}

	//	for(int i = 0; i < flatBufferUserData.Eventdata.Value.EventmainrewardsLength; ++i)
 //       {
	//		var data = flatBufferUserData.Eventdata.Value.Eventmainrewards(i);
	//		var newdata = new EventMainRewardData(data.Value.Missionorder, data.Value.Missionidx,data.Value.Rewardtype, data.Value.Rewardidx ,  data.Value.Rewardcount);
	//		eventData.EventMainRewardData.Add(newdata);
 //       }


	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.EventpassrewardsLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Eventpassrewards(i);
	//		var newdata = new EventPassRewardData(data.Value.Missionorder,data.Value.Missionidx , data.Value.Type , data.Value.Idx , data.Value.Count);
	//		eventData.EventPassRewardData.Add(newdata);
	//	}


	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.EventspecialproductLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Eventspecialproduct(i);
	//		var newdata = new EventSpecialProductData(data.Value.Specialcatidx, data.Value.Buffvalue ,data.Value.Bufflv);
	//		eventData.EventData.EventSpecialProductList.Add(newdata);
	//	}


	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.PackagedatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Packagedatas(i);

	//		var packageData = new PackageData(data.Value.Index, data.Value.Rewardgroup, data.Value.Starttime, data.Value.Buycount , data.Value.Openfirstcount , data.Value.Opensecondcout);

	//		eventData.ShopData.packageDatas.Add(packageData);
	//	}

	//	for (int i = 0; i < flatBufferUserData.Eventdata.Value.ItemdatasLength; ++i)
	//	{
	//		var data = flatBufferUserData.Eventdata.Value.Itemdatas(i);

	//		var itemdata = new ItemData()
	//		{
	//			ItemID = data.Value.Index,
	//			Count = data.Value.Count,
	//		};
	//		itemdata.Create();
	//		eventData.ItemData.Add(itemdata);
	//	}

	//	if (!string.IsNullOrEmpty(flatBufferUserData.Eventdata.Value.Eventquestvalue))
	//		eventData.EventData.QuestValue.Value = BigInteger.Parse(flatBufferUserData.Eventdata.Value.Eventquestvalue);
	//	eventData.EventData.QuestLevel.Value = flatBufferUserData.Eventdata.Value.Eventquestlevel;
	//	eventData.EventData.ReceiveLevel = flatBufferUserData.Eventdata.Value.Receivequestlevel;
	//	eventData.EventData.PremiumReceiveLevel = flatBufferUserData.Eventdata.Value.Receivepremiumlevel;

	//	if (flatBufferUserData.Eventdata.Value.Roulettedata != null)
	//	{
	//		var facility = flatBufferUserData.Eventdata.Value.Roulettedata.Value.Facilitydata.Value;
	//		var RouletteFacility = new FacilityData(facility.Index,
	//												facility.Isopend,
	//												facility.Slotcount,
	//												facility.Level,
	//												facility.Manager.Value.Idx1,
	//												facility.Manager.Value.Idx2,
	//												facility.Manager.Value.Idx3,
	//												facility.Guestcount);

	//		BigInteger rouletteCoin;
	//		if (flatBufferUserData.Eventdata.Value.Roulettedata.Value.Roulettecoin == null ||
	//			flatBufferUserData.Eventdata.Value.Roulettedata.Value.Roulettecoin.Length == 0)
	//			rouletteCoin = 0;
	//		else rouletteCoin = BigInteger.Parse(flatBufferUserData.Eventdata.Value.Roulettedata.Value.Roulettecoin);
	//		eventData.EventData.RouletteData = new RouletteData(RouletteFacility, flatBufferUserData.Eventdata.Value.Roulettedata.Value.Runcount, rouletteCoin);
			
	//	}

		
	//	TutoDataCheck();
	//}

	//public void ChangeDataMode(DataState state)
	//{
	//	if(state == DataState)
	//		return;

	//	TpLog.Log($"ChangeDataMode:{state.ToString()}");
	//	switch(state)
	//	{
	//		case DataState.Main:
	//			CurMode = mainData;
	//		break;
	//		case DataState.Event:
	//			CurMode = eventData;
	//		break;
	//	}

	//	DataState = state;
	//}

	//public UserDataEvent CurEventData { get { return eventData; } }


	//public UserDataMain CurMainData { get { return mainData; } }

	//private void SnycCollectionToDB<T, U>(IList<T> db, IEnumerable<U> collector) where T : class
	//{
	//	db.Clear();
	//	foreach(var iter in collector)
	//	{
	//		db.Add(iter as T);
	//	}
	//}

	//private void SnycCollectionToClient<T, U>(IList<T> db, IEnumerable<U> collector) 
	//where T : class, IReadOnlyData
	//where U : class, IReadOnlyData
	//{
	//	db.Clear();
	//	foreach(var iter in collector)
	//	{
	//		db.Add(iter.Clone() as T);
	//	}
	//}

	//public void AddRecordCount(Config.RecordCountKeys idx, int count, params object[] objs)
	//{
	//	var strKey = ProjectUtility.GetRecordCountText(idx, objs);
	//	if (RecordCount.ContainsKey(strKey))
	//		RecordCount[strKey] += count;
	//	else
	//		RecordCount.Add(strKey, count);


	//}

	//public bool FindRecordCount(Config.RecordCountKeys idx, params object[] objs)
 //   {
	//	var strKey = ProjectUtility.GetRecordCountText(idx, objs);


	//	return RecordCount.ContainsKey(strKey);
	//}

	//public void RemoveRecordCount(Config.RecordCountKeys idx , params object[] objs)
 //   {
	//	var strKey = ProjectUtility.GetRecordCountText(idx, objs);
	//	if (RecordCount.ContainsKey(strKey))
	//		RecordCount.Remove(strKey);


	//}

	//public void SyncHUDCurrency(int currencyID = -1)
 //   {
	//	if(currencyID < 0)
	//	{
	//		HUDMoney.Value = CurMode.Money.Value;
	//		HUDMaterial.Value = CurMode.Material.Value;
	//		HUDCash.Value = Cash.Value;
	//		HUDTicket.Value = CurMode.Medal.Value;
	//	}else if(currencyID == (int)Config.CurrencyID.Cash)
	//	{
	//		HUDCash.Value = Cash.Value;
	//	}
	//	else if (currencyID == (int)Config.CurrencyID.Material || currencyID == (int)Config.CurrencyID.EventMaterial)
	//	{
	//		HUDMaterial.Value = CurMode.Material.Value;
	//	}
	//	else if (currencyID == (int)Config.CurrencyID.Money || currencyID == (int)Config.CurrencyID.EventMoney)
	//	{
	//		HUDMoney.Value = CurMode.Money.Value;
	//	}
	//	else if (currencyID == (int)Config.CurrencyID.Medal || currencyID == (int)Config.CurrencyID.EventMedal)
	//	{
	//		HUDTicket.Value = CurMode.Medal.Value;
	//	}
	//}

	//public void SetHUDUIReward(int rewardType, int rewardIdx, BigInteger rewardCnt)
 //   {
	//	if (rewardType != (int)Config.RewardGroup.Currency) return;
	//	switch (rewardIdx)
	//	{
	//		case (int)Config.CurrencyID.EventMaterial:
	//		case (int)Config.CurrencyID.Material:
	//			{
	//				HUDMaterial.Value += (int)rewardCnt;
	//			}
	//			break;
	//		case (int)Config.CurrencyID.EventMoney:
	//		case (int)Config.CurrencyID.Money:
	//			{
	//				HUDMoney.Value += rewardCnt;
	//			}
	//			break;
	//		case (int)Config.CurrencyID.Cash:
	//			{
	//				HUDCash.Value += (int)rewardCnt;
	//			}
	//			break;
	//		case (int)Config.CurrencyID.EventMedal:
	//		case (int)Config.CurrencyID.Medal:
	//			{
	//				HUDTicket.Value += (int)rewardCnt;
	//			}
	//			break;
	//		case (int)Config.CurrencyID.SilverDump:
 //               {
	//				HUDSilverDump.Value += (int)rewardCnt;
 //               }
	//			break;
	//		case (int)Config.CurrencyID.GoldDump:
	//			{
	//				HUDGoldDump.Value += (int)rewardCnt;
	//			}
	//			break;
	//	}
	//}

	//public void SetReward(int rewardType, int rewardIdx, BigInteger rewardCnt, bool hudRefresh = true)
	//{
	//	switch(rewardType)
	//	{
	//		case (int)Config.RewardGroup.Currency:
	//		{
	//				switch (rewardIdx)
	//				{
	//					case (int)Config.CurrencyID.StoreMoney:
	//						{
	//							CurMode.StoreMoney.Value += rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.EventMaterial:
	//					case (int)Config.CurrencyID.Material:
	//						{
	//							CurMode.Material.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.EventMoney:
	//					case (int)Config.CurrencyID.Money:
	//						{
	//							CurMode.Money.Value += rewardCnt;
	//							if (rewardCnt > 0 && GameRoot.Instance.CurInGameType == InGameType.Event)
	//								CurMode.EventData.QuestValue.Value += rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.Cash:
	//						{
	//							Cash.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.GachaCoin:
	//						{
	//							CurMode.EventData.RouletteData.RouletteCoin.Value += rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.EventMedal:
 //                           {
	//							eventData.Medal.Value += (int)rewardCnt;

	//							GameRoot.Instance.UserData.AddRecordCount(Config.RecordCountKeys.EventTicketAddCount, (int)rewardCnt);

	//							List<TpParameter> parameters = new List<TpParameter>();
	//							parameters.Add(new TpParameter("season", GameRoot.Instance.UserData.CurEventData.EventData.EventStatusData.CurEventIdx));
	//							parameters.Add(new TpParameter("type", GameRoot.Instance.ShopSystem.EventPassPremium ? 2 : 1));
	//							parameters.Add(new TpParameter("count", (long)rewardCnt));
	//							GameRoot.Instance.PluginSystem.AnalyticsProp.AllEvent(IngameEventType.None,
	//								"m_season_event_ticket", parameters);

	//						}
	//						break;
	//					case (int)Config.CurrencyID.Medal:
	//						{
	//							CurMode.Medal.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.SilverDump:
	//						{
	//							mainData.SilverDump.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.GoldDump:
	//						{
	//							mainData.GoldDump.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.EnergyFly:
	//						{
	//							mainData.EnergyExp.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.EnergyLightning:
	//						{
	//							mainData.EnergyGem.Value += (int)rewardCnt;
	//						}
	//						break;
	//					case (int)Config.CurrencyID.ConquestTicket:
	//					case (int)Config.CurrencyID.DragonTicket:
	//					case (int)Config.CurrencyID.PropertyTicket:
 //                           {
	//							var data = mainData.TicketData.Where(x => x.TicketIdx == rewardIdx).FirstOrDefault();
	//							if(data != null)
	//							{
	//								var info = Tables.Instance.GetTable<ExpeditionCalendar>().DataList.Find(x => x.ticket_idx == rewardIdx);
	//								data.TicketCountProperty.Value += (int)rewardCnt;
	//								if (data.TicketCountProperty.Value >= info.ticket_max_count)
	//								{
	//									data.TicketResetTime = default(System.DateTime);
	//								}

	//								GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.ExpeditionTicket);
	//							}
 //                           }break;
	//				}
	//			if(hudRefresh)
 //               {
	//				SetHUDUIReward(rewardType, rewardIdx, rewardCnt);
 //               }
	//		}
	//		break;
	//		case(int)Config.RewardGroup.Manager:
	//		{
	//			var haveManager = CurMode.ManagerData.Where( x => x.ManagerIdx == rewardIdx).FirstOrDefault();
	//			if(haveManager != null)
	//			{
	//				haveManager.EarnManagerCard((int)rewardCnt);
	//				if(haveManager.ManagerCntProperty.Value == -1)
 //                   {
	//					CurMode.ManagerData.Remove(haveManager);
 //                   }
	//			}
	//			else
	//			{
	//				var managerData = new ManagerData(rewardIdx, (int)rewardCnt - 1, 1, TimeSystem.GetCurTime().Ticks);
	//				CurMode.ManagerData.Add(managerData);
	//				GameRoot.Instance.GameNotification.AddOnceNotiRegister(GameNotificationSystem.NotificationCategory.NewManagerCard.ToString());
	//				GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.NewManagerCard);

	//			}
	//			GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.GachaManagerSlot);
	//			GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.FacilityManagerSlot);
	//			GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.ManagerLevelUp);
	//		}
	//		break;
	//		case (int)Config.RewardGroup.DragonCave:
 //               {
	//				GameRoot.Instance.DragonCaveSystem.AddDragon(rewardIdx);
 //               }
	//			break;
	//	}

	


	//}

	public void RefreshUICurrency()
	{
		
	}


	private void TutoDataCheck()
    {

    }
}