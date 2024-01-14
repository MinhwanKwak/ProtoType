using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;

public class BuildEnemyCamp : BuildCamp
{



    public override void Init()
    {
        base.Init();


        var stageidx = GameRoot.Instance.UserData.CurMode.StageIdx;


        var td = Tables.Instance.GetTable<StageBuild>().GetData(stageidx);

        if(td != null)
        {
            Hp = td.ally_build_hp;
        }

    }

}
