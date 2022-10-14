using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BanpoFri
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIEventPathAttribute : Attribute
    {
        private string path;

        public UIEventPathAttribute(string name)
        {
            path = name;
        }

        public string Path
        {
            get
            {
                return path;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class UIPathAttribute : Attribute
    {
        private string path;
        private bool hud;
        private bool world;

        public UIPathAttribute(string name, bool _hud = false, bool _world = false)
        {
            path = name;
            hud = _hud;
            world = _world;
        }
        public string Path
        {
            get
            {
                return path;
            }
        }

        public bool Hud
        {
            get
            {
                return hud;
            }
        }

        public bool World
        {
            get
            {
                return world;
            }
        }
    }

    public enum UIBaseType
    {
        Ingame,
        Page,
        Popup,
        Top,
    }

    interface IUIBase
    {
        UIBaseType UIType {get;}
    }

    public interface IScreenAction
    {
        UIBase.HUDType[] HudType {get;}
        bool IsScreenAction {get;}
        void ScreenAction(bool value);
        bool IsScreenTopOn();
        void ScreenTopOn(bool value);
    }

    public interface IFloatingUI
    {
        void Init(Transform parent);
    }

    [RequireComponent(typeof(Animator))]
    public class UIBase : MonoBehaviour, IUIBase
    {
        [Serializable]
        public enum HUDType
        {
            None,
            All,
            OnlyBottom,
            OnlyTop,
            OnlyQuest,
            OnlyLeft,
            OnlyRight,
        }

        [Serializable]
        public class SortingOrder
        {
            public GameObject target;
            public int order;
        }
        public class OriginSortingData
        {
            public string sortingLayerName;
            public int sortingOrder;
        }
        [SerializeField]
        private UIBaseType uiType;
        public UIBaseType UIType { get{
            return uiType;
        }}
        [HideInInspector]
        [SerializeField]
        protected string uiShowStateName;
        [SerializeField]
        protected Animator activeAnimator;
        [SerializeField]
        protected Button closeBtn;
        [SerializeField]
        private List<SortingOrder> sortingObjects = new List<SortingOrder>();
        [SerializeField]
        private bool batchParticleOrder = false;
        public bool DontCloseInteraction = false;
        [SerializeField]
        protected float touchLockTime = .5f;

        public Action OnUIHide = null;
        public Action OnUIHideAfter = null;
		public Action OnUIShowBefore = null;
		public Action OnUIShowAfter = null;
        protected bool manualAnimator = false;
        private OriginSortingData originSortingData = new OriginSortingData();

        public bool isInHide { get { return activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hide"); } }

        protected virtual void Awake() {            
            if(closeBtn != null)
                closeBtn.onClick.AddListener(Hide);  
        }

        protected virtual void OnEnable() 
        {
            if(activeAnimator == null)
            {
                OnShowBefore();
                OnShowAfter();            
                if(closeBtn != null)
                    closeBtn.interactable = true;
            }    
            else
            {
                if(manualAnimator)
                    return;
                    
                if(!activeAnimator.enabled)
                    activeAnimator.enabled = true;
                activeAnimator.Play(uiShowStateName, 0, 0f);
                if(closeBtn != null)
                    StartCoroutine(WaitCallback(touchLockTime, () => {
                        closeBtn.interactable = true;
                    }));
            }
        }

        public virtual void SaveOringSortingData()
        {
            var canvas = GetComponent<Canvas>();
            originSortingData.sortingLayerName = canvas.sortingLayerName;
            originSortingData.sortingOrder = canvas.sortingOrder;
        }

        public virtual int GetSortingOrderLayer()
        {
           return GetComponent<Canvas>().sortingOrder;
        }


        public void MaxSort()
        {
            GetComponent<Canvas>().sortingOrder = 32767;
        }

        public virtual void RecoverySortingData()
        {
            var canvas = GetComponent<Canvas>();
            canvas.sortingLayerName = originSortingData.sortingLayerName;
            canvas.sortingOrder = originSortingData.sortingOrder;
        }

        public void ReOderParticleInUIBase()
        {
            var parentOrder = GetComponent<Canvas>().sortingOrder;
            foreach(var obj in sortingObjects)
            {
                var canvas = obj.target.GetComponent<Canvas>();
                if(canvas != null)
                {
                    canvas.sortingOrder = obj.order + parentOrder;
                }
                else
                {
                    var particle = obj.target.GetComponent<ParticleSystemRenderer>();
                    if(particle != null)
                    {
                        particle.sortingOrder = obj.order + parentOrder;
                    }
                }
            }
            if(batchParticleOrder)
            {
                var particles = GetComponentsInChildren<ParticleSystemRenderer>();
                foreach(var particle in particles)
                {
                    particle.sortingOrder = parentOrder + 88;
                }
            }
        }

        protected void ShowImediately()
        {
            if(activeAnimator)
            {
                activeAnimator.ResetTrigger("Hide");
                activeAnimator.Play(uiShowStateName, 0, 0f);
            }
        }

        public virtual void Show()
        {
            if(closeBtn != null)    
                closeBtn.interactable = false;

            if (!gameObject.activeSelf)
            {
                for (var i = 0; i < this.transform.childCount; ++i)
                {
                    var child = this.transform.GetChild(i);
                    Utility.SetActiveCheck(child.gameObject, false);
                }
            }
        }

        public virtual void OnShowBefore()
        {
            for(var i = 0; i < this.transform.childCount; ++i)
            {
                var child = this.transform.GetChild(i);
                Utility.SetActiveCheck(child.gameObject, true);
            }
            OnUIShowBefore?.Invoke();
            OnUIShowBefore = null;
        }

        protected IEnumerator WaitCallback(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }


        public virtual void OnShowAfter()
        {
            if(closeBtn != null)
            closeBtn.interactable = true;

            OnUIShowAfter?.Invoke();
            OnUIShowAfter = null;
        }

        public virtual void CustomSortingOrder()
        {
        }

        public virtual void OnHideBefore()
        {
        }

        public virtual void OnHideAfter()
        {

            var canvas = GetComponent<Canvas>();
            canvas.sortingOrder = 0;

            this.gameObject.SetActive(false);

            OnUIHideAfter?.Invoke();
            OnUIHideAfter = null;

       
        }

        public virtual void Hide()
        {
            OnUIHide?.Invoke();
            OnUIHide = null;
			if (activeAnimator)
				activeAnimator.Play("Hide", -1, 0f);
			else
				OnHideAfter();
            if(closeBtn != null)
                closeBtn.interactable = false;


		}
    }
}