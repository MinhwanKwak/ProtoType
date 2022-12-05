using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BanpoFri;
public class ProjectUtility 
{


    public static int FindNextSearchFacilityidx()
    {
        var finddata = GameRoot.Instance.UserData.CurMode.FacilityDatas.LastOrDefault();

        if (finddata == null)
            return 1;

        return finddata.GroundIndex + 1;
    }


    public static void SetActiveCheck(GameObject target, bool value)
    {
        if (target != null)
        {
            if (value && !target.activeSelf)
                target.SetActive(true);
            else if (!value && target.activeSelf)
                target.SetActive(false);
        }
    }


    public static int LandCondination(int needpoint)
    {
        var tdlist = Tables.Instance.GetTable<LandCondition>().DataList.ToList();

        foreach(var td in tdlist)
        {
            if (td.need_point[0] <= needpoint && td.need_point[1] >= needpoint)
                return td.condition;
        }



        return 0;
    }


}
