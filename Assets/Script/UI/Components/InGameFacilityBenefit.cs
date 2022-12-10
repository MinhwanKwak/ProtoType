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

    [SerializeField]
    private GameObject JackPodObj;


    private System.Action OnClickAction = null;
    private void Awake()
    {
        BenefitBtn.onClick.AddListener(OnClickBenefit);
    }

    public void ActiveJackPod(bool isjackpod)
    {
        Utility.SetActiveCheck(JackPodObj, isjackpod);
    }

    public void Set(System.Numerics.BigInteger benefit , System.Action clickaction)
    {
        Benefit = benefit;
        OnClickAction = clickaction;
    }


    public  void OnClickBenefit()
    {
        OnClickAction?.Invoke();
        GameRoot.Instance.UserData.SetReward((int)Config.RewardGroup.Currency, (int)Config.CurrencyID.Money, Benefit);
        Utility.SetActiveCheck(this.gameObject, false);
    }

}
