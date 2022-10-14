using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BanpoFri
{
    public class UIBaseStateMachine : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var uiBase = animator.gameObject.GetComponent<UIBase>();
            if(uiBase)
            {
                // if(stateInfo.IsName("Init"))
                //     uiBase.OnShowBefore();
                // else 
                if(stateInfo.IsName("Idle"))
                    uiBase.OnShowAfter();
                else if(stateInfo.IsName("Hide"))
                    uiBase.OnHideBefore();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var uiBase = animator.gameObject.GetComponent<UIBase>();
            if(uiBase)
            {
                if(stateInfo.IsName("Init"))
                    uiBase.OnShowBefore();
                else if(stateInfo.IsName("Hide"))
                    uiBase.OnHideAfter();
            }
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}