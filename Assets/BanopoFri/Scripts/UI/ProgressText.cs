using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressText : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private string frontTextColorKey;
    [HideInInspector]
    [SerializeField]
    private string backTextColorKey;
    [SerializeField]
    private bool FullColorPossible = false;
    [HideInInspector]
    [SerializeField]
    private string fullTextColorKey;
    [SerializeField]
    private Text text;


    private void Awake()
    {
        text = GetComponent<Text>();
    }

    [ExecuteInEditMode]
    public void SetValue(string front, string back, bool fullColor = false)
    {
        if( text == null)
        {
            text = GetComponent<Text>();
            if( text == null)
            {
                Debug.LogError("ProgressText::SetValue  text is null");
                return;
            }
        }
        #if UNITY_EDITOR
        Color frontColor;
        Color backColor;
        Color _fullColor;
        if(!Application.isPlaying)
        {
            var loadData = UnityEditor.AssetDatabase.LoadAssetAtPath<Config>("Assets/Arts/Config/Config.asset");
            frontColor = loadData.GetTextColor(frontTextColorKey);
            backColor = loadData.GetTextColor(backTextColorKey);
            _fullColor = string.IsNullOrEmpty(fullTextColorKey) ? Color.white : loadData.GetTextColor(fullTextColorKey);
        }
        else
        {
            frontColor = Config.Instance.GetTextColor(frontTextColorKey);
            backColor = Config.Instance.GetTextColor(backTextColorKey);
            _fullColor = string.IsNullOrEmpty(fullTextColorKey) ? Color.white : Config.Instance.GetTextColor(fullTextColorKey);
        }        
        #else
        var frontColor = Config.Instance.GetTextColor(frontTextColorKey);
        var backColor = Config.Instance.GetTextColor(backTextColorKey);
        var _fullColor = string.IsNullOrEmpty(fullTextColorKey) ? Color.white : Config.Instance.GetTextColor(fullTextColorKey);
        #endif
        if(fullColor && FullColorPossible)
        {
            text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_fullColor)}>{front}</color><color=#{ColorUtility.ToHtmlStringRGB(_fullColor)}>/{back}</color>";

        }
        else
            text.text = $"<color=#{ColorUtility.ToHtmlStringRGB(frontColor)}>{front}</color><color=#{ColorUtility.ToHtmlStringRGB(backColor)}>/{back}</color>";
    }

    public void Test()
    {
        SetValue("0","1");
    }
}
