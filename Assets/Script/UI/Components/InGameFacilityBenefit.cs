using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;

[UIPath("UI/InGame/InGameFacilityBenefit")]
public class InGameFacilityBenefit : InGameFloatingUI
{

    private System.Numerics.BigInteger Benefit;


    [SerializeField]
    private Button BenefitBtn;


    private void Awake()
    {
        BenefitBtn.onClick.AddListener(OnClickBenefit);
    }


    public void Set(System.Numerics.BigInteger benefit)
    {
        Benefit = benefit;
    }


    public  void OnClickBenefit()
    {
        GameRoot.Instance.UserData.SetReward((int)Config.RewardGroup.Currency, (int)Config.CurrencyID.Money, Benefit);
        Destroy(this.gameObject);
    }

}
