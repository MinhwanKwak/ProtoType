using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;
using UniRx; 
public class InGameFacility : MonoBehaviour
{

    public int FacilityIdx = 0;

    public int GetFacilityIdx { get { return FacilityIdx; } }

    private CompositeDisposable disposable = new CompositeDisposable();
    
    public void Init(int facilityidx)
    {
        FacilityIdx = facilityidx;
        disposable.Clear();


        var facilitydata = GameRoot.Instance.UserData.CurMode.FacilityDatas;


        var lastfacilityidx = ProjectUtility.FindNextSearchFacilityidx();
        ProjectUtility.SetActiveCheck(this.gameObject, FacilityIdx <= lastfacilityidx);

        facilitydata.ObserveAdd().Subscribe(x =>
        {
           var facilityidx = ProjectUtility.FindNextSearchFacilityidx();
           ProjectUtility.SetActiveCheck(this.gameObject, FacilityIdx <= facilityidx);
        }).AddTo(disposable);


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
