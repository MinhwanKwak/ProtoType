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


    public bool IsLoadComplete { get; private set; }


    private int FacilityIdx = 0;

    public void Init()
    {
        IsLoadComplete = false;


        //tempdata
        var stageidx = 1;
        FacilityIdx = 0;

        var tdlist = Tables.Instance.GetTable<LandBasic>().DataList.Where(x => stageidx == x.stage).ToList();

        for(int i = 0; i  < tdlist.Count; ++i)
        {
            Addressables.InstantiateAsync(tdlist[i].prefab.First(), LandTrList.First()).Completed += (handle) =>
            {
                var facility = handle.Result.GetComponent<InGameFacility>();
                if(facility != null)
                {
                    facility.Init();
                    facility.transform.position = LandTrList[FacilityIdx].position;
                    ++FacilityIdx;
                }
            };

        }



        //GameRoot.Instance.InGameSystem.CurInGame.SetCameraBoundMinY(-(defaultCameraMinY + cameraYInterval * (nextOpenFloor)));

        //load 
    }


    //public InGameFacility GetFacility(int order)
    //{
    //    if (facilities.Count > order && order >= 0)
    //        return facilities[order];

    //    return null;
    //}




    //public InGameFacilityBase GetFacilityByIdx(int facilityIdx)
    //{
    //    if (facilityStart.FacilityIndex == facilityIdx)
    //        return facilityStart;
    //    else if (facilityEnd.FacilityIndex == facilityIdx)
    //        return facilityEnd;
    //    else if (facilityIdx == Config.RouletteIdx)
    //        return facilityRoulette;
    //    else
    //        return facilities.Find(x => x.FacilityIndex == facilityIdx);
    //}

    public void OpenFacility(int order)
    {
        //var Facility = GetFacility(order);
        //if (Facility != null)
        //{
        //    var nextOpenFloor = GameRoot.Instance.UserData.CurMode.StageData.NextOpenFloor;
        //    Utility.SetActiveCheck(Facility.gameObject, true);
        //    Facility.SetOpen(true);
        //    GameRoot.Instance.InGameSystem.CurInGame.IngameCamera.FocusPosition(Facility.transform.position,
        //        GameRoot.Instance.InGameSystem.CurInGame.IngameCamera.zoomOutSize);
        //    GameRoot.Instance.InGameSystem.CurInGame.SetCameraBoundMinY(-(defaultCameraMinY + cameraYInterval * (nextOpenFloor)));
        //    GameRoot.Instance.EffectSystem.Play<FacilityOpenEffect>(Facility.OpenFacilityEffectTrans.position);

        //    if (GameRoot.Instance.UserData.CurMode.StageData.IsMaxFloor)
        //    {
        //        Utility.SetActiveCheck(facilityEmpty.gameObject, false);
        //        facilityEmpty.UIActive(false);
        //        facilityEnd.transform.position = new Vector3(0f, -cameraYInterval * nextOpenFloor, 0f);
        //    }
        //    else
        //    {
        //        facilityEmpty.transform.position = new Vector3(0f, -cameraYInterval * nextOpenFloor, 0f);
        //        facilityEnd.transform.position = new Vector3(0f, -cameraYInterval * (nextOpenFloor + 1), 0f);

        //        Utility.SetActiveCheck(facilityEmpty.gameObject, true);
        //        facilityEmpty.UpdatePos();
        //    }
        //    if (GameRoot.Instance.UserData.CurMode.StageData.IsMaxFloor)
        //        facilityEnd.ChangeDir((int)Facility.transform.localScale.x * -1);
        //    else
        //    {
        //        var EndFacility = GetFacility(nextOpenFloor - 1);
        //        Utility.SetActiveCheck(EndFacility.gameObject, true);
        //        EndFacility.DisableEffect(false);
        //        var endScaleX = EndFacility.transform.localScale.x * -1;
        //        facilityEnd.ChangeDir((int)endScaleX);
        //    }
        //    facilityEnd.UpdatePos();
        //    facilityRoulette?.UpdatePos();
        //}
    }
}
