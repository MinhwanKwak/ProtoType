using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;

public class FacilitySystem 
{

    public int month_get_profit_period { get; private set; }
    public int month_get_profit_jackpot_rate { get; private set; }
    public int month_get_profit_jackpot_incre { get; private set; }

    public void Create()
    {


        month_get_profit_period = Tables.Instance.GetTable<Define>().GetData("month_get_profit_period").value;
        month_get_profit_jackpot_rate = Tables.Instance.GetTable<Define>().GetData("month_get_profit_jackpot_rate").value;
        month_get_profit_jackpot_incre = Tables.Instance.GetTable<Define>().GetData("month_get_profit_jackpot_incre").value;
    }


    public void AddFacility(int groundidx)
    {
        var facilitydata = new FacilityData(groundidx, 1, 3, false, TimeSystem.GetCurTime().Ticks, false);

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
