using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using BanpoFri;

public class GameRoot : Singleton<GameRoot>
{
	[SerializeField]
	private Transform MainUITrans;
	[SerializeField]
	private Transform HUDUITrans;
	[SerializeField]
	public Canvas MainCanvas;
	[SerializeField]
	private Canvas WorldCanvas;
	[SerializeField]
	private GameObject CheatWindow;

	public RectTransform GetMainCanvasTR { get { return MainCanvas.transform as RectTransform; } }
	public UISystem UISystem { get; private set; } = new UISystem();
	public UserDataSystem UserData { get; private set; } = new UserDataSystem();
	public InGameSystem InGameSystem { get; private set; } = new InGameSystem();
	public SnapshotCamera SnapshotCam { get; private set; } = null;


	public GameObject UILock;
	private static bool InitTry = false;
	public static bool LoadComplete { get; private set; } = false;
	private float deltaTime = 0f;
	public InGameType CurInGameType { get; private set; } = InGameType.Main;
	private Queue<System.Action> TouchStartActions = new Queue<System.Action>();
	public Queue<System.Action> TitleCloseActions = new Queue<System.Action>();

	public static bool IsInit()
	{
		if (instance != null && !InitTry)
			Load();

		return instance != null;
	}

	public static void Load()
	{
		InitTry = true;
		Addressables.InstantiateAsync("GameRoot").Completed += (handle) => {
			instance = handle.Result.GetComponent<GameRoot>();
			instance.name = "GameRoot";
		};
	}

	public void AddTouchAction(System.Action cb)
	{
		TouchStartActions.Enqueue(cb);
	}

	public void AddTitleCloseAction(System.Action cb)
	{
		TitleCloseActions.Enqueue(cb);
	}



	private void Update()
	{
		if (!LoadComplete)
			return;

		UserData.Update();

		//SkinChangeSystem.Update();
		//GrowthSystem.Update();
		//StageClearSystem.Update();
		//HireSystem.Update();
		//FeverSystem.Update();
		//ExchangeSysytem.Update();
		if (deltaTime >= 1f) // one seconds updates;
		{
			//BuffSystem.UpdateOneSeconds();
			//TimerSystem.UpdateOneSeconds();
			//BoostSystem.UpdateOneSeconds();
			//EventSystem.UpdateOneSeconds();
			//DayworkSystem.UpdateOneSeconds();
			//MysteryBoxSystem.UpdateOneSecondsHoldingTime();
			////			TicketShopSystem.UpdateOneSeconds();
			//TicketSystem.UpdateOneSeconds();
			//MonsterBuffSystem.UpdateOneSeconds();
			deltaTime -= 1f;
		}
		deltaTime += Time.deltaTime;

		if (Input.GetMouseButtonUp(0))
		{
			if (InGameSystem.CurInGame != null)
			{
				Vector2 localPos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, Input.mousePosition,
					MainCanvas.worldCamera, out localPos);

			
				if (!IsPointerOverUIObject(Input.mousePosition))
				{
					//if (UISystem.GetUI<HUDBottom>() != null)
					//{
					//	//if (UISystem.GetUI<HUDBottom>().isFacilityInfoShow())
					//	//	UISystem.GetUI<HUDBottom>().HideFacilityInfo();
					//}
				}


				if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null)
				{


					if (TouchStartActions.Count > 0)
					{
						var cnt = TouchStartActions.Count;
						for (int i = 0; i < cnt; i++)
						{
							TouchStartActions.Dequeue().Invoke();
						}
					}
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			//if (TutorialSystem.IsActive())
			//	return;

			var uibase = UISystem.GetOpenPopupLastUI();
			if (uibase != null)
			{
				UISystem.ClosePopupBackBtn();
			}
			else
			{
				
			}
		}
#if UNITY_EDITOR
		//if(Input.GetKeyDown(KeyCode.S))
		//      {
		//	if (GameRoot.Instance.UISystem.GetUI<PopupCompetitionResult>() != null &&
		//		GameRoot.Instance.UISystem.GetUI<PopupCompetitionResult>().gameObject.activeSelf) return;
		//	GameRoot.Instance.UISystem.OpenUI<PopupCompetitionResult>(popup =>
		//		popup.SetTestMode()
		//		);
		//      }
#endif
	}

	public bool IsPointerOverUIObject(Vector2 touchPos)
	{
		UnityEngine.EventSystems.PointerEventData eventDataCurrentPosition
			= new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);

		eventDataCurrentPosition.position = touchPos;

		List<UnityEngine.EventSystems.RaycastResult> results = new List<UnityEngine.EventSystems.RaycastResult>();


		UnityEngine.EventSystems.EventSystem.current
		.RaycastAll(eventDataCurrentPosition, results);

		return results.Count > 0;
	}

	public void DestroyObj(GameObject obj)
	{
		Destroy(obj);
	}

	IEnumerator Start()
	{
		if (instance == null)
		{
			Load();
			Destroy(this.gameObject);
			yield break;
		}
		//TouchStartActions.Clear();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//PluginSystem.Init();
		//InAppPurchaseManager = GetComponent<InAppPurchaseManager>();
		//if (InAppPurchaseManager != null)
		//	InAppPurchaseManager.Init();
		//SnapshotCam = SnapshotCamera.MakeSnapshotCamera("SnapShot");
		//SnapshotCam.transform.SetParent(this.transform);
		//SnapshotCam.transform.position = new Vector3(0f, 0f, -1f);
		//yield return TimeSystem.GetGoogleTime(null);
		UISystem.SetMainUI(MainUITrans);
		UISystem.SetHudUI(HUDUITrans);
		UISystem.SetWorldCanvas(WorldCanvas);
		UISystem.LockScreen = UILock;
		yield return LoadGameData();
		//Application.deepLinkActivated += OnDeepLinkActivated;
		if (!string.IsNullOrEmpty(Application.absoluteURL))
		{
			// Cold start and Application.absoluteURL not null so process Deep Link.
			//OnDeepLinkActivated(Application.absoluteURL);
		}
	}

	private IEnumerator LoadGameData()
	{
		//yield return Materials.Create();
		//yield return SoundPlayer.Create();
		//SoundPlayer.Instance.SetRoot(transform);
		yield return Config.Create();
		yield return Tables.Create();
		//yield return TpRemoteProp.FetchRemote();

		//if (ForceUpdateByAppVersion())
		//{
			
		//	yield break;
		//}
		//PluginSystem.LoginProp.InitPlatformLogin();
		UserData.Load();
		//ui preload
		// var handle = Addressables.LoadAssetsAsync<GameObject>("PreLoadUI",  (obj) => {
		// 	var uiBase = obj.GetComponent<UIBase>();
		// 	if(uiBase != null)
		// 	{
		// 		UISystem.PreLoadUI(uiBase.GetType());
		// 	}
		// });

		// yield return handle;
		// Addressables.Release(handle);
#if !BANPOFRI_LOG
		Destroy(CheatWindow);
#endif
		//startGame
		if (GameRoot.Instance.UserData.CurMode.StageData.FacilityState.Count < 1 &&
			GameRoot.Instance.UserData.CurMode.StageData.StageIdx == 1)
		{
			//InGameSystem.NextGameStage(true);
			SetNativeLanguage();

		}
		else
		{
			//BuffSystem.Create();
			//QuestSystem.Create(); // 룸데이터 생성 이후
			//EventSystem.Create();
			//GameNotification.Create();
			//AchievementsSystem.Create();//Have To Do After Quest Create
			//DayworkSystem.Create();
			//PluginSystem.InitMax();
		}

		//FlyADSystem.Init();
		//BannerSystem.Init();
		//AdFlySkinSystem.Init();
		//MysteryBoxSystem.Init();
		//BoostSystem.Init();
		//ShopSystem.Init();
		//DragonCaveSystem.Init();
		//HeroSkillSystem.Init();
		//expeditionsystem.Init();
		//HeroSystem.Init();
		//EnergySystem.Create();
		//GrowthSystem.Init();
		//BoxDropSystem.Init();
		////		HireSystem.Init();
		//QuestSystem.Init();
		////		ExchangeSysytem.Init();
		//AchievementsSystem.Init();
		//PentHouseSystem.Init();
		////		TicketShopSystem.Init();
		//MonsterBuffSystem.Init();
		//TicketSystem.Init();
		//AttendanceSystem.Init();
		////FeverSystem.Create();
		InGameSystem.ChangeMode(CurInGameType);
		//expeditionsystem.Create();
		//DragonHuntSystem.Create();
		////optiondata reset


		//SoundPlayer.Instance.EffectSwitch(UserData.Effect);
		//ProjectUtility.SlowGraphic(UserData.SlowGraphic);





		LoadComplete = true;
	}

	public void BgmOn()
	{
		if (GameRoot.instance.CurInGameType == InGameType.Event)
		{
			SoundPlayer.Instance.PlayBGM("bgm_pirate", true);
			SoundPlayer.Instance.BgmSwitch(UserData.Bgm);
		}
		else
		{
			SoundPlayer.Instance.PlayBGM("bgm", true);
			SoundPlayer.Instance.BgmSwitch(UserData.Bgm);
		}
	}

	public void ChangeIngameType(InGameType type, bool changeData = false)
	{

		CurInGameType = type;
		//InGameSystem.ChangeMode(CurInGameType);
		if (changeData)
		{
			DataState dataState = DataState.None;
			switch (CurInGameType)
			{
				case InGameType.Main:
					{
						dataState = DataState.Main;
					}
					break;
				case InGameType.Event:
					{
						dataState = DataState.Event;
					}
					break;
			}
			if (dataState != DataState.None)
				UserData.ChangeDataMode(dataState);
		}
	}



	private void SetNativeLanguage()
	{
		////var LocaleCode = TreepllaNative.getLocaleCode();
		//if (System.Enum.IsDefined(typeof(Config.Language), LocaleCode))
		//{
		//	if (!System.Enum.TryParse<Config.Language>(LocaleCode, out UserData.Language))
		//	{
		//		UserData.Language = Config.Language.en;
		//	}
		//}
		//else
		//{
		//	UserData.Language = Config.Language.en;
		//}
	}

	public void SetCheatWindow(bool value)
	{
		if (CheatWindow != null)
			CheatWindow.SetActive(value);
	}

	IEnumerator waitTimeAndCallback(float time, System.Action callback)
	{
		yield return new WaitForSeconds(time);
		callback?.Invoke();
	}

	IEnumerator waitFrameAndCallback(int frame, System.Action callback)
	{
		for (int i = 0; i < frame; i++)
			yield return new WaitForEndOfFrame();
		callback?.Invoke();
	}

	public void WaitTimeAndCallback(float time, System.Action callback)
	{
		StartCoroutine(waitTimeAndCallback(time, callback));
	}

	public void WaitFrameAndCallback(int frame, System.Action callback)
	{
		StartCoroutine(waitFrameAndCallback(frame, callback));
	}

	public Vector3 GetRewardEndPos(int rewardType, int rewardIdx)
	{
		//switch (rewardType)
		//{
		//	case (int)Config.RewardGroup.Currency:
		//		{
		//			if (rewardIdx >= (int)Config.CurrencyID.ConquestTicket &&
		//				rewardIdx <= (int)Config.CurrencyID.PropertyTicket)
		//			{
		//				var hudbottom = UISystem.GetUI<HUDBottom>();
		//				if (hudbottom != null)
		//				{
		//					return hudbottom.GetExpeditionBtnPos();
		//				}
		//				else
		//				{
		//					return Vector3.zero;
		//				}

		//			}
		//			else
		//			{
		//				var hud = UISystem.GetUI<HUD>();
		//				if (hud != null)
		//				{
		//					return UISystem.GetUI<HUD>().GetHUDWorldPos(rewardIdx);
		//				}
		//				else
		//				{
		//					return Vector3.zero;
		//				}
		//			}
		//		}
		//	case (int)Config.RewardGroup.Manager:
		//		{
		//			var hudbottom = UISystem.GetUI<HUDBottom>();
		//			if (hudbottom != null)
		//				return UISystem.GetUI<HUDBottom>().GetCardBtnPos();
		//			else
		//				return Vector3.zero;
		//		}
		//	case (int)Config.RewardGroup.RandomManager:
		//		{
		//			var hudbottom = UISystem.GetUI<HUDBottom>();
		//			if (hudbottom != null)
		//				return UISystem.GetUI<HUDBottom>().GetCardBtnPos();
		//			else
		//				return Vector3.zero;
		//		}
		//	//case (int)Config.StageClearType.Choice:
		//	//             {
		//	//		var clearreward = GameRoot.instance.UISystem.GetUI<PopupStageClearReward>();
		//	//		return clearreward.ChoiceRoot.position;

		//	//	}
		//	//case (int)Config.StageClearType.NoneChoice:
		//	//	{
		//	//		var clearreward = GameRoot.instance.UISystem.GetUI<PopupStageClearReward>();
		//	//		return clearreward.NoneChoiceRoot.position;
		//	//             }
		//	case (int)Config.RewardGroup.StageClearBox:
		//		{
		//			var HudQuest = GameRoot.instance.UISystem.GetUI<HUDQuest>();
		//			return HudQuest.HudLeft.BeforePackageIconPos().position;

		//		}

		//	//case (int)Config.RewardGroup.PentHouseRandom:
		//	//case (int)Config.RewardGroup.PentHouseItem:
		//	//             {
		//	//		var hud = UISystem.GetUI<HUD>();
		//	//		if (hud != null)
		//	//		{
		//	//			return UISystem.GetUI<HUD>().GetHUDWorldPos((int)Config.CurrencyID.Material);
		//	//		}
		//	//		else
		//	//		{
		//	//			return Vector3.zero;
		//	//		}
		//	//	}
		//	case (int)Config.RewardGroup.EventBuffCat:
		//		{
		//			var popup = GameRoot.instance.UISystem.GetUI<PopupEventPass>();
		//			if (popup != null)
		//			{
		//				return popup.GetMainRewardTr().position;
		//			}
		//			else
		//				return Vector3.zero;
		//		}
		//	case (int)Config.RewardGroup.DragonCave:
		//		{
		//			var hudbottom = UISystem.GetUI<HUDBottom>();
		//			if (hudbottom != null)
		//				return UISystem.GetUI<HUDBottom>().GetCaveBtnPos();
		//			else
		//				return Vector3.zero;
		//		}
		//}
		return Vector3.zero;
	}



	private void OnApplicationPause(bool pause)
	{
		if (!LoadComplete)
			return;


		if (pause)
		{

			//PluginSystem.OnApplicationPause(pause);

			GameRoot.instance.UserData.CurMode.ExpeditionData.offlineattacktime = GameRoot.instance.UserData.CurMode.ExpeditionData.AttackTimeProperty.Value;

			
				
		}
		else
		{
			//GameRoot.Instance.WaitTimeAndCallback(.5f, () => LocalNotification.CheckNotiOpenApp());
			//LocalNotification.ClearNotification(Tables.Instance.GetTable<LocalPush>().GetKeys.ToArray());

			//if (GameRoot.Instance.TutorialSystem.IsActive())
			//	return;

			var time = GameRoot.Instance.UserData.CurMode.LastLoginTime;

			if (time.Equals(default(System.DateTime)))
			{
				return;
			}


			if (InGameSystem.GetInGame<InGameTycoon>() == null) return;
			if (InGameSystem.GetInGame<InGameTycoon>().curInGameStage == null) return;
			if (InGameSystem.GetInGame<InGameTycoon>().curInGameStage.IsLoadComplete == false) return;

			//TicketSystem.CheckOfflineTicketCount();

			//var diff = TimeSystem.GetCurTime().Subtract(time);


			//	if (diff.TotalSeconds > 0f && GameRoot.instance.expeditionsystem.IsAttack)
			//	{
			//		GameRoot.instance.expeditionsystem.OfflineMonsterCalculate();
			//	}

			//	System.Action CheckAtt = () =>
			//	{
			//		if (CurInGameType == InGameType.Main)
			//		{
			//			AttendanceSystem.UpdateTargetAttendance();
			//			if (AttendanceSystem.CheckAttendance())
			//			{
			//				var attPopup = UISystem.GetUI<PopupMission>();

			//				if (attPopup != null)
			//				{
			//					if (attPopup.gameObject.activeSelf)
			//					{
			//						attPopup.Show(MissionTab.Attendance);
			//						//attPopup.StartAttendance();
			//					}
			//					else
			//					{
			//						UISystem.OpenUI<PopupMission>(popup => { popup.Show(MissionTab.Attendance); }, null, false);
			//					}
			//				}
			//				else
			//				{
			//					UISystem.OpenUI<PopupMission>(popup => { popup.Show(MissionTab.Attendance); }, null, false);
			//				}
			//			}
			//		}
			//	};

			//	if (diff.TotalSeconds > minRewardTime)
			//	{
			//		//GameRoot.instance.FeverSystem.GenerateOfflineDust();
			//		var offlinereward = GameRoot.instance.UISystem.GetUI<PopupOfflineReward>();

			//		if (offlinereward != null)
			//		{
			//			if (!offlinereward.gameObject.activeSelf)
			//			{
			//				if ((int)diff.TotalSeconds >= maxRewardTime)
			//					GameRoot.instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set(maxRewardTime), CheckAtt, true, ProjectUtility.GetRunStage()); //offline max value 
			//				else
			//					GameRoot.instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set((int)diff.TotalSeconds), CheckAtt, true, ProjectUtility.GetRunStage()); //offline not max value
			//			}
			//			else
			//			{
			//				if ((int)diff.TotalSeconds >= maxRewardTime)
			//					offlinereward.Set(maxRewardTime);
			//				else
			//					offlinereward.Set((int)diff.TotalSeconds);
			//			}
			//		}
			//		else
			//		{
			//			if ((int)diff.TotalSeconds >= maxRewardTime)
			//				GameRoot.instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set(maxRewardTime), CheckAtt, true, ProjectUtility.GetRunStage()); //offline max value 
			//			else
			//				GameRoot.instance.UISystem.OpenUI<PopupOfflineReward>(popup => popup.Set((int)diff.TotalSeconds), CheckAtt, true, ProjectUtility.GetRunStage()); //offline not max value

			//		}
			//	}
			//	else
			//	{
			//		CheckAtt.Invoke();
			//	}


			//	GameRoot.Instance.MonsterBuffSystem.CalculateOfflineReduce();
			//	GameRoot.Instance.MonsterBuffSystem.bUpdate = true;
			//}
		}
	}

		//public void OnReceviedNotiNumber(string notiNumber)
		//{
		//	TpLog.Log($"received num : {notiNumber}");

		//}


		//private void OnDeepLinkActivated(string url)
		//{
		//	var deeplinkURL = url;
		//	var arg = deeplinkURL.Split("?"[0])[1];
		//	EventDeepLink = true;
		//}

#if UNITY_EDITOR
		private void OnApplicationQuit()
		{
			//PluginSystem.OnApplicationPause(true);
			UnityEditor.AssetDatabase.SaveAssets();
			UnityEditor.AssetDatabase.Refresh();
		}
#endif

		//private bool ForceUpdateByAppVersion()
		//{
		//	//var appversion = 0;
		//	//if (int.TryParse(Application.version.Split('.').LastOrDefault(), out appversion))
		//	//{
		//	//	var force_update_version = Tables.Instance.GetTable<Define>().GetData("force_update_version").value;
		//	//	if (appversion < force_update_version)
		//	//		return true;
		//	//}

		//	//return false;
		//}
	}
