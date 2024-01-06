using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using BanpoFri;
using UniRx;
using System.Linq;

public class InGameTycoon : InGameMode
{
  
    //private ObjectPool<Dust> dustPool = new ObjectPool<Dust>();
    //private List<BubbleReward> activeBubbles = new List<BubbleReward>();
    //private List<Dust> activeDusts = new List<Dust>();


    [HideInInspector]
    public InGameStage curInGameStage;


    public IReactiveProperty<int> FarsightedTimeProperty = new ReactiveProperty<int>(0);
    public IReactiveProperty<bool> MaxMode = new ReactiveProperty<bool>(true);
    public IReactiveProperty<float> GameSpeedMultiValue = new ReactiveProperty<float>(1f);
    //private int dustMaxCnt = 20;


    private int ProductHeroIdxs = 0;
    public override void Load()
    {
        base.Load();




        Addressables.InstantiateAsync("InGame1_1").Completed += (handle) =>
        {
            curInGameStage = handle.Result.GetComponent<InGameStage>();
            if (curInGameStage != null)
            {
                curInGameStage.Init();
            }
        };

        //CalculateGameSpeed();

    }

    //public void CalculateGameSpeed()
    //{
    //    var buffValue = GameRoot.Instance.BuffSystem.GetValueByBuffType(BuffSystem.BuffType.ProductSpeed);
    //    GameSpeedMultiValue.Value = 1f - buffValue;
    //}

    protected override void LoadUI()
    {
        base.LoadUI();

        GameRoot.Instance.UISystem.OpenUI<HUD>(popup => popup.Init());

        //GameRoot.Instance.UISystem.OpenUI<HUD>();
        //GameRoot.Instance.UISystem.OpenUI<HUDQuest>(quest => { quest.Init(); });
        ////GameRoot.Instance.UISystem.OpenUI<HudRight>(right => right.Init());
        //GameRoot.Instance.UISystem.OpenUI<PageNavigator>();
        /*if (GameRoot.Instance.UserData.CurMode.StageData.StageIdx > 1)
        {
            GameRoot.Instance.UISystem.OpenUI<HUDBottom>();
        }*/

    }


    public override void UnLoad()
    {
        base.UnLoad();
        //foreach(var obj in activeObjs)
        //    if(obj != null && obj.gameObject != null) 
        //        Addressables.ReleaseInstance(obj.gameObject);

        //GameRoot.Instance.EnergySystem.BubbleUnLoad();

        //if (curInGameStage != null && curInGameStage.gameObject != null)
        //    Addressables.ReleaseInstance(curInGameStage.gameObject);

        //activeObjs.Clear();
        //objectPool.Clear();
        //bubblePool.Clear();
        //cachedSkin.Clear();
    }

}
