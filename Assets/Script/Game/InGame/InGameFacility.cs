using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;
public class InGameFacility : MonoBehaviour
{

    int FacilityIdx = 0;

    public int GetFacilityIdx { get { return FacilityIdx; } }

    public void Init(int facilityidx)
    {
        FacilityIdx = facilityidx;

    }


        
    public void OnClickFacility()
    {
        var finddata = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x => x.GroundIndex == FacilityIdx);

        if(finddata != null)
        {

        }
        else
        {
            GameRoot.Instance.UISystem.OpenUI<PopupPurchaseLand>(popup=> popup.Set(FacilityIdx));

        }
    }
}
