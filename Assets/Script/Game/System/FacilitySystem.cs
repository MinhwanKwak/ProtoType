using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;

public class FacilitySystem 
{
    public void AddFacility(int groundidx)
    {
        var facilitydata = new FacilityData(groundidx, 1, 3, false, TimeSystem.GetCurTime().Ticks);

        GameRoot.Instance.UserData.CurMode.FacilityDatas.Add(facilitydata);
    }
}
