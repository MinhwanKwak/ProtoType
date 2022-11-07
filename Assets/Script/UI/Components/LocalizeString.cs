using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BanpoFri;
using TMPro;
public class LocalizeString : MonoBehaviour
{
    public static List<LocalizeString> Localizelist { get; set; } = new List<LocalizeString>();
    [HideInInspector]
    [SerializeField]
    private string keyLocalize = "str_error";

    [SerializeField]
    private bool IsTextMeshPro = false;

    private void Start() {
        if(!Localizelist.Contains(this))
            Localizelist.Add(this);
        var tmp = GetComponent<Text>();
        var tmppro = GetComponent<TextMeshProUGUI>();
        RefreshText();
    }
    public void RefreshText()
    {
        if (!IsTextMeshPro)
        {
            var tmp = GetComponent<Text>();
            if (tmp)
            {
                tmp.text = Tables.Instance.GetTable<Localize>().GetString(keyLocalize);
            }
            else
            {
                var label = GetComponent<Text>();
                if (label)
                    label.text = Tables.Instance.GetTable<Localize>().GetString(keyLocalize);
            }
        }
        else
        {
            var tmppro = GetComponent<TextMeshProUGUI>();
            if (tmppro)
            {
                tmppro.text = Tables.Instance.GetTable<Localize>().GetString(keyLocalize);
            }
            else
            {
                var label = GetComponent<TextMeshProUGUI>();
                if (label)
                    label.text = Tables.Instance.GetTable<Localize>().GetString(keyLocalize);
            }
        }
    }
  
    public void SetText(string txt)
    {
        if (!IsTextMeshPro)
        {
            var tmp = GetComponent<Text>();
            if (tmp)
            {
                tmp.text = txt;
            }
            else
            {
                var label = GetComponent<Text>();
                if (label)
                    label.text = txt;
            }
        }
        else
        {
            var tmppro = GetComponent<TextMeshProUGUI>();
            if (tmppro)
            {
                tmppro.text = txt;
            }
            else
            {
                var label = GetComponent<TextMeshProUGUI>();
                if (label)
                    label.text = txt;
            }
        }
    }
}
