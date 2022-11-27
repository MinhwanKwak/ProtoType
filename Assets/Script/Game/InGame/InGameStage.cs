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
    [SerializeField]
    private List<Transform> LandTrList = new List<Transform>();


    [SerializeField]
    private List<InGameFacility> CurStageFacilityList = new List<InGameFacility>();

    public bool IsLoadComplete { get; private set; }


    private CompositeDisposable disposable = new CompositeDisposable();

    public void Init()
    {
        IsLoadComplete = false;
        CurStageFacilityList.Clear();
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


    public void SearchGroundAdd(int facilitygradeidx ,int facilityidx)
    {
        var tdlist = Tables.Instance.GetTable<LandBasic>().DataList.Where(x => 1 == x.stage).ToList();


        var finddata = CurStageFacilityList.Find(x => x.GetFacilityIdx == facilityidx);

        var getfacilitydata = GameRoot.Instance.UserData.CurMode.FacilityDatas.ToList().Find(x => x.GroundIndex == facilityidx);

        if(getfacilitydata != null)
        {
            if (finddata != null)
            {
                CurStageFacilityList.Remove(finddata);

                Destroy(finddata.gameObject);
            }

            Addressables.InstantiateAsync(tdlist[facilitygradeidx].prefab[facilitygradeidx], LandTrList[facilityidx]).Completed += (handle) =>
            {
                var facility = handle.Result.GetComponent<InGameFacility>();
                if (facility != null)
                {

                    facility.Init(facilityidx);

                    CurStageFacilityList.Add(facility);

                    facility.transform.position = LandTrList[facilityidx - 1].position;
                } 
            };
        }
        else
        {
            Addressables.InstantiateAsync(tdlist[facilitygradeidx].prefab[facilitygradeidx], LandTrList[facilityidx]).Completed += (handle) =>
            {
                var facility = handle.Result.GetComponent<InGameFacility>();
                if (facility != null)
                {

                    facility.Init(facilityidx);

                    CurStageFacilityList.Add(facility);

                    facility.transform.position = LandTrList[facilityidx - 1].position;
                }
            };
        }
    }



}
