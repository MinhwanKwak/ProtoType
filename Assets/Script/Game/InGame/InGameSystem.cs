using System.Collections.Generic;
using System.Linq;
using BanpoFri;
using UnityEngine;
public enum InGameType
{
    Main,
    Event,
}

public class InGameSystem
{
    public SceneSystem SceneSystem { get; private set; } = new SceneSystem();
    public InGameMode CurInGame {get; private set;} = null;

    private bool firstInit = false;
	private bool IsActiveEventTuto = false;
	private bool IsTitle = false;

	private System.Action NextStageAction = null;
	private System.Action NextStageCloseAction = null;
    public T GetInGame<T>() where T : InGameMode
    {
        return CurInGame as T;
    }

    public void RegisteInGame(InGameMode mode)
	{
		CurInGame = mode;
	}
    public void ChangeMode(InGameType type)
    {
       // Tables.Instance.GetTable<FacilityUpgrade>().CalculateUpgradeTable(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);
        System.GC.Collect();
        StartGame(type, () => {
            firstInit = false;
        });
    }
    private void StartGame(InGameType type, System.Action loadCallback = null)
    {
        //if (!firstInit) GameRoot.Instance.Title.Show();
        //else GameRoot.Instance.Loading.Show(true);
        SceneSystem.ChangeScene(type, loadCallback);
    }

    //    public void ChangeMode(InGameType type)
    //    {   
    //        Tables.Instance.GetTable<FacilityUpgrade>().CalculateUpgradeTable(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);
    //        System.GC.Collect();
    //        StartGame(type, ()=> {
    //			firstInit = false;
    //		});
    //    }

    //	public void InitPopups()
    //	{
    //		var curStageIdx = GameRoot.Instance.UserData.CurMode.StageData.StageIdx;
    //		var ActionQueue = new Queue<System.Action>();
    //		IsActiveEventTuto = false;


    //		var time = GameRoot.Instance.UserData.CurMode.LastLoginTime;

    //		System.Action NextAction = () =>
    //		{
    //			if (ActionQueue.Count < 1)
    //			{
    //				GameRoot.Instance.MonsterBuffSystem.bUpdate = true;

    //				return;
    //			}

    //			var action = ActionQueue.Dequeue();
    //			action.Invoke();
    //		};

    //		if (!firstInit && GameRoot.Instance.CurInGameType == InGameType.Main && !IsTitle)
    //		{
    //			IsTitle = true;

    //			GameRoot.Instance.Title.Play(()=> {
    //				NextAction();
    //				if (GameRoot.Instance.TitleCloseActions.Count > 0)
    //                {
    //					for(int i = 0; i < GameRoot.Instance.TitleCloseActions.Count; ++i)
    //						GameRoot.Instance.TitleCloseActions.Dequeue()?.Invoke();
    //				}
    //			});
    //		}
    //		else
    //		{
    //			GameRoot.Instance.Loading.Hide(false);
    //			GameRoot.Instance.BgmOn();
    //		}



    //        if (curStageIdx == 1)
    //        {
    //            ActionQueue.Enqueue(() =>
    //            {
    //                GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () =>
    //                {
    //                    NextAction();
    //                };

    //                GameRoot.Instance.TutorialSystem.StartTutorial("1");
    //            });
    //        }

    //        if (curStageIdx == Tables.Instance.GetTable<ContentsOpen>().GetData(Config.ContentsOpenType.tuto_popup.ToString()).condition_stage)
    //		{
    //			if (!GameRoot.Instance.TutorialSystem.IsClearTuto("1101"))
    //			{

    //				//GameRoot.Instance.InGameSystem.GetInGame<InGameTycoon>().MaxMode.Value = true;
    //				//int index = System.Convert.ToInt16(true);
    //				//PlayerPrefs.SetInt(ProjectUtility.SAVE_MAX_TOGGLE, index);


    //				GameRoot.Instance.PluginSystem.AnalyticsProp.EventForAppQuantum("install");

    //				//logs
    //				List<TpParameter> parameters = new List<TpParameter>();
    //				GameRoot.Instance.PluginSystem.AnalyticsProp.AllEvent(IngameEventType.None,
    //					"install", parameters);



    //				ActionQueue.Enqueue(() =>
    //				{
    //					GameRoot.Instance.UISystem.OpenUI<PopupFirstStageStart>(popup => popup.Init(), () => { NextAction(); });
    //				});
    //			}

    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () =>
    //				{
    //					NextAction();
    //					GameRoot.Instance.UISystem.OpenUI<PagePlayGuide>(popup=> { GameRoot.Instance.StartCoroutine(popup.StartGuide(PagePlayGuide.GuidType.StageFirst)); });				
    //				};
    //				GameRoot.Instance.TutorialSystem.StartTutorial("1101");
    //			});
    //		}

    //		if (GameRoot.Instance.UserData.CurMode.StageData.StageIdx == 2)
    //		{
    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () => {
    //					NextAction();
    //					GameRoot.Instance.UISystem.OpenUI<PagePlayGuide>(popup => { GameRoot.Instance.StartCoroutine(popup.StartGuide(PagePlayGuide.GuidType.StageSecond)); });
    //				};
    //				GameRoot.Instance.TutorialSystem.StartTutorial("1106");
    //			});
    //		}

    //		if (ProjectUtility.ContentsOpenCheck(Config.ContentsOpenType.expedition_conquest) && !GameRoot.Instance.TutorialSystem.IsClearTuto("1100"))
    //		{
    //			if (!GameRoot.Instance.UserData.CurMode.ExpeditionData.MiddleRewardData.IsMiddleReward.Value)
    //			{
    //				ActionQueue.Enqueue(() =>
    //				{
    //					GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () => { NextAction(); };
    //					GameRoot.Instance.TutorialSystem.StartTutorial("1100");
    //				});
    //			}
    //			else
    //            {
    //				GameRoot.Instance.TutorialSystem.TutoClear("1100");
    //			}
    //		}

    //        if (GameRoot.Instance.CurInGameType == InGameType.Main)
    //        {
    //            if (GameRoot.Instance.AttendanceSystem.CheckAttendance())
    //            {
    //                ActionQueue.Enqueue(() =>
    //                {
    //                    GameRoot.Instance.UISystem.OpenUI<PopupMission>(popup => { popup.Show(MissionTab.Attendance); }, NextAction,false);
    //                });
    //            }
    //        }

    //        if (!firstInit)
    //        {
    //			if (!GameRoot.Instance.TutorialSystem.IsActive() && !time.Equals(default(System.DateTime)))
    //			{
    //				var diff = TimeSystem.GetCurTime().Subtract(time);
    //				var minRewardTime = Tables.Instance.GetTable<Define>().GetData("offline_bonus_min_time").value;
    //				var maxRewardTime = Tables.Instance.GetTable<Define>().GetData("offline_bonus_max_time").value;

    //				if (diff.TotalSeconds > minRewardTime)
    //				{
    //					int rewardTime = (int)diff.TotalSeconds;
    //					if ((int)diff.TotalSeconds >= maxRewardTime)
    //					{
    //						rewardTime = maxRewardTime;

    //						ActionQueue.Enqueue(() =>
    //						{
    //							if (!IsActiveEventTuto)
    //								GameRoot.Instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set(maxRewardTime), () => NextAction(), true, ProjectUtility.GetRunStage()); //offline max value
    //						});
    //					}
    //					else
    //					{
    //						ActionQueue.Enqueue(() =>
    //						{
    //							if (!IsActiveEventTuto)
    //								GameRoot.Instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set((int)diff.TotalSeconds), () => NextAction(), true, ProjectUtility.GetRunStage()); //offline not max value
    //						});
    //					}

    //				}
    //			}
    //		}

    //		if (GameRoot.Instance.CurInGameType == InGameType.Main)
    //		{
    //			if (GameRoot.Instance.UserData.CurEventData.EventMainRewardData.Count > 0)
    //			{
    //				ActionQueue.Enqueue(() =>
    //				{
    //					GameRoot.Instance.WaitTimeAndCallback(0.5f, () => { GameRoot.Instance.UISystem.OpenUI<PopupEventMainReward>(popup => popup.Init(false), ()=> NextAction()); });
    //				});
    //			}
    //		}

    //		//if (GameRoot.Instance.BannerSystem.Inhouse_banner_open_stage <= curStageIdx &&
    //		//		GameRoot.Instance.BannerSystem.EnableReceiveRewardProperty.Value &&
    //		//		GameRoot.Instance.CurInGameType == InGameType.Main && GameRoot.Instance.Title.gameObject.activeSelf)
    //		//{
    //		//	ActionQueue.Enqueue(() =>
    //		//	{
    //		//		GameRoot.Instance.UISystem.OpenUI<PopupOneLink>(null, () => NextAction());
    //		//	});
    //		//}

    //		bool showPackage = false;
    //		List<PackageData> nextpackagelist = new List<PackageData>();
    //		foreach(var nextpackage in GameRoot.Instance.UserData.CurMode.ShopData.packageDatas)
    //        {
    //			var td = Tables.Instance.GetTable<ShopCabinet>().GetData(nextpackage.PackageIdx);
    //			if (td == null)
    //				continue;
    //			if (nextpackage.BuyCntProperty.Value == 0)
    //				continue;
    //			if (nextpackage.StartTime == default(System.DateTime) && td.type == 1)
    //				continue;
    //			if (nextpackage.PackagedRewardGroup == -1)
    //				continue;

    //			showPackage = true;
    //			break;
    //		}

    //		if (GameRoot.Instance.CurInGameType == InGameType.Main)
    //		{
    //			if (showPackage && !ShopSystem.IsMainShowPackage)
    //			{
    //				ActionQueue.Enqueue(() =>
    //				{
    //					ShopSystem.IsMainShowPackage = true;
    //					GameRoot.Instance.UISystem.OpenUI<PopupPackage>(popup => { }/*popup.Set(packageidx, lefthud.ShopPackageInit, NextAction)*/, NextAction, false);
    //				});
    //			}
    //		}

    //		if (GameRoot.Instance.CurInGameType == InGameType.Event)
    //		{
    //			if (showPackage && !ShopSystem.IsEventShowPackage)
    //			{
    //				ActionQueue.Enqueue(() =>
    //				{
    //					ShopSystem.IsEventShowPackage = true;
    //					GameRoot.Instance.UISystem.OpenUI<PopupPackage>(popup => { }/*popup.Set(packageidx, lefthud.ShopPackageInit, NextAction)*/, NextAction , false);
    //				});
    //			}


    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.UISystem.OpenUI<PopupEventPass>(popup => {
    //					popup.Init();
    //					GameRoot.Instance.TutorialSystem.StartTutorial("1009");
    //				}, () => NextAction());
    //			});
    //		}


    //		if (ProjectUtility.ContentsOpenCheck(Config.ContentsOpenType.seasonal_event) &&
    //						GameRoot.Instance.EventSystem.CurEventStatus.Value == (int)EventSystem.Status.Starting)
    //		{
    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () =>
    //				{
    //					NextAction();
    //				};
    //				GameRoot.Instance.TutorialSystem.StartTutorial("1008");
    //			});
    //		}


    //		if (!GameRoot.Instance.TutorialSystem.IsClearTuto("1108") && ProjectUtility.ContentsOpenCheck(Config.ContentsOpenType.dragon_cave))
    //		{
    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () => { NextAction(); };

    //				if (GameRoot.Instance.CurInGameType == InGameType.Main && !GameRoot.Instance.TutorialSystem.IsActive())
    //				{
    //				    GameRoot.Instance.UserData.SetReward((int)Config.RewardGroup.DragonCave,  3, 1);
    //					GameRoot.Instance.TutorialSystem.StartTutorial("1108");
    //				}
    //			});
    //		}




    //		if (!GameRoot.Instance.TutorialSystem.IsClearTuto("1109") && ProjectUtility.ContentsOpenCheck(Config.ContentsOpenType.dragon_hunt))
    //		{
    //			ActionQueue.Enqueue(() =>
    //			{
    //				GameRoot.Instance.TutorialSystem.OnActiveTutoEnd = () => { NextAction(); };

    //				if(GameRoot.Instance.CurInGameType == InGameType.Main && !GameRoot.Instance.TutorialSystem.IsActive())
    //				GameRoot.Instance.TutorialSystem.StartTutorial("1109");
    //			});
    //		}


    //		if (!firstInit)
    //		{
    //			firstInit = true;
    //		}



    //		if (!GameRoot.Instance.Title.gameObject.activeSelf)
    //			NextAction();
    //	}

    //    public void NextGameStage(bool Init = false)
    //	{
    //		GameRoot.Instance.MonsterBuffSystem.bUpdate = false;

    //		if (GameRoot.Instance.InGameSystem.CurInGame != null)
    //        {
    //			GameRoot.Instance.InGameSystem.CurInGame.UnLoad();
    //		}

    //		var saveTime = TimeSystem.GetCurTime().Ticks;
    //        var curidx = GameRoot.Instance.UserData.CurMode.StageData.StageIdx;

    //		if (!Init)
    //        {
    //			var questcount = Tables.Instance.GetTable<StageQuest>().DataList.FindAll(x => x.stage == curidx).Count;
    //			var clearquestcount = GameRoot.Instance.UserData.CurMode.QuestData.FindAll(x => x.ReceviedProperty.Value).Count;

    //			//logs
    //			List<TpParameter> parameters = new List<TpParameter>();
    //			parameters.Add(new TpParameter("lv", curidx));
    //			parameters.Add(new TpParameter("questcount", GameRoot.Instance.UserData.CurMode.QuestData.FindAll(x => x.ReceviedProperty.Value).Count));
    //			parameters.Add(new TpParameter("time", ProjectUtility.GetDiffTimeByStageStartTime()));
    //			GameRoot.Instance.PluginSystem.AnalyticsProp.AllEvent(IngameEventType.None,
    //				"m_stage_clear", parameters);

    //			GameRoot.Instance.UserData.CurMode.StageData.UpgradeStageIdx();
    //            curidx = GameRoot.Instance.UserData.CurMode.StageData.StageIdx;
    //			Tables.Instance.GetTable<FacilityUpgrade>().CalculateUpgradeTable(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);

    //			var heros = GameRoot.Instance.UserData.CurMode.HeroData;
    //			List<TpParameter> parameters1 = new List<TpParameter>();
    //			parameters1.Add(new TpParameter("type", 0));
    //			parameters1.Add(new TpParameter("lv", curidx));
    //			for (int i = 1; i <= 5; i++)
    //			{
    //				parameters1.Add(new TpParameter($"solider_{i}", heros.Count >= i ? heros[0].Lv : 0));
    //			}
    //			GameRoot.Instance.PluginSystem.AnalyticsProp.AllEvent(IngameEventType.None,
    //				"m_solider_lv", parameters1);
    //		}
    //		else
    //        {
    //			//GameRoot.Instance.HireSystem.HireDataInit();
    //		}
    //		PlayerPrefs.SetString(ProjectUtility.SAVE_STAGE_CLEAR_TIME_KEY, saveTime.ToString());
    //		GameRoot.Instance.QuestSystem.ClearStage(curidx);
    //        var stageDatas = Tables.Instance.GetTable<FacilityStage>().DataList.FindAll(x => x.stage == curidx);        
    //        foreach(var floor in stageDatas)
    //        {
    //            var opend = floor.facility_floor == 0 || floor.facility_floor == 99 ? true : false;
    //            var fd = new FacilityData(floor.facility_idx, opend, 1, 1, -1, -1, -1, 0);
    //            GameRoot.Instance.UserData.CurMode.StageData.FacilityState.Add(floor.facility_idx, fd);
    //        }

    //		ShopSystem.IsMainShowPackage = false;
    //		GameRoot.Instance.UserData.SyncHUDCurrency();
    //		GameRoot.Instance.UserData.CurMode.StageData.Init(curidx);
    //        //GameRoot.Instance.ShopSystem.NextStageAssignPackage(curidx);
    //        GameRoot.Instance.BuffSystem.Create();
    //        GameRoot.Instance.QuestSystem.Create();
    //		GameRoot.Instance.ShopSystem.Create();
    //        GameRoot.Instance.GameNotification.Create();
    //		GameRoot.Instance.AchievementsSystem.Create();
    //		GameRoot.Instance.MainPassSystem.Create();
    //		GameRoot.Instance.DayworkSystem.Create();
    //		GameRoot.Instance.FlyADSystem.AdFlyShowUpStage();
    //		GameRoot.Instance.EnergySystem.StartEnergyEventByStage();
    //		GameRoot.Instance.EventSystem.Create();
    //		SoundPlayer.Instance.Init();
    //		GameRoot.Instance.MysteryBoxSystem.MysteryBoxShowUpStage();
    ////		GameRoot.Instance.TicketShopSystem.CheckByStage();
    //		GameRoot.Instance.NavigatorSystem.Clear();
    //        GameRoot.Instance.TutorialSystem.ClearRegisiter();
    //        GameRoot.Instance.UserData.CurMode.Money.Value = 0;
    //		GameRoot.Instance.UserData.CurMode.StoreMoney.Value = 0;
    //		GameRoot.Instance.UserData.CurMode.Medal.Value = 0;
    //		GameRoot.Instance.MonsterBuffSystem.UpdateReduceTime();
    //		GameRoot.Instance.AttendanceSystem.UpdateTargetAttendance();
    //		GameRoot.Instance.ShopSystem.AssignPackageOpenCheck();
    //		//GameRoot.Instance.UserData.Save();


    //		if (GameRoot.Instance.UserData.CurMode.StageData.StageIdx != 1)
    //		{
    //			NextStageAction = () =>
    //			{

    //				GameRoot.Instance.WaitTimeAndCallback(2f, () =>
    //				{
    //					SoundPlayer.Instance.PlaySound("effect_intro_chicken");
    //					GameRoot.Instance.InGameSystem.GetInGame<InGameTycoon>().curInGameStage.FacilityStart.ActiveMorningShinyEffect(true);
    //					NextStageCloseAction?.Invoke();
    //					NextStageCloseAction = null;
    //				});
    //			};
    //		}




    //		if (!Init)
    //        {
    //            StartGame(GameRoot.Instance.CurInGameType);

    //			if (curidx == Tables.Instance.GetTable<ContentsOpen>().GetData(Config.ContentsOpenType.expedition_conquest.ToString()).condition_stage)
    //			{
    //				GameRoot.Instance.GameNotification.AddOnceNotiRegister(GameNotificationSystem.NotificationCategory.HudBottomExpedition.ToString());
    //				GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.HudBottomExpedition);
    //			}
    //		}
    //		if (curidx >= 5)
    //		{
    //			GameRoot.Instance.GameNotification.AddOnceNotiRegister(GameNotificationSystem.NotificationCategory.Competition.ToString());
    //			GameRoot.Instance.GameNotification.UpdateNotification(GameNotificationSystem.NotificationCategory.Competition);
    //		}


    //    }

    //	public void ChangeInGameMode(InGameType type)
    //    {
    //		GameRoot.Instance.MonsterBuffSystem.bUpdate = false;

    //		if (GameRoot.Instance.InGameSystem.CurInGame != null)
    //			GameRoot.Instance.InGameSystem.CurInGame.UnLoad();

    //		GameRoot.Instance.UserData.CurMode.LastLoginTime = TimeSystem.GetCurTime();
    //		//GameRoot.Instance.UserData.Save(true);

    //        GameRoot.Instance.UserData.ChangeDataMode(type == InGameType.Main ? DataState.Main : DataState.Event);

    //		if (type == InGameType.Event && GameRoot.Instance.UserData.CurEventData.EventData.EventStatusData.IsNewEvent)
    //		{
    //			GameRoot.Instance.UserData.CurEventData.EventData.EventStatusData.IsNewEvent = false;
    //			NewEventStage();


    //			var recordkey = ProjectUtility.GetRecordCountText(Config.RecordCountKeys.SeasonalJoin);
    //			GameRoot.Instance.UserData.AddRecordCount(Config.RecordCountKeys.SeasonalJoin, 1);

    //			List<TpParameter> parameters = new List<TpParameter>();
    //			parameters.Add(new TpParameter("season", GameRoot.Instance.UserData.CurEventData.EventData.EventStatusData.CurEventIdx));
    //			parameters.Add(new TpParameter("count", GameRoot.Instance.UserData.RecordCount[recordkey]));
    //			GameRoot.Instance.PluginSystem.AnalyticsProp.AllEvent(IngameEventType.None,
    //				"m_season_enter", parameters);
    //		}


    //		Tables.Instance.GetTable<FacilityUpgrade>().CalculateUpgradeTable(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);
    //		GameRoot.Instance.ChangeIngameType(type);




    //		GameRoot.Instance.HeroSkillSystem.Init();
    //		GameRoot.Instance.HeroSystem.Init();
    //		GameRoot.Instance.ShopSystem.Init();
    //		GameRoot.Instance.InGameDragonSystem.Init();
    //		GameRoot.Instance.DragonCaveSystem.Init();
    //		GameRoot.Instance.HeroSkillSystem.Create();
    //		GameRoot.Instance.EventSystem.Create();
    //		GameRoot.Instance.EnergySystem.Create();
    //		GameRoot.Instance.DragonHuntSystem.Create();
    //		GameRoot.Instance.BuffSystem.Create();
    //		GameRoot.Instance.ShopSystem.Create();
    //		GameRoot.Instance.QuestSystem.Create();
    //		GameRoot.Instance.GameNotification.Create();
    //		GameRoot.Instance.MainPassSystem.Create();
    //		GameRoot.Instance.AchievementsSystem.Create();
    //		GameRoot.Instance.DayworkSystem.Create();
    //		GameRoot.Instance.FlyADSystem.AdFlyShowUpStage();
    //		GameRoot.Instance.MysteryBoxSystem.MysteryBoxShowUpStage();
    //		GameRoot.Instance.MysteryBoxSystem.InitMysteryBoxCoolTime();
    ////		GameRoot.Instance.TicketShopSystem.CheckByStage();
    //		GameRoot.Instance.NavigatorSystem.Clear();
    //		GameRoot.Instance.TutorialSystem.ClearRegisiter();
    //		GameRoot.Instance.ShopSystem.ChangeMode();
    //		GameRoot.Instance.expeditionsystem.Init();
    //		GameRoot.Instance.TicketSystem.Init();
    //		GameRoot.Instance.MonsterBuffSystem.Init();
    //		GameRoot.Instance.AttendanceSystem.UpdateTargetAttendance();
    //		GameRoot.Instance.UserData.CurMainData.AdFlySkinData.Create();

    //		SoundPlayer.Instance.Init();

    //		//GameRoot.Instance.UserData.Save();
    //	}

    //	//public void ChangeEventMode()	
    // //   {
    //	//	if (GameRoot.Instance.CurInGameType == InGameType.Event) return;

    //	//	GameRoot.Instance.UserData.CurMode.LastLoginTime = TimeSystem.GetCurTime();
    //	//	GameRoot.Instance.UserData.Save(true);

    //	//	GameRoot.Instance.UserData.ChangeDataMode(DataState.Event);
    //	//	if (GameRoot.Instance.UserData.CurMode.EventData.EventIdx != GameRoot.Instance.EventSystem.CurStartedEventIdx.Value)
    //	//		NewEventStage();
    //	//	GameRoot.Instance.ChangeIngameType(InGameType.Event);
    // //   }

    //	public void NewEventStage()
    //    {
    //		var eventIdx = GameRoot.Instance.UserData.CurEventData.EventData.EventStatusData.CurEventIdx;
    //		var stageIdx = Tables.Instance.GetTable<SeasonalEvent>().GetData(eventIdx).stage;


    //		GameRoot.Instance.UserData.CurMode.StageData.SetStageIdx(stageIdx);
    //        GameRoot.Instance.QuestSystem.ClearStage(stageIdx);
    //        var stageDatas = Tables.Instance.GetTable<FacilityStage>().DataList.FindAll(x => x.stage == stageIdx);
    //        foreach (var floor in stageDatas)
    //        {
    //            var opend = floor.facility_floor == 0 || floor.facility_floor == 99 ? true : false;
    //            var fd = new FacilityData(floor.facility_idx, opend, 1, 1, -1, -1, -1, 0);
    //            GameRoot.Instance.UserData.CurMode.StageData.FacilityState.Add(floor.facility_idx, fd);
    //        }

    //        var rouletteFacility = new FacilityData(Config.RouletteIdx, true, 1, 1, -1, -1, -1, 0);
    //        GameRoot.Instance.UserData.CurMode.EventData.RouletteData = new RouletteData(rouletteFacility, 0, 0);

    //        GameRoot.Instance.UserData.CurMode.EventData.SetEventIdx(eventIdx);
    //        GameRoot.Instance.UserData.CurMode.StageData.Init(stageIdx);
    //        //GameRoot.Instance.ShopSystem.AssignPackage(stageIdx);
    //        //GameRoot.Instance.ShopSystem.EventPass = false;
    //        //GameRoot.Instance.ShopSystem.EventPassPremium = false;

    //        GameRoot.Instance.UserData.CurMode.Material.Value = 0;
    //        GameRoot.Instance.UserData.CurMode.Money.Value = 0;
    //        GameRoot.Instance.UserData.CurMode.StoreMoney.Value = 0;
    //		GameRoot.Instance.UserData.CurEventData.EventPassRewardData.Clear();
    //		GameRoot.Instance.UserData.CurEventData.EventMainRewardData.Clear();
    //		GameRoot.Instance.UserData.CurEventData.ManagerData.Clear();
    //		GameRoot.Instance.UserData.CurEventData.LastLoginTime = default(System.DateTime);


    //		//GameRoot.Instance.UserData.Save();
    //    }

    //	public void ClearEventStage()
    //	{
    //		GameRoot.Instance.UserData.CurEventData.ShopData.packageDatas.Clear();
    //		GameRoot.Instance.UserData.CurEventData.ItemData.Clear();
    //		//GameRoot.Instance.UserData.CurEventData.ExchangeData.Clear();

    //		//GameRoot.Instance.UserData.Save();
    //	}

    //	public void CheatGoToGameStage(int stageIdx)
    //    {

    //		GameRoot.Instance.QuestSystem.EndRestQuestAdd(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);
    //		if (GameRoot.Instance.InGameSystem.CurInGame != null)
    //            GameRoot.Instance.InGameSystem.CurInGame.UnLoad();

    //		var saveTime = TimeSystem.GetCurTime().Ticks;
    //        GameRoot.Instance.UserData.CurMode.StageData.SetStageIdx(stageIdx);
    //		var curidx = GameRoot.Instance.UserData.CurMode.StageData.StageIdx;
    //		Tables.Instance.GetTable<FacilityUpgrade>().CalculateUpgradeTable(GameRoot.Instance.UserData.CurMode.StageData.StageIdx);
    //		GameRoot.Instance.QuestSystem.ClearStage(curidx);
    //        var stageDatas = Tables.Instance.GetTable<FacilityStage>().DataList.FindAll(x => x.stage == curidx);        
    //        foreach(var floor in stageDatas)
    //        {
    //            var opend = floor.facility_floor == 0 || floor.facility_floor == 99 ? true : false;
    //            var fd = new FacilityData(floor.facility_idx, opend, 1, 1, -1, -1, -1,0);
    //            GameRoot.Instance.UserData.CurMode.StageData.FacilityState.Add(floor.facility_idx, fd);
    //		}
    //		ShopSystem.IsMainShowPackage = false;
    //		GameRoot.Instance.UserData.SyncHUDCurrency();
    //		GameRoot.Instance.UserData.CurMode.StageData.Init(curidx);
    //        //GameRoot.Instance.ShopSystem.NextStageAssignPackage(curidx);
    //        GameRoot.Instance.BuffSystem.Create();
    //		GameRoot.Instance.EventSystem.Create();
    //		GameRoot.Instance.ShopSystem.Create();
    //		GameRoot.Instance.QuestSystem.Create();
    //		GameRoot.Instance.EnergySystem.StartEnergyEventByStage();
    //		GameRoot.Instance.MainPassSystem.Create();
    //		GameRoot.Instance.GameNotification.Create();
    //		GameRoot.Instance.AchievementsSystem.Create();
    //		GameRoot.Instance.MysteryBoxSystem.MysteryBoxShowUpStage();
    ////		GameRoot.Instance.TicketShopSystem.CheckByStage();
    //		GameRoot.Instance.DayworkSystem.Create();
    //		GameRoot.Instance.NavigatorSystem.Clear();
    //        GameRoot.Instance.TutorialSystem.ClearRegisiter();
    //		GameRoot.Instance.AttendanceSystem.UpdateTargetAttendance();
    //		GameRoot.Instance.UserData.CurMode.Money.Value = 0;
    //		GameRoot.Instance.UserData.CurMode.StoreMoney.Value = 0;

    //		SoundPlayer.Instance.Init();
    //		GameRoot.Instance.ShopSystem.AssignPackageOpenCheck();
    //		//GameRoot.Instance.UserData.Save();
    //        StartGame(GameRoot.Instance.CurInGameType);
    //    }
}
