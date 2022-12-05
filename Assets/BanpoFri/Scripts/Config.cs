using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;
using BanpoFri;

[System.Serializable]
public class Config : BanpoFri.SingletonScriptableObject<Config>, BanpoFri.ILoader
{

    public enum LandCondination
    {
        Great,
        Basic,
        Sad,
    }


    public enum Language
    {
        ko,
        en,
        ja,
        th,
        de,
        fr,
    }


    public enum RewardType
    {
        CurrencyGroup = 1,
        RewardGroup = 2,
        ManagerGroup = 3,
        DragonCaveGroup = 4,
        EventSpecialQuest = 1001,
    }

    public enum RewardGroup
    {
        Currency = 1,
        RandomManager = 2,
        Manager = 3,
        DragonCave = 4,
        DragonCaveRandomManager = 5,

        DropBox = 996,
        EventSeasonalMainReward = 997,
        EventBuffCat = 998,
        StageClearBox = 999,
    }




    public enum CurrencyID
    {
        StoreMoney = 9999,
        Money = 1,
        Material = 2,
        Cash = 3,
        FeverCoin = 4,
        Medal = 5,
        SilverDump = 6,
        GoldDump = 7,
        EventMoney = 101,
        EventMaterial = 102,
        EventMedal = 103,
        GachaCoin = 104,
        ClearBox = 999,
        EnergyFly = 1001,
        EnergyLightning = 1002,
        ConquestTicket = 10001,
        DragonTicket = 10002,
        PropertyTicket = 10003
    }

    public enum ManagerGrade
    {
        Noraml = 1,
        Rare = 2,
        Unique = 3,
        UnCommon = 0,
    }



    [System.Serializable]
    public class ColorDefine
    {
        public string key_string;
        public Color color;
    }

    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _textColorDefines = new List<ColorDefine>();
    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _eventTextColorDefines = new List<ColorDefine>();
    private Dictionary<string, Color> _textColorDefinesDic = new Dictionary<string, Color>();
    public List<ColorDefine> TextColorDefines {
        get {
            return _textColorDefines;
        }
    }
    public List<ColorDefine> EventTextColorDefines {
        get {
            return _eventTextColorDefines;
        }
    }

    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _imageColorDefines = new List<ColorDefine>();
    [HideInInspector]
    [SerializeField]
    private List<ColorDefine> _eventImgaeColorDefines = new List<ColorDefine>();
    private Dictionary<string, Color> _imageColorDefinesDic = new Dictionary<string, Color>();
    public List<ColorDefine> ImageColorDefines {
        get {
            return _imageColorDefines;
        }
    }
    public List<ColorDefine> EventImageColorDefines {
        get {
            return _eventImgaeColorDefines;
        }
    }

    public Material SkeletonGraphicMat;
    public Material DisableSpriteMat;
    public Material EnableSpriteMat;
    public Material ImgAddtiveMat;
    [SerializeField]
    private SpriteAtlas DynamicAtlas;
    [SerializeField]
    private SpriteAtlas DynamicShopAtlas;
    [SerializeField]
    private SpriteAtlas ItemCardAtlas;
    [SerializeField]
    private SpriteAtlas CommonAtlas;
    [SerializeField]
    private SpriteAtlas facilityAtlas;
    [SerializeField]
    private SpriteAtlas UiIconAtlas;
    [SerializeField]
    private SpriteAtlas UiBgAtlas;


    public Color GetTextColor(string key)
    {
        if (_textColorDefinesDic.ContainsKey(key))
            return _textColorDefinesDic[key];

        return Color.white;
    }


    public Color GetImageColor(string key)
    {
        if (_imageColorDefinesDic.ContainsKey(key))
            return _imageColorDefinesDic[key];

        return Color.white;
    }

    public Color GetManagerGradeBGColor(ManagerGrade grade)
    {
        switch (grade)
        {
            case ManagerGrade.Noraml:
                return GetImageColor("Card_Normal_Bg");
            case ManagerGrade.Rare:
                return GetImageColor("Card_Rare_Bg");
            case ManagerGrade.Unique:
                return GetImageColor("Card_Unique_Bg");
        }

        return Color.white;
    }

    public Color GetManagerGradeFrameColor(ManagerGrade grade)
    {
        switch (grade)
        {
            case ManagerGrade.Noraml:
                return GetImageColor("Card_Normal_Frame");
            case ManagerGrade.Rare:
                return GetImageColor("Card_Rare_Frame");
            case ManagerGrade.Unique:
                return GetImageColor("Card_Unique_Frame");
        }

        return Color.white;
    }
    public Sprite GetItemcardImg(string key)
    {
        return ItemCardAtlas.GetSprite(key);
    }

  

    public Sprite GetIUiIconImg(string key)
    {
        return UiIconAtlas.GetSprite(key);
    }

    public Sprite GetUIBgImg(string key)
    {
        return UiBgAtlas.GetSprite(key);
    }


    public Sprite GetCommonImg(string key)
    {
        return CommonAtlas.GetSprite(key);
    }

    public Sprite GetDynamicImg(string key)
    {
        return DynamicAtlas.GetSprite(key);
    }

    public Sprite GetDynamicShop(string key)
    {
        return DynamicShopAtlas.GetSprite(key);
    }


    //public Sprite GetProductImg(string key)
    //{
    //    return productAtlas.GetSprite(key);
    //}

    
    public Sprite GetFacilityImg(string key)
    {
        return facilityAtlas.GetSprite(key);
    }



    public Sprite GetCurrencyImg(CurrencyID id)
    {
        switch(id)
        {
            case CurrencyID.Money:
                return GetDynamicImg("Dynamic_Icon_Btn_GameMoney");
            case CurrencyID.Cash:
                return GetDynamicImg("Dynamic_Icon_Btn_Gem");
            case CurrencyID.Material:
                return GetDynamicImg("Dynamic_Icon_Btn_Material");
            case CurrencyID.GachaCoin:
                return GetDynamicImg("Dynamic_Icon_Seasonal_RouletteCoin");
            case CurrencyID.EventMoney:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Money");
            case CurrencyID.EventMaterial:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Material");
            case CurrencyID.Medal:
                return GetDynamicImg("Dynamic_Icon_Medal");
            case CurrencyID.EventMedal:
                return GetDynamicImg("Dynamic_Icon_Seasonal_Ticket");
            case CurrencyID.EnergyLightning:
                return GetDynamicImg("Dynamic_Icon_Btn_Energy_2");
            case CurrencyID.EnergyFly:
                return GetDynamicImg("Dynamic_Icon_Btn_Energy_1");
            case CurrencyID.ConquestTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Conquest");
            case CurrencyID.DragonTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Dragon");
            case CurrencyID.PropertyTicket:
                return GetDynamicImg("Dynamic_Icon_ExpeditionTicket_Elements");

        }
        return null;
    }

    public void Load()
    {
        _textColorDefinesDic.Clear();
        foreach(var cd in _textColorDefines)
        {
            _textColorDefinesDic.Add(cd.key_string, cd.color);
        }
        foreach(var cd in _eventTextColorDefines)
        {
            _textColorDefinesDic.Add(cd.key_string, cd.color);
        }
        _imageColorDefinesDic.Clear();
        foreach(var cd in _imageColorDefines)
        {
            _imageColorDefinesDic.Add(cd.key_string, cd.color);
        }
        foreach(var cd in _eventImgaeColorDefines)
        {
            _imageColorDefinesDic.Add(cd.key_string, cd.color);
        }
    }

    public Sprite GetManagerBottomImg(ManagerGrade grade)
    {
        switch(grade)
        {
            case ManagerGrade.Noraml:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Normal");
            case ManagerGrade.Rare:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Rare");
            case ManagerGrade.Unique:
                return GetDynamicImg("Dynamic_Bg_ManagerPlace_Unique");
        }

        return null;
    }

    public Sprite GetManagerFrameImg(ManagerGrade grade)
    {
        switch(grade)
        {
            case ManagerGrade.Noraml:
                return GetItemcardImg("ItemCard_Frame_Card_Normal_Back");
            case ManagerGrade.Rare:
                return GetItemcardImg("ItemCard_Frame_Card_Rare_Back");
            case ManagerGrade.Unique:
                return GetItemcardImg("ItemCard_Frame_Card_Unique_Back");
        }

        return null;
    }
}
