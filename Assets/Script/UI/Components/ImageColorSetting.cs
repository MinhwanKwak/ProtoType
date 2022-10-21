using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorSetting : MonoBehaviour
{
    public enum ImageType {Unknown, SpriteRenderer, Image, Renderer};
    [SerializeField]
    protected ImageType currentImageType = ImageType.Unknown;
    [SerializeField]
    protected bool SetEventMode = false;

    [HideInInspector]
    [SerializeField]
    private string keyColor = string.Empty;
    [HideInInspector]
    [SerializeField]
    private List<string> eventKeyColor = new List<string>();// string.Empty;
    [HideInInspector]
    [SerializeField]
    private int eventCount = 0;

    private void Start()
    {
        //if(string.IsNullOrEmpty(keyColor)) {
        //    return;
        //}

        //if(GameRoot.Instance.CurInGameType == InGameType.Main ||
        //    SetEventMode == false ||
        //    eventKeyColor.Count == 0)
        //    SetImageColor(Config.Instance.GetImageColor(keyColor));
        //else if(GameRoot.Instance.CurInGameType == InGameType.Event)
        //{
        //    var theme = GameRoot.Instance.EventSystem.CurOpendStage % 10000;
        //    if (eventKeyColor.Count < theme) SetImageColor(Config.Instance.GetImageColor(keyColor));
        //    else if(eventKeyColor[theme-1].Length == 0) SetImageColor(Config.Instance.GetImageColor(keyColor));

        //    SetImageColor(Config.Instance.GetImageColor(eventKeyColor[theme-1]));
        //}

        UpdateColor();
    }

    public void UpdateColor()
    {

        if (string.IsNullOrEmpty(keyColor))
        {
            return;
        }

        //if (GameRoot.Instance.CurInGameType == InGameType.Main ||
        //    SetEventMode == false ||
        //    eventKeyColor.Count == 0)
            SetImageColor(Config.Instance.GetImageColor(keyColor));
        //else if (GameRoot.Instance.CurInGameType == InGameType.Event)
        //{
        //    var theme = GameRoot.Instance.EventSystem.CurOpendStage % 10000;
        //    if (eventKeyColor.Count < theme) SetImageColor(Config.Instance.GetImageColor(keyColor));
        //    else if (eventKeyColor[theme - 1].Length == 0) SetImageColor(Config.Instance.GetImageColor(keyColor));

        //    SetImageColor(Config.Instance.GetImageColor(eventKeyColor[theme - 1]));
        //}
    }

    public void SetImageColor(Color color) 
    {
        if(currentImageType == ImageType.Unknown) {
            return;
        }

        if((currentImageType == ImageType.SpriteRenderer) && TrySetColorTo<SpriteRenderer>(color)) {
            currentImageType = ImageType.SpriteRenderer;
            return;
        }

        if((currentImageType == ImageType.Image) && TrySetColorTo<Image>(color)) {
            currentImageType = ImageType.Image;
            return;
        }

        if((currentImageType == ImageType.Renderer) && TrySetColorTo<Renderer>(color)) {
            currentImageType = ImageType.Renderer;
            return;
        }
    }

    protected bool TrySetColorTo<T>(Color color) 
    {
        var temp = GetComponent<T>();

        if(temp == null) {
            return false;
        }

        if(temp is SpriteRenderer) {
            (temp as SpriteRenderer).color = color;
        } else if (temp is Image) {
            (temp as Image).color = color;
        } else if (temp is Renderer) {
            (temp as Renderer).material.SetColor("_Color", color);
        }

        return true;
    }
}
