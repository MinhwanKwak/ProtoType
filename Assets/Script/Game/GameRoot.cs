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
	public PluginSystem PluginSystem { get; private set; } = new PluginSystem();
	public FacilitySystem FacilitySystem { get; private set; } = new FacilitySystem();
	public PlayTimeSystem PlayTimeSystem { get; private set; } = new PlayTimeSystem();



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
		PlayTimeSystem.Update();

		if (deltaTime >= 1f) // one seconds updates;
		{
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
		
		yield return Config.Create();
		yield return Tables.Create();
	
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
//#if !BANPOFRI_LOG
//		Destroy(CheatWindow);
//#endif
		//startGame
		//if (GameRoot.Instance.UserData.CurMode.StageData.FacilityState.Count < 1 &&
		//	GameRoot.Instance.UserData.CurMode.StageData.StageIdx == 1)
		//{
		//	//InGameSystem.NextGameStage(true);
		//	SetNativeLanguage();

		//}
		//else
		//{
		//	//BuffSystem.Create();
		//	//QuestSystem.Create(); // 룸데이터 생성 이후
		//	//EventSystem.Create();
		//	//GameNotification.Create();
		//	//AchievementsSystem.Create();//Have To Do After Quest Create
		//	//DayworkSystem.Create();
		//	//PluginSystem.InitMax();
		//}

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
		return Vector3.zero;
	}



	private void OnApplicationPause(bool pause)
	{
		if (!LoadComplete)
			return;




		if (pause)
		{

			PluginSystem.OnApplicationPause(pause);

			
				
		}
		else
		{
			


			//if (time.Equals(default(System.DateTime)))
			//{
			//	return;
			//}


			if (InGameSystem.GetInGame<InGameTycoon>() == null) return;
			if (InGameSystem.GetInGame<InGameTycoon>().curInGameStage == null) return;
			if (InGameSystem.GetInGame<InGameTycoon>().curInGameStage.IsLoadComplete == false) return;

			
		}
	}


#if UNITY_EDITOR
		private void OnApplicationQuit()
		{
			PluginSystem.OnApplicationPause(true);
			UnityEditor.AssetDatabase.SaveAssets();
			UnityEditor.AssetDatabase.Refresh();
		}
#endif

	}
