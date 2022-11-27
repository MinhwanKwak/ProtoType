using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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


}
