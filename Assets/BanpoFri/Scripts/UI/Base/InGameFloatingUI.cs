using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;


public class InGameFloatingUI : MonoBehaviour, IFloatingUI
{
    [SerializeField]
    private bool TrackingScale = false;
    [SerializeField]
    private bool TrackingPos = false;
    private Transform FollowTrans = null;

    public virtual void Init(Transform parent)
    {
        FollowTrans = parent;
        this.transform.position = FollowTrans.position;
    }

    public void UpdatePos()
    {
        if (FollowTrans != null)
            this.transform.position = FollowTrans.position;
    }

    protected virtual void Update()
    {
        if (TrackingPos)
        {
            if (FollowTrans != null)
                this.transform.position = FollowTrans.position;
        }

        if (TrackingScale)
        {
            if (FollowTrans != null)
                this.transform.localScale = FollowTrans.localScale;
        }
    }
}
