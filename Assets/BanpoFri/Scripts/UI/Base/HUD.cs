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
        CashText.text = "0";
        MaterialText.text = "0";
        MoneyText.text = "0";



    }
}
