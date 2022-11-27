using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;

public class FacilitySystem 
{
    public void AddFacility(int groundidx)
    {
        var facilitydata = new FacilityData(groundidx, 1, 3, false, TimeSystem.GetCurTime().Ticks);

        GameRoot.Instance.UserData.CurMode.FacilityDatas.Add(facilitydata);
    }


    public void SetFacilityGrade(int grade, int groundidx)
    {
        var facilitydatas = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x=> x.GroundIndex == groundidx);


        if(facilitydatas != null)
        {
            facilitydatas.FacilityGradeProperty.Value = grade;
            GameRoot.Instance.InGameSystem.GetInGame<InGameTycoon>().curInGameStage.SearchGroundAdd(grade,groundidx);

            GameRoot.Instance.UserData.Save();
        }
        



    }
}
