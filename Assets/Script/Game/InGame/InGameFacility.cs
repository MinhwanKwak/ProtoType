using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;
using UniRx; 
public class InGameFacility : MonoBehaviour
{
    [SerializeField]
    private Transform BenefitRoot;

    public int FacilityIdx = 0;

    public int GetFacilityIdx { get { return FacilityIdx; } }

    private CompositeDisposable disposable = new CompositeDisposable();


    private bool IsBenefit = false;

    private bool IsJackPod = false;

    private float benefittime = 0f;

    private InGameFacilityBenefit FacilityBenefitUI;

    public void Init(int facilityidx)
    {
        FacilityIdx = facilityidx;
        disposable.Clear();


        var facilitydata = GameRoot.Instance.UserData.CurMode.FacilityDatas;

        var finddata = facilitydata.ToList().Find(x => x.GroundIndex == facilityidx && x.FacilityGradeIdx >= 1);


        var lastfacilityidx = ProjectUtility.FindNextSearchFacilityidx();
        ProjectUtility.SetActiveCheck(this.gameObject, FacilityIdx <= lastfacilityidx);

        facilitydata.ObserveAdd().Subscribe(x =>
        {
           var facilityidx = ProjectUtility.FindNextSearchFacilityidx();
           ProjectUtility.SetActiveCheck(this.gameObject, FacilityIdx <= facilityidx);
        }).AddTo(disposable);


        if (finddata != null)
        {

            if (FacilityBenefitUI != null)
            {
                Destroy(FacilityBenefitUI.gameObject);
                FacilityBenefitUI = null;

            }


            var landupgradetd = Tables.Instance.GetTable<LandUpgrade>().GetData(finddata.FacilityGradeIdx);

            if (landupgradetd != null)
            {
                GameRoot.Instance.UISystem.LoadFloatingUI<InGameFacilityBenefit>((_value) =>
                {
                    FacilityBenefitUI = _value;
                    FacilityBenefitUI.transform.position = BenefitRoot.position;

                    FacilityBenefitUI.Set(landupgradetd.profit, OnClickUIAction);
    

                    Utility.SetActiveCheck(FacilityBenefitUI.gameObject, IsBenefit);
                });
            }
        }



        GameRoot.Instance.PlayTimeSystem.RemainTimeProperty.Subscribe(x => {

            if(!IsBenefit)
            {
                benefittime += 1;

                if (benefittime >= GameRoot.Instance.FacilitySystem.month_get_profit_period)
                {
                    benefittime = 0;
                    IsBenefit = true;
                    IsJackPod = JackPodProfit();

                    if(FacilityBenefitUI != null)
                    {
                        Utility.SetActiveCheck(FacilityBenefitUI.gameObject, true);
                        FacilityBenefitUI.ActiveJackPod(IsJackPod);
                    }
                }
            }
        }).AddTo(disposable);

    }

    public void OnClickUIAction()
    {
        IsBenefit = false;
    }


    public bool JackPodProfit()
    {
        var ratio = GameRoot.Instance.FacilitySystem.month_get_profit_jackpot_rate;

        var rand = Random.Range(0, 100);

        return ratio > rand;
    }



    private void OnDestroy()
    {
        disposable.Clear();
    }


    public void OnClickFacility()
    {
        var finddata = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x => x.GroundIndex == FacilityIdx);

        if(finddata != null)
        {
            GameRoot.Instance.UISystem.OpenUI<PopupInfoLand>(popup => popup.Set(FacilityIdx));
        }
        else
        {
            GameRoot.Instance.UISystem.OpenUI<PopupPurchaseLand>(popup=> popup.Set(FacilityIdx));
        }
    }
}
