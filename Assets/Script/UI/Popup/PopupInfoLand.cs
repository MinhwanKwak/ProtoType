using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;
using System.Linq;

[UIPath("UI/Popup/PopupInfoLand")]
public class PopupInfoLand : UIBase
{
    [SerializeField]
    private Text MonthBenefitText;

    [SerializeField]
    private Text BenefitNumberText;

    [SerializeField]
    private Text CurStatusDescText;

    [SerializeField]
    private Text UpgradeValueText;

    [SerializeField]
    private Image CurFacilityImg;

    [SerializeField]
    private Image NextFacilityImg;

    [SerializeField]
    private Button BuyBtn;

    [SerializeField]
    private Button DimBtn;


    private System.Numerics.BigInteger CostFacilityUpgrade;

    private int GroundIdx = 0;

    private int NextGradeIdx = 0;
    protected override void Awake()
    {
        base.Awake();
        BuyBtn.onClick.AddListener(OnClickBuy);
        DimBtn.onClick.AddListener(Hide);


    }

    public void Set(int groundidx)
    {
        GroundIdx = groundidx;

        var landdata = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x => x.GroundIndex == groundidx);

        if (landdata == null)
        {
            Utility.SetActiveCheck(this.gameObject, false);
            return;
        }
        NextGradeIdx = landdata.FacilityGradeIdx + 1;

        //var td = Tables.Instance.GetTable<LandEvent>().GetData(groundidx);
        var upgradetd = Tables.Instance.GetTable<LandUpgrade>().GetData(NextGradeIdx);
        var basictd = Tables.Instance.GetTable<LandBasic>().GetData(new KeyValuePair<int, int>(1, landdata.GroundIndex));

        if (upgradetd == null || basictd == null)
        {
            Utility.SetActiveCheck(this.gameObject, false);
            return;
        }


        CurFacilityImg.sprite = Config.Instance.GetFacilityImg(basictd.icon[landdata.FacilityGradeIdx]);

        NextFacilityImg.sprite = Config.Instance.GetFacilityImg(basictd.icon[NextGradeIdx]);

        UpgradeValueText.text = Utility.CalculateMoneyToString(upgradetd.cost);
        CostFacilityUpgrade = upgradetd.cost;
    }


    public void OnClickBuy()
    {

        if(GameRoot.Instance.UserData.CurMode.Material.Value >= CostFacilityUpgrade)
        {
            GameRoot.Instance.UserData.SetReward((int)Config.RewardGroup.Currency, (int)Config.CurrencyID.Material, -CostFacilityUpgrade);

            GameRoot.Instance.FacilitySystem.SetFacilityGrade(NextGradeIdx,GroundIdx);
            Hide();



        }

    }



}
