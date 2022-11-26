using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameFacility : MonoBehaviour
{

    int FacilityGroundIdx = 0;


    public void Init(int facilitygroundidx)
    {
        FacilityGroundIdx = facilitygroundidx;

    }



    public void OnClickFacility()
    {
        var finddata = GameRoot.Instance.UserData.CurMode.FacilityDatas.Find(x => x.GroundIndex == FacilityGroundIdx);

        if(finddata != null)
        {

        }
        else
        {

        }
    }
}
