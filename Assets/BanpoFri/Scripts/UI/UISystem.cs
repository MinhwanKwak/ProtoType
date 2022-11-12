using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace BanpoFri
{
    [Serializable]
    public class UISystem
    {
        public const string STR_SORTINGLAYER_PAGE = "Page";
        public const string STR_SORTINGLAYER_POPUP = "Popup";
        public const string STR_SORTINGLAYER_TOP = "Top";

        public const int START_PAGE_SORTING_NUMBER = 100;
        public const int START_POPUP_SORTING_NUMBER = 10000;
        public const int START_TOP_SORTING_NUMBER = 20000;
        

		public bool CheatHide = false;
        public Transform UIRootT {get; private set;}
        public Transform HUDUIRootT {get; private set;}
        public Canvas WorldCanvas {get; private set;}
		public GameObject LockScreen;
        private Dictionary<Type, UIBase> cachedUIs = new Dictionary<Type, UIBase>();
        private Dictionary<Type, GameObject> cachedIngameUITrans = new Dictionary<Type, GameObject>();
        private List<UIBase> openPopupList = new List<UIBase>();
        private List<IScreenAction> screenActionList = new List<IScreenAction>();
        private bool cacheMode = true;
		private Type LoadWaitUI = null;
		private Action<UIBase> OnLoadWait = null;
        private bool backButtonEnable = true;

        public Action ImmediatePackageAction = null;
        public Action EndEventRewardAction = null;





        public void OpenUI<T>(Action<T> OnLoad = null, Action OnClose = null, bool caching = true, int targetEventStage = -1) where T : UIBase
        {
            Action LoadComplete = () => 
            {
                var targetUI = cachedUIs[typeof(T)] as T;
                if(targetUI)
                {
                    var baseCanvas = targetUI.GetComponent<Canvas>();
                    //if (!targetUI.gameObject.activeSelf)
                    //    LockScreen.SetActive(true);
                    //targetUI.OnUIShowBefore = () => {

                    //    LockScreen.SetActive(false);
                    //};
                    targetUI.Show();
					targetUI.gameObject.SetActive(true);
                    switch(targetUI.UIType)
                    {
                        case UIBaseType.Popup:                            
                            {
                                baseCanvas.overrideSorting = true;
                                baseCanvas.sortingLayerName = STR_SORTINGLAYER_POPUP;
                                if(!openPopupList.Contains(targetUI))
                                    openPopupList.Add(targetUI);

                                calculatePopupSortingOrder();   
                                var uiBase = targetUI.GetComponent<UIBase>();
                                uiBase.ReOderParticleInUIBase();
                                if((!cacheMode) || caching == false)
                                {
                                    targetUI.OnUIHideAfter += () =>
                                    {
                                        EndEventRewardAction?.Invoke();
                                        ImmediatePackageAction?.Invoke();
                                        GameObject.Destroy(targetUI.gameObject, 3f);
                                    };
                                }
                                backButtonEnable = false;
                                targetUI.OnUIShowAfter = () => { 
                                    backButtonEnable = true;
                                };

                                targetUI.OnUIHide = () => {
                                    if((!cacheMode)||caching == false) cachedUIs.Remove(typeof(T));
                                    //baseCanvas.sortingOrder = 0;
                                    openPopupList.Remove(targetUI);
                                    calculatePopupSortingOrder();
                                    calculateHUDAction();
                                    EndEventRewardAction?.Invoke();
                                    ImmediatePackageAction?.Invoke();
                                    OnClose?.Invoke();
                                };
                            }
                        break;
                        case UIBaseType.Page:
                            {
                                var uiBase = targetUI.GetComponent<UIBase>();
                                if(uiBase is IScreenAction)
                                {
                                    screenActionList.Add(uiBase as IScreenAction);
                                }
                                
                                baseCanvas.sortingLayerName = STR_SORTINGLAYER_PAGE;
                                baseCanvas.sortingOrder = START_PAGE_SORTING_NUMBER;
                                targetUI.OnUIHide = () => {
                                    OnClose?.Invoke();
                                    screenActionList.Remove(uiBase as IScreenAction);
                                };
                            }
                        break;
                        case UIBaseType.Top:
                            baseCanvas.sortingLayerName = STR_SORTINGLAYER_TOP;
                            baseCanvas.sortingOrder = START_TOP_SORTING_NUMBER;
                            targetUI.OnUIHide = OnClose;
                        break;
                        default:
                            targetUI.OnUIHide = OnClose;
                        break;
                    }
                    targetUI.CustomSortingOrder();
					if(LoadWaitUI != null && LoadWaitUI.Equals(targetUI.GetType()))
					{
						OnLoadWait?.Invoke(targetUI);
						OnLoadWait = null;
					}
                    OnLoad?.Invoke(targetUI);
                }
                else
                    Debug.LogError($"UIBase::OpenUI don't load uiBase type: {typeof(T).Name}");
            };

            if(!cachedUIs.ContainsKey(typeof(T)))
            {
                bool findAtt = false;
                var attrs = Attribute.GetCustomAttributes(typeof(T));

                bool useEventPopup = false;
                var isExistPath = Array.Exists(attrs, x => x is UIPathAttribute);
                var isExistEvent = Array.Exists(attrs, x => x is UIEventPathAttribute);
                if (isExistEvent && targetEventStage > 10000) useEventPopup = true;

                System.Action<string> createUI = (string path) => { };
                foreach ( var attr in attrs)
                {
                    if(useEventPopup && attr is UIEventPathAttribute)
                    {
                        var uiPath = (UIEventPathAttribute) attr;

                        var addrPath = string.Format(uiPath.Path, targetEventStage);

                        cachedUIs.Add(typeof(T), null);

                        Addressables.InstantiateAsync(addrPath).Completed += (obj) => {
                            //var inst = GameObject.Instantiate( obj.Result );
                            var inst = obj.Result;

                            inst.transform.SetParent(UIRootT, false);

                            var uiBase = inst.GetComponent<T>();
                            cachedUIs[typeof(T)] = uiBase;
                            inst.gameObject.SetActive(false);
                            LoadComplete.Invoke();
                        };
                        findAtt = true;
                        break;
                    }
                    else if(attr is UIPathAttribute)
                    {
                        var uiPath = (UIPathAttribute) attr;

                        cachedUIs.Add(typeof(T), null);
						//Addressables.LoadAssetAsync<GameObject>(uiPath.Path).Completed += (obj) => {
						Addressables.InstantiateAsync(uiPath.Path).Completed += (obj) => {
							//var inst = GameObject.Instantiate( obj.Result );
							var inst = obj.Result;
                            if (uiPath.Hud)
                                inst.transform.SetParent(HUDUIRootT, false);
                            else if (uiPath.World)
                                inst.transform.SetParent(WorldCanvas.transform, false);
                            else
                                inst.transform.SetParent(UIRootT, false);
                            
                            var uiBase = inst.GetComponent<T>();
                            cachedUIs[typeof(T)] = uiBase;
                            inst.gameObject.SetActive(false);
                            LoadComplete.Invoke();
                        };
                        findAtt = true;
                        break;
                    }
                }

                if(!findAtt){
                    Debug.LogError("UIBase::OpenUI don't find UIPathAttribute. plz attach them");
                    return;
                }
            }
            else
            {
                if(cachedUIs[typeof(T)] != null)
                {
                    LoadComplete.Invoke();
                    //var targetUI = cachedUIs[typeof(T)];
                    //if(!targetUI.isInHide)
                    //    LoadComplete.Invoke();
                }
            }
        }

        public void PreLoadUI(Type type)
        {
            var attrs = Attribute.GetCustomAttributes(type);
            foreach( var attr in attrs)
            {
                if(attr is UIPathAttribute)
                {
                    var uiPath = (UIPathAttribute) attr;
                    cachedUIs.Add(type, null);
                    Addressables.InstantiateAsync(uiPath.Path).Completed += (obj) => {
                        var inst = obj.Result;
                        if (uiPath.Hud)
                            inst.transform.SetParent(HUDUIRootT, false);
                        else if (uiPath.World)
                            inst.transform.SetParent(WorldCanvas.transform, false);
                        else
                            inst.transform.SetParent(UIRootT, false);
                        
                        var uiBase = inst.GetComponent(type) as UIBase;
                        cachedUIs[type] = uiBase;
                        inst.SetActive(false);
                    };
                    break;
                }
            }
        }

        public void SetFloatingUIActiveAll(bool value)
        {
            foreach(var uiTrans in cachedIngameUITrans)
                Utility.SetActiveCheck(uiTrans.Value, value);
        }

        public void SetFloatingUIActive<T>(bool value) where T : IFloatingUI
        {
            var TType = typeof(T);
            if(cachedIngameUITrans.ContainsKey(TType))
            {
                Utility.SetActiveCheck(cachedIngameUITrans[TType], value);
            }
        }

        public GameObject GetFloatingUI<T>() where T : IFloatingUI
        {
            var TType = typeof(T);
            if(cachedIngameUITrans.ContainsKey(TType))
            {
                if(cachedIngameUITrans[TType].transform.childCount > 0)
                    return cachedIngameUITrans[TType].transform.GetChild(0).gameObject;
            }

            return null;
        }

        public void LoadFloatingUI<T>(Action<T> onSuccess, bool caching = false) where T : IFloatingUI
        {
            var TType = typeof(T);
            if(caching)
            {
                if(cachedIngameUITrans.ContainsKey(TType))
                {
                    if(cachedIngameUITrans[TType].transform.childCount > 0)
                    {
                        cachedIngameUITrans[TType].transform.SetAsLastSibling();
                        onSuccess?.Invoke(cachedIngameUITrans[TType].transform.GetChild(0).GetComponent<T>());
                        return;
                    }
                }
            }
            var attrs = Attribute.GetCustomAttributes(TType);
            foreach (var attr in attrs)
            {
                if (attr is UIPathAttribute)
                {
                    var uiPath = (UIPathAttribute)attr;
                    var handle = Addressables.InstantiateAsync(uiPath.Path);
                    handle.Completed += (obj) => {
                        var inst = obj.Result;
                        GameObject parent;
                        if(cachedIngameUITrans.ContainsKey(TType))
                            parent = cachedIngameUITrans[TType];
                        else
                        {
                            parent = new GameObject(TType.ToString());
                            parent.transform.SetParent(WorldCanvas.transform);
                            parent.transform.position = Vector3.zero;
                            parent.transform.localScale = Vector3.one;
                            cachedIngameUITrans.Add(TType, parent);
                        }
                        inst.transform.SetParent(parent.transform, false);
                        onSuccess?.Invoke(inst.GetComponent<T>());
                    };
                    break;
                }
            }
        }

        public void FloatingUIFirstDepth<T>() where T : IFloatingUI
        {
            var TType = typeof(T);
            if(cachedIngameUITrans.ContainsKey(TType))
            {
                cachedIngameUITrans[TType].transform.SetAsFirstSibling();
            }
        }

        public void CloseUI<T>() where T : UIBase
        {
            var target = GetUI<T>();
            if(target)
                target.Hide();
        }

        public void ClosePopupAll()
        { 
            var closeList = new List<UIBase>();
            foreach( var uibase in openPopupList)
            {
                if(uibase && !uibase.DontCloseInteraction)
                {
                    closeList.Add(uibase);
                }    
            }
            foreach( var uibase in closeList)
            {
                openPopupList.Remove(uibase);
                uibase.Hide();
            }
        }

        public void ClosePopupBackBtn()
        {
            if(!backButtonEnable)
                return;
                
            if(openPopupList.Count < 1)
                return;
            
            var ui = openPopupList.LastOrDefault();
            if(ui != null)
            {
                if(!ui.DontCloseInteraction)
                    ui.Hide();
            }
        }

        public UIBase GetOpenPopupLastUI()
        {
            if(openPopupList.Count < 1)
                return null;

            return openPopupList.LastOrDefault();
        }

        public int GetOpenPopupCount()
        {
            return openPopupList.Count;
        }

        public T GetUI<T>(Action<UIBase> onLoadWait = null) where T : UIBase
        {
            if(cachedUIs.ContainsKey(typeof(T)))
			{
				var returnValue = cachedUIs[typeof(T)] as T;
				if (onLoadWait != null && returnValue == null)
				{
					LoadWaitUI = typeof(T);
					OnLoadWait = onLoadWait;
				}
				return returnValue;
			}                
            else
			{
				if (onLoadWait != null)
				{
					LoadWaitUI = typeof(T);
					OnLoadWait = onLoadWait;
				}
				return null;
			}
				
		}

        public void FrontUIbyUI<T,U>() where T : UIBase where U : UIBase
        {
            var frontUI = GetUI<T>();
            var backUI = GetUI<U>();

            if(frontUI == null || backUI == null)
                return;

            frontUI.SaveOringSortingData();
            var frontCanvas = frontUI.GetComponent<Canvas>();
            var backCanvas = backUI.GetComponent<Canvas>();
            frontCanvas.sortingLayerName = backCanvas.sortingLayerName;
            frontCanvas.sortingOrder = backCanvas.sortingOrder + 99;
        }
        public void RecoveryUIOrder<T>() where T : UIBase
        {
            var target = GetUI<T>();
            if(target != null)
            {
                target.RecoverySortingData();
            }
        }

		public void AllUIHide()
		{
			CheatHide = true;
			foreach (var ui in cachedUIs)
				ui.Value.gameObject.SetActive(false);
		}
		public void AllUIShow()
		{
			CheatHide = false;
			foreach (var ui in cachedUIs)
			{
				if (ui.Value.UIType == UIBaseType.Ingame || ui.Value.UIType == UIBaseType.Page)
				{
					if(!ui.Value.name.Contains("Loading"))
						ui.Value.gameObject.SetActive(true);
				}
			}
		}

        private void calculatePopupSortingOrder()
        {
            var idx = START_POPUP_SORTING_NUMBER;
            foreach(var uibase in openPopupList)
            {
                 var baseCanvas = uibase.GetComponent<Canvas>();
                if(baseCanvas != null)
                {
                    baseCanvas.sortingOrder = idx;
                }
                idx += 100;
            }
        }

        private void calculateHUDAction()
        {
            if(openPopupList.Count < 1)
            {
                ScreenAction(true, UIBase.HUDType.All);
                ScreenTopOn(false, UIBase.HUDType.All);
            }
        }

        public void UnLoadUIAll()
        {
            foreach(var ui in cachedUIs)
            {
                if (ui.Value != null)
                    Addressables.ReleaseInstance(ui.Value.gameObject);
                    //GameObject.Destroy(ui.Value.gameObject);
            }
            cachedUIs.Clear();
            foreach(var trans in cachedIngameUITrans)
            {
                foreach(Transform child in trans.Value.transform)
                {
                    Addressables.ReleaseInstance(child.gameObject);
                }
                GameObject.Destroy(trans.Value);
            }
            cachedIngameUITrans.Clear();
            openPopupList.Clear();
            screenActionList.Clear();
        }

        public List<GameObject> GetOpendedUI()
        {
            List<GameObject> result = new List<GameObject>();
            foreach(var ui in cachedUIs)
            {
                if(ui.Value.gameObject.activeSelf)
                {
                    result.Add(ui.Value.gameObject);
                }
            }

            return result;
        }
        
        public void ScreenAction(bool value, UIBase.HUDType type)
        {
            foreach(var ui in screenActionList)
            {
                if(ui.HudType.Contains(type))
                    ui.ScreenAction(value);
            }
        }

        public bool IsScreenAction(UIBase.HUDType type)
		{
            foreach (var ui in screenActionList)
            {
                if (ui.HudType.Contains(type))
                    return ui.IsScreenAction;
            }

            return false;
        }

        public void ScreenTopOn(bool value, UIBase.HUDType type)
        {
            foreach(var ui in screenActionList)
            {
                if(ui.HudType.Contains(type))
                    ui.ScreenTopOn(value);
            }
        }

        public bool IsScreenTopOn(UIBase.HUDType type)
		{
            foreach (var ui in screenActionList)
            {
                if (ui.HudType.Contains(type))
                    return ui.IsScreenTopOn();
            }

            return false;
        }

        public void SetMainUI(Transform _trans)
        {
            UIRootT = _trans;
        }
        public void SetHudUI(Transform _trans)
        {
            HUDUIRootT = _trans;
        }

        public void SetWorldCanvas(Canvas _canvas)
        {
            WorldCanvas = _canvas;
        }
    }    
}
