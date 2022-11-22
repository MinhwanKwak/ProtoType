using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using BanpoFri;

public interface IInGameMode
{
    void Load();
    void UnLoad();
    void SetCameraBoundMinY(float value);
}

public abstract class InGameMode : MonoBehaviour, IInGameMode
{
    [SerializeField]
    private PanAndZoom ingameCamera;
    public PanAndZoom IngameCamera {get{ return ingameCamera;}}
    public Camera MainCamera {get {return IngameCamera.cam;}}

    [SerializeField]
    private float zoomTime = 3f;
    private float deletaTimeZoom = 0f;
    private bool zoomOut = false;

    private void Awake()
    {
        if(GameRoot.IsInit())
        {
            GameRoot.Instance.InGameSystem.RegisteInGame(this);
            GameRoot.Instance.UISystem.WorldCanvas.worldCamera = MainCamera;
            // ingameCamera.onPinch += (oldd, newd) => {
            //     if(GameRoot.Instance.TutorialSystem.IsActive())
            //         return;

            //     if(ingameCamera.IsZoomOutOver)
            //     {
            //         if(!zoomOut)
            //         {
            //             zoomOut = true;                        
            //             GameRoot.Instance.UISystem.ScreenAction(false, UIBase.HUDType.All);
            //         }
            //         deletaTimeZoom = 0f;
            //     }
            //     else
            //     {
            //         if(zoomOut)
            //         {
            //             zoomOut = false;
            //             GameRoot.Instance.UISystem.ScreenAction(true, UIBase.HUDType.All);
            //         }
            //     }
            // };
            // ingameCamera.onSwipe += (newd) => {
            //     if(GameRoot.Instance.TutorialSystem.IsActive())
            //         return;
            //     if(ingameCamera.IsZoomOutOver)
            //     {
            //         if(!zoomOut)
            //         {
            //             zoomOut = true;
            //             GameRoot.Instance.UISystem.ScreenAction(false, UIBase.HUDType.All);
            //         }
            //         deletaTimeZoom = 0f;
            //     }
            // };
            Load();
        }
        else
        {
            GameRoot.Load();
        }
    }

    protected virtual void Update()
    {
        // if(zoomOut)
        // {
        //     if(deletaTimeZoom >= zoomTime)
        //     {
        //         zoomOut = false;
        //         deletaTimeZoom = 0f;
        //         GameRoot.Instance.UISystem.ScreenAction(true, UIBase.HUDType.All);
        //         ingameCamera.FocusOut();
        //     }
        //     deletaTimeZoom += Time.deltaTime;
        // }
    }

    public void SetCameraBoundMinY(float value)
    {
        IngameCamera.boundMinY = value;
    }

    public virtual void Load() 
    {
        LoadUI();
    }
    public virtual void UnLoad() 
    {
        UnLoadUI();
    }

    protected virtual void LoadUI() {}
    protected virtual void UnLoadUI() {
        //GameRoot.Instance.UISystem.UnLoadUIAll();
    }
}