using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;
public class BuildCamp : MonoBehaviour
{
    [SerializeField]
    protected Image BuildImg;

    [SerializeField]
    protected Transform BuildHpTr;

    protected int Hp = 0;

    protected BuildHpUI HpUI;

    public virtual void Init()
    {

    }


    public virtual void Dead()
    {

    }
}
