using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.UI;



[UIPath("UI/Popup/BuildHpUI")]
public class BuildHpUI : InGameFloatingUI
{

    [SerializeField]
    private Slider HpSlider;

    [SerializeField]
    private Text HpText;


    public void SetValue(float value)
    {


    }
}
