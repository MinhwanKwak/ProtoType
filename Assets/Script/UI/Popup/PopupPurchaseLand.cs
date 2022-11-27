using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;

[UIPath("UI/Popup/PopupPurchaseLand")]
public class PopupPurchaseLand : UIBase
{
    [SerializeField]
    private Text BuyText;

    [SerializeField]
    private Text MonthBenefitText;

    [SerializeField]
    private Text LandNameText;

    [SerializeField]
    private Image StatusIcon;

    [SerializeField]
    private Slider ProgressValue;

    [SerializeField]
    private Button BuyBtn;

    [SerializeField]
    private Button CloseDimBtn;

    System.Numerics.BigInteger Value;

    private int GroundIdx = 0;

    protected override void Awake()
    {
        base.Awake();

        BuyBtn.onClick.AddListener(OnClickBuy);
        CloseDimBtn.onClick.AddListener(Hide);
    }



    public void Set(int groundidx)
    {
        int stageidx = 1;

        GroundIdx = groundidx;

        var landbasictd = Tables.Instance.GetTable<LandBasic>().GetData(new KeyValuePair<int, int>(stageidx, groundidx));

        var landupgradetd = Tables.Instance.GetTable<LandUpgrade>().GetData(1);

        if (landbasictd == null && landupgradetd == null)
            return;

        Value = landbasictd.open_cost;

        BuyText.text = Utility.CalculateMoneyToString(landbasictd.open_cost);

        MonthBenefitText.text = Utility.CalculateMoneyToString(landupgradetd.profit);

        LandNameText.text = Tables.Instance.GetTable<Localize>().GetString(landbasictd.name);
    }


    public void OnClickBuy()
    {

        if(GameRoot.Instance.UserData.CurMode.Money.Value >= Value)
        {
            GameRoot.Instance.UserData.SetReward((int)Config.RewardGroup.Currency, (int)Config.CurrencyID.Money, -Value);

            GameRoot.Instance.FacilitySystem.AddFacility(GroundIdx);
            Hide();

        }
    }
}
