using System.Collections;
using System;
using System.Numerics;
using System.Collections.Generic;
using UniRx;

public interface IUserDataMode
{
	// Config.Language Language {get; set;}= Config.Language.en;
	IReactiveProperty<BigInteger> Money { get; set; }
	IReactiveProperty<BigInteger> StoreMoney { get; set; }
	IReactiveProperty<int> Material { get; set; }
	IReactiveProperty<int> EnergyExp { get; set; }
	IReactiveProperty<int> EnergyGem { get; set; }
	IReactiveCollection<ManagerData> ManagerData { get; set; }
	IReactiveCollection<ItemData> ItemData { get; set; }
	IReactiveCollection<HeroData> HeroData { get; set; }
	List<HeroSkillData> HeroSkillData { get; set; }
	IReactiveCollection<string> BuyInappIds { get; set; }
	IReactiveCollection<int> CurrentStageHeroIdxs { get; set; }
	IReactiveProperty<int> Medal { get; set; }
	IReactiveProperty<int> ButterFlyTicket { get; set; }
	IReactiveProperty<int> SilverDump { get; set; }
	IReactiveProperty<int> GoldDump { get; set; }
	IReactiveCollection<EventPassRewardData> EventPassRewardData { get; set; }
	public IReactiveProperty<int> RemainSpecialMissionCount { get; set; }
	List<QuestData> QuestData { get; set; }
	List<RestQuestData> RestQuestData { get; set; }
	List<StageClearData> StageClearData { get; set; }
	List<MagicPackageData> MagicPackageData { get; set; }
	List<EventMainRewardData> EventMainRewardData { get; set; }
	List<ProductData> ProductData { get; set; }
	int[] QuestActiveOrders {get; set;}
	StageData StageData { get; set; }
	ShopData ShopData { get; set; }
	HireCardData HierCardDatas { get; set; }
	DateTime LastLoginTime { get; set; }
	DateTime BoostEndTime { get; set; }
	PentHouseData PenthouseData { get; set; }
	AdFlySkinData AdFlySkinData { get; set; }
	CompetitionData CompetitionData { get; set; }
	//int FeverGage {get; set;}
	//ForEvent Only
	EventData EventData { get; set; }
	ExpeditionData ExpeditionData { get; set; }
	GrowthCabinetData GrowthCabinetData { get; set; }
	DragonHuntData DragonHuntData { get; set; }
	TicketShopData TicketShopData { get; set; }
	EnergyData EnergyData { get; set; }
	SkinChangeData SkinChangeData { get; set; }
	List<TicketData> TicketData { get; set; }
	MonsterBuffData MonsterBuffData { get; set; }
	IReactiveCollection<DragonCaveData> DragonCaveData { get; set; }
	AttendanceData AttendanceData { get; set; }
	DropBoxData DropBoxData { get; set; }
	//int EventIdx {get; set;}
	//IReactiveProperty<BigInteger> EventQuestValue { get; set; }
	//IReactiveProperty<int> EventQuestLevel { get; set; }
	//int ReceiveEventQuestLevel { get; set; }
}

public class UserDataMain : IUserDataMode
{
	public IReactiveProperty<BigInteger> Money { get; set; } = new ReactiveProperty<BigInteger>(0);
    public IReactiveProperty<BigInteger> StoreMoney { get; set; } = new ReactiveProperty<BigInteger>(0);
	public IReactiveProperty<int> Material { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> EnergyExp { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> EnergyGem { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveCollection<int> CurrentStageHeroIdxs { get; set; } = new ReactiveCollection<int>();
	public IReactiveCollection<ManagerData> ManagerData {get; set;} = new ReactiveCollection<ManagerData>();
	public IReactiveCollection<ItemData> ItemData { get; set; } = new ReactiveCollection<ItemData>();
	public IReactiveCollection<HeroData> HeroData { get; set; } = new ReactiveCollection<HeroData>();
	public List<HeroSkillData> HeroSkillData { get; set; } = new List<HeroSkillData>();
	public IReactiveCollection<string> BuyInappIds {get; set;} = new ReactiveCollection<string>();
	public IReactiveCollection<EventPassRewardData> EventPassRewardData { get; set; } = new ReactiveCollection<EventPassRewardData>();
	public IReactiveCollection<DragonCaveData> DragonCaveData { get; set; } = new ReactiveCollection<DragonCaveData>();
	public IReactiveProperty<int> Medal { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> ButterFlyTicket { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> RemainSpecialMissionCount { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> SilverDump { get; set; } = new ReactiveProperty<int>(0);
	public IReactiveProperty<int> GoldDump { get; set; } = new ReactiveProperty<int>(0);
	public List<QuestData> QuestData {get; set;} = new List<QuestData>();
	public List<RestQuestData> RestQuestData { get; set; } = new List<RestQuestData>();
	public List<MagicPackageData> MagicPackageData { get; set; } = new List<MagicPackageData>();
	public List<ProductData> ProductData { get; set; } = new List<ProductData>();
	public List<EventMainRewardData> EventMainRewardData { get; set; } = new List<EventMainRewardData>();
	public int[] QuestActiveOrders {get; set;} = new int[3] {-1,-1,-1};
	public StageData StageData { get; set; } = new StageData();
	public ShopData ShopData { get; set;} = new ShopData();
	public HireCardData HierCardDatas { get; set; } = new HireCardData();
	public BigInteger InBusMoney { get; set; }
	public DateTime LastLoginTime { get; set; } = default(DateTime);
	public DateTime BoostEndTime { get; set; } = default(DateTime);
	public PentHouseData PenthouseData { get; set; } = new PentHouseData();
	public GrowthCabinetData GrowthCabinetData { get; set; } = new GrowthCabinetData();
	public CompetitionData CompetitionData { get; set; } = new CompetitionData();
	public List<StageClearData> StageClearData { get; set; } = new List<StageClearData>();
	public AdFlySkinData AdFlySkinData { get; set; } = new AdFlySkinData();
	public EnergyData EnergyData { get; set; } = new EnergyData();
	public ExpeditionData ExpeditionData { get; set; } = new ExpeditionData();
	public TicketShopData TicketShopData { get; set; } = new TicketShopData();
	public SkinChangeData SkinChangeData { get; set; } = new SkinChangeData();
	public List<TicketData> TicketData { get; set; } = new List<TicketData>();
	public MonsterBuffData MonsterBuffData { get; set; } = new MonsterBuffData();
	public DragonHuntData DragonHuntData { get; set; } = new DragonHuntData();
	public AttendanceData AttendanceData { get; set; } = new AttendanceData();
	public DropBoxData DropBoxData { get; set; } = new DropBoxData();
	//public int FeverGage {get; set;} = 0;

	//ForEvent Only
	public EventData EventData { get; set; } = new EventData();
	//public int EventIdx { get; set; } = 0;
	//public IReactiveProperty<BigInteger> EventQuestValue { get; set; } = new ReactiveProperty<BigInteger>(0);
	//public IReactiveProperty<int> EventQuestLevel { get; set; } = new ReactiveProperty<int>(0);
	//public int ReceiveEventQuestLevel { get; set; } = 0;
}

public class UserDataEvent : UserDataMain
{
}