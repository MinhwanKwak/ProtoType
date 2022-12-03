using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;
using UniRx;
[UIPath("UI/Page/HUD", true)]
public class HUD : UIBase
{
    [SerializeField]
    private Text CashText;

    [SerializeField]
    private Text MaterialText;

    [SerializeField]
    private Text MoneyText;




    public void Init()
    {
    }

    public override void OnShowBefore()
    {
        base.OnShowBefore();
        SyncData();
    }


    public void SyncData()
    {

        GameRoot.Instance.UserData.SyncHUDCurrency();

        MoneyText.text = Utility.CalculateMoneyToString(GameRoot.Instance.UserData.CurMode.Money.Value);
        MaterialText.text = GameRoot.Instance.UserData.CurMode.Material.Value.ToString();
        CashText.text = GameRoot.Instance.UserData.Cash.Value.ToString();


        GameRoot.Instance.UserData.HUDCash.Subscribe(x =>
        {
            CashText.text = x.ToString();
        }).AddTo(this);

        GameRoot.Instance.UserData.HUDMoney.Subscribe(x =>
        {
            MoneyText.text = Utility.CalculateMoneyToString(x);
        }).AddTo(this);
        GameRoot.Instance.UserData.HUDMaterial.Subscribe(x =>
        {
            MaterialText.text = x.ToString();
        }).AddTo(this);
    }
}
