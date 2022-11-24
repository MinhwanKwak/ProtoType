using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;
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

        var userdata = GameRoot.Instance.UserData;



        userdata.CurMode.Money.Value += 10000000;
        userdata.CurMode.Material.Value += 1000;

        userdata.Cash.Value += 1000;


        CashText.text = userdata.Cash.Value.ToString();
        MaterialText.text = userdata.CurMode.Material.Value.ToString();
        MoneyText.text = userdata.CurMode.Money.Value.ToString();

        GameRoot.Instance.UserData.Save();

    }
}
