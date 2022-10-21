using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSetting : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private string keyColor = string.Empty;
    private void Awake() 
    {
        if(string.IsNullOrEmpty(keyColor)) {
            return;
        }
        if(!Config.IsCreate) {
            return;
        }

        var tmp = GetComponent<Text>();
        if(tmp)
        {
            tmp.color = Config.Instance.GetTextColor(keyColor);
        }
        else
        {
            var textMesh = GetComponent<Text>();
            if(textMesh)
            {
                textMesh.color = Config.Instance.GetTextColor(keyColor);
            }
            else
            {
                var label = GetComponent<Text>();
                if(label)
                    label.color = Config.Instance.GetTextColor(keyColor);
            }
        }
    }
    
    [ExecuteInEditMode]
    public void SetTextColor(Color color)
    {
        var tmp = GetComponent<Text>();
        if(tmp)
        {
            tmp.color = color;
        }
        else
        {
            var textMesh = GetComponent<TMPro.TextMeshPro>();
            if(textMesh)
            {
                textMesh.color = color;
            }
            else
            {
                var label = GetComponent<Text>();
                if(label)
                    label.color = color;
            }
        }
    }
}
