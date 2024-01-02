using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;
using BanpoFri;

public class InGameStage : MonoBehaviour
{
    [Header("Input Value")]
    [SerializeField]
    private float cameraYInterval = 8.77f;
    [SerializeField]
    private float defaultCameraMinY = 17f;

    public bool IsLoadComplete { get; private set; }


    private CompositeDisposable disposable = new CompositeDisposable();

    public void Init()
    {
        IsLoadComplete = false;
        disposable.Clear();


        GameRoot.Instance.UserData.CurMode.FacilityDatas.ObserveAdd().Subscribe(x => {
            SearchGroundAdd(x.Value.FacilityGradeIdx, x.Value.GroundIndex);
        }).AddTo(disposable);

        //tempdata
        var stageidx = 1;
        var tdlist = Tables.Instance.GetTable<LandBasic>().DataList.Where(x => stageidx == x.stage).ToList();

        for(int i = 0; i  < tdlist.Count; ++i)
        {
            var finddata = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x => x.GroundIndex == tdlist[i].idx && stageidx == tdlist[i].stage);


            if (finddata != null)
            {
                SearchGroundAdd(finddata.FacilityGradeIdx, finddata.GroundIndex);
            }
            else
            {
                SearchGroundAdd(0, tdlist[i].idx);
            }
        }

    }

    private void OnDestroy()
    {
        disposable.Clear();
    }


    public void SearchGroundAdd(int facilitygradeidx, int facilityidx)
    {
    }


}
