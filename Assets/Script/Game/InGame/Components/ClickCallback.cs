using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickCallback : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private string targetTagName = string.Empty;
    public ColliderClickedEvent OnCallback = new ColliderClickedEvent();

    [System.Serializable]
    public class ColliderClickedEvent : UnityEvent
    {
    }
    
    public void Click(string clickedTag)
    {
		if (targetTagName == clickedTag)
        {
            OnCallback?.Invoke();
            if (clickedTag == "SpeedUp" || clickedTag == "FacilitySlot1" || clickedTag == "FacilitySlot2" || clickedTag == "FacilitySlot3")
            {
                return;
            }

           //SoundPlayer.Instance.PlaySound("btn");
        }
    }

    public void TutorialClick()
    {
        OnCallback?.Invoke();
    }
}
