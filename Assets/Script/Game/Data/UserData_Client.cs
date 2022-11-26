using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using BanpoFri;
using BanpoFri.Data;
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
	public IReactiveCollection<string> Tutorial {get; private set;} = new ReactiveCollection<string>();
	public IReactiveCollection<string> GameNotifications { get; private set; } = new ReactiveCollection<string>();
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


    void ConnectReadOnlyDatas()
    {
        ChangeDataMode(DataState.Main);


        Cash.Value = flatBufferUserData.Cash;
        mainData.Money.Value = BigInteger.Parse(flatBufferUserData.Money);
        mainData.Material.Value = flatBufferUserData.Material;



        for (int i = 0; i < flatBufferUserData.FacilitydatasLength; ++i)
        {
            var data = flatBufferUserData.Facilitydatas(i);

            var facilitydata = new FacilityData(data.Value.Groundidx , data.Value.Facilitygrade , data.Value.Landstatuseventidx
                ,data.Value.Iseventground , data.Value.Landbenefittime);

            mainData.FacilityDatas.Add(facilitydata);

        }
    }


    public UserDataEvent CurEventData { get { return eventData; } }


    public UserDataMain CurMainData { get { return mainData; } }

    private void SnycCollectionToDB<T, U>(IList<T> db, IEnumerable<U> collector) where T : class
    {
        db.Clear();
        foreach (var iter in collector)
        {
            db.Add(iter as T);
        }
    }

    private void SnycCollectionToClient<T, U>(IList<T> db, IEnumerable<U> collector)
    where T : class, IReadOnlyData
    where U : class, IReadOnlyData
    {
        db.Clear();
        foreach (var iter in collector)
        {
            db.Add(iter.Clone() as T);
        }
    }

    public void SyncHUDCurrency(int currencyID = -1)
    {
        if (currencyID < 0)
        {
            HUDMoney.Value = CurMode.Money.Value;
            HUDMaterial.Value = CurMode.Material.Value;
            HUDCash.Value = Cash.Value;
        }
        else if (currencyID == (int)Config.CurrencyID.Cash)
        {
            HUDCash.Value = Cash.Value;
        }
        else if (currencyID == (int)Config.CurrencyID.Material || currencyID == (int)Config.CurrencyID.EventMaterial)
        {
            HUDMaterial.Value = CurMode.Material.Value;
        }
        else if (currencyID == (int)Config.CurrencyID.Money || currencyID == (int)Config.CurrencyID.EventMoney)
        {
            HUDMoney.Value = CurMode.Money.Value;
        }
    }

    public void SetHUDUIReward(int rewardType, int rewardIdx, BigInteger rewardCnt)
    {
        if (rewardType != (int)Config.RewardGroup.Currency) return;
        switch (rewardIdx)
        {
            case (int)Config.CurrencyID.EventMaterial:
            case (int)Config.CurrencyID.Material:
                {
                    HUDMaterial.Value += (int)rewardCnt;
                }
                break;
            case (int)Config.CurrencyID.EventMoney:
            case (int)Config.CurrencyID.Money:
                {
                    HUDMoney.Value += rewardCnt;
                }
                break;
            case (int)Config.CurrencyID.Cash:
                {
                    HUDCash.Value += (int)rewardCnt;
                }
                break;
         
        }
    }

    public void SetReward(int rewardType, int rewardIdx, BigInteger rewardCnt, bool hudRefresh = true)
    {
        switch (rewardType)
        {
            case (int)Config.RewardGroup.Currency:
                {
                    switch (rewardIdx)
                    {
                     
                        case (int)Config.CurrencyID.EventMaterial:
                        case (int)Config.CurrencyID.Material:
                            {
                                CurMode.Material.Value += (int)rewardCnt;
                            }
                            break;
                        case (int)Config.CurrencyID.EventMoney:
                        case (int)Config.CurrencyID.Money:
                            {
                                CurMode.Money.Value += rewardCnt;
                            }
                            break;
                        case (int)Config.CurrencyID.Cash:
                            {
                                Cash.Value += (int)rewardCnt;
                            }
                            break;
                  
                      
                    }
                    if (hudRefresh)
                    {
                        SetHUDUIReward(rewardType, rewardIdx, rewardCnt);
                    }
                }
                break;
           
        }




    }

    public void RefreshUICurrency()
	{
		
	}


	private void TutoDataCheck()
    {

    }
}