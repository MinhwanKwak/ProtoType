using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BanpoFri;
using System;

public class CheatWindow : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

    public void SetMoney()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            TpLog.LogError("input field empty!");
            return;
        }
        BigInteger convert;
        if (!BigInteger.TryParse(inputField.text, out convert))
        {
            TpLog.LogError("input field string don't convert number!");
            return;
        }
        inputField.text = "";
        GameRoot.Instance.UserData.CurMode.Money.Value += convert;
        GameRoot.Instance.UserData.HUDMoney.Value += convert;
    }

    public void SetMaterial()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            TpLog.LogError("input field empty!");
            return;
        }
        BigInteger convert;
        if (!BigInteger.TryParse(inputField.text, out convert))
        {
            TpLog.LogError("input field string don't convert number!");
            return;
        }

        inputField.text = "";

        if (convert > int.MaxValue || (convert + GameRoot.Instance.UserData.CurMode.Material.Value) > int.MaxValue)
        {
            GameRoot.Instance.UserData.CurMode.Material.Value = int.MaxValue;
            GameRoot.Instance.UserData.HUDMaterial.Value = int.MaxValue;
        }
        else
        {
            GameRoot.Instance.UserData.CurMode.Material.Value += (int)convert;
            GameRoot.Instance.UserData.HUDMaterial.Value += (int)convert;
        }
    }



    public void SetCash()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            TpLog.LogError("input field empty!");
            return;
        }

        BigInteger convert;
        if (!BigInteger.TryParse(inputField.text, out convert))
        {
            TpLog.LogError("input field string don't convert number!");
            return;
        }

        inputField.text = "";

        if (convert > int.MaxValue || (convert + GameRoot.Instance.UserData.Cash.Value) > int.MaxValue)
        {
            GameRoot.Instance.UserData.Cash.Value = int.MaxValue;
            GameRoot.Instance.UserData.HUDCash.Value = int.MaxValue;
        }
        else
        {
            GameRoot.Instance.UserData.Cash.Value += (int)convert;
            GameRoot.Instance.UserData.HUDCash.Value += (int)convert;
        }
    }





#if UNITY_EDITOR
    [UnityEditor.MenuItem("BanpoFri/ShowCheat _F3")]
    static void ShowCheat()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            GameRoot.Instance.SetCheatWindow(true);
    }
#endif
}
